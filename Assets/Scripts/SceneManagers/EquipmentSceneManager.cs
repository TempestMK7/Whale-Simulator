using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EquipmentSceneManager : MonoBehaviour {

    public GameObject masterPanel;
    public GameObject detailPanel;

    // Master panel views.
    public RecyclerView masterEquipmentRecycler;
    public GameObject masterListItemPrefab;

    // Detail panel views.
    public Text equipmentLabel;
    public Text equipmentPositionLabel;
    public Image equipmentOwnerIcon;

    public Image equipmentDisplay;

    public RarityBehavior equipmentRarityView;
    public Text fuseButtonText;

    public GameObject statPanel;
    public Text attackLabel;
    public Text magicLabel;
    public Text critLabel;
    public Text defenseLabel;
    public Text reflectionLabel;
    public Text deflectLabel;

    // Master panel things.
    private EquipmentFilter? currentFilter;
    private List<AccountEquipment> filteredList;
    private MasterEquipmentAdapter masterAdapter;

    // Detail panel things.
    private int currentSelection;

    public void Awake() {
        masterPanel.SetActive(true);
        detailPanel.SetActive(false);

        masterAdapter = new MasterEquipmentAdapter(this, masterListItemPrefab);
        masterEquipmentRecycler.SetAdapter(masterAdapter);
        FilterList();
    }

    #region Master panel methods.

    public void OnFilterPressed(int position) {
        var allFilters = (EquipmentFilter[])Enum.GetValues(typeof(EquipmentFilter));
        var newFilter = allFilters[position];
        if (newFilter == currentFilter) {
            currentFilter = null;
        } else {
            currentFilter = newFilter;
        }
        FilterList();
    }

    private void FilterList() {
        var unfiltered = StateManager.GetCurrentState().AccountEquipment;
        if (currentFilter == null) {
            filteredList = unfiltered;
        } else {
            var allowedTypes = new List<EquipmentType>();
            switch (currentFilter) {
                case EquipmentFilter.ONE_HAND:
                    allowedTypes.Add(EquipmentType.DAGGER);
                    allowedTypes.Add(EquipmentType.SWORD);
                    allowedTypes.Add(EquipmentType.AXE);
                    allowedTypes.Add(EquipmentType.CLUB);
                    allowedTypes.Add(EquipmentType.SCEPTER);
                    allowedTypes.Add(EquipmentType.TOME);
                    allowedTypes.Add(EquipmentType.METAL_SHIELD);
                    allowedTypes.Add(EquipmentType.CRYSTAL_SHIELD);
                    break;
                case EquipmentFilter.TWO_HAND:
                    allowedTypes.Add(EquipmentType.GREAT_SWORD);
                    allowedTypes.Add(EquipmentType.GREAT_AXE);
                    allowedTypes.Add(EquipmentType.GREAT_CLUB);
                    allowedTypes.Add(EquipmentType.STAFF);
                    break;
                case EquipmentFilter.CLOTH:
                    allowedTypes.Add(EquipmentType.CLOTH_CHEST);
                    allowedTypes.Add(EquipmentType.CLOTH_PANTS);
                    allowedTypes.Add(EquipmentType.CLOTH_HAT);
                    break;
                case EquipmentFilter.LEATHER:
                    allowedTypes.Add(EquipmentType.LEATHER_CHEST);
                    allowedTypes.Add(EquipmentType.LEATHER_PANTS);
                    allowedTypes.Add(EquipmentType.LEATHER_HAT);
                    break;
                case EquipmentFilter.PLATE:
                    allowedTypes.Add(EquipmentType.PLATE_CHEST);
                    allowedTypes.Add(EquipmentType.PLATE_PANTS);
                    allowedTypes.Add(EquipmentType.PLATE_HELMET);
                    break;
                case EquipmentFilter.CRYSTAL:
                    allowedTypes.Add(EquipmentType.CRYSTAL_CHEST);
                    allowedTypes.Add(EquipmentType.CRYSTAL_PANTS);
                    allowedTypes.Add(EquipmentType.CRYSTAL_HELMET);
                    break;
                default:
                    break;
            }
            filteredList = unfiltered.FindAll((AccountEquipment matchable) => {
                return allowedTypes.Contains(matchable.GetBaseEquipment().Type);
            });
        }
        masterAdapter.SetFilteredList(filteredList);
        masterEquipmentRecycler.NotifyDataSetChanged();
    }

    public void OnMasterListItemSelected(int position) {
        currentSelection = position;
        masterPanel.SetActive(false);
        detailPanel.SetActive(true);
        BindDetailPanel();
    }

    public void OnMasterBackPressed() {
        SceneManager.LoadSceneAsync("HubScene");
    }

    #endregion

    #region Detail panel methods

    public void BindDetailPanel() {
        var state = StateManager.GetCurrentState();
        var currentEquipment = filteredList[currentSelection];
        var baseEquipment = currentEquipment.GetBaseEquipment();

        equipmentLabel.text = baseEquipment.Name;
        equipmentPositionLabel.text = string.Format("({0} of {1})", currentSelection + 1, filteredList.Count);
        if (currentEquipment.EquippedHeroGuid == null) {
            equipmentOwnerIcon.enabled = false;
        } else {
            equipmentOwnerIcon.enabled = true;
            var equippedHero = state.AccountHeroes.Find((AccountHero hero) => {
                return hero.HeroGuid.Equals(currentEquipment.EquippedHeroGuid);
            });
            if (equippedHero != null) equipmentOwnerIcon.sprite = equippedHero.GetBaseHero().HeroIcon;
        }

        equipmentDisplay.sprite = baseEquipment.Icon;

        equipmentRarityView.SetLevel(0, currentEquipment.Level, true);
        ToggleStatFusePanels(true);

        attackLabel.text = string.Format("Attack: {0}", baseEquipment.BaseAttack * currentEquipment.Level);
        magicLabel.text = string.Format("Magic: {0}", baseEquipment.BaseMagic * currentEquipment.Level);
        critLabel.text = string.Format("Critical: {0}%", (baseEquipment.BaseCrit * 100).ToString("0"));
        defenseLabel.text = string.Format("Defense: {0}", baseEquipment.BaseDefense * currentEquipment.Level);
        reflectionLabel.text = string.Format("Reflection: {0}", baseEquipment.BaseReflection * currentEquipment.Level);
        deflectLabel.text = string.Format("Deflect: {0}%", (baseEquipment.BaseDeflect * 100).ToString("0"));
    }

    private void ToggleStatFusePanels(bool statPanelOpen) {
        statPanel.SetActive(statPanelOpen);
        // Handle fuse panel.
        fuseButtonText.text = statPanelOpen ? "Fuse" : "Stats";
    }

    public void OnFuseButtonPressed() {
        bool openStatPanel = !statPanel.activeSelf;
        ToggleStatFusePanels(openStatPanel);
    }

    public void OnDetailBackPressed() {
        masterPanel.SetActive(true);
        detailPanel.SetActive(false);
    }

    public void OnPreviousPressed() {
        if (currentSelection > 0) {
            currentSelection--;
        }
        BindDetailPanel();
    }

    public void OnNextPressed() {
        if (currentSelection < filteredList.Count - 1) {
            currentSelection++;
        }
        BindDetailPanel();
    }

    #endregion
}

public enum EquipmentFilter {
    ONE_HAND, TWO_HAND, CLOTH, LEATHER, PLATE, CRYSTAL
}

public class MasterEquipmentAdapter: RecyclerViewAdapter {

    private EquipmentSceneManager parent;
    private GameObject listItemPrefab;
    private List<AccountEquipment> filteredList;

    public MasterEquipmentAdapter(EquipmentSceneManager parent, GameObject listItemPrefab) {
        this.parent = parent;
        this.listItemPrefab = listItemPrefab;
    }

    public void SetFilteredList(List<AccountEquipment> filteredList) {
        this.filteredList = filteredList;
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return UnityEngine.Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        viewHolder.GetComponent<EquipmentListItem>()
            .SetEquipment(filteredList[position], true, parent.OnMasterListItemSelected, position);
    }

    public override int GetItemCount() {
        return filteredList == null ? 0 : filteredList.Count;
    }
}
