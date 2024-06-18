using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;
using Com.Tempest.Whale.Combat;

public class EquipmentSceneManager : MonoBehaviour {

    public Canvas mainCanvas;

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
    public FusionFanfareBehavior fusionFanfare;

    public RarityBehavior equipmentRarityView;
    public Text fuseButtonText;

    public GameObject fusePanel;
    public EquipmentFusionButton centerFusionButton;
    public EquipmentFusionButton bottomLeftFusionButton;
    public EquipmentFusionButton bottomRightFusionButton;
    public UnityEngine.UI.Button suggestedFusionButton;
    public UnityEngine.UI.Button completeFusionButton;
    public GameObject fusionPopupPrefab;

    public GameObject statPanel;
    public Text primaryLabel;
    public Text secondaryLabel;
    public Text tertiaryLabel;
    public Text explanationLabel;

    private CredentialsManager credentialsManager;
    private StateManager stateManager;
    private bool loadingFromServer = false;

    // Master panel things.
    private EquipmentSlot? currentFilter;
    private List<AccountEquipment> filteredList;
    private MasterEquipmentAdapter masterAdapter;

    // Detail panel things.
    private int currentSelection;
    private bool fanfarePlaying = false;

    public void Awake() {
        credentialsManager = FindObjectOfType<CredentialsManager>();
        stateManager = FindObjectOfType<StateManager>();

        masterPanel.SetActive(true);
        detailPanel.SetActive(false);

        masterAdapter = new MasterEquipmentAdapter(this, masterListItemPrefab);
        masterEquipmentRecycler.SetAdapter(masterAdapter);
        FilterList();
    }

    #region Master panel methods.

    public void OnFilterPressed(int position) {
        var allFilters = (EquipmentSlot[])Enum.GetValues(typeof(EquipmentSlot));
        var newFilter = allFilters[position];
        if (newFilter == currentFilter) {
            currentFilter = null;
        } else {
            currentFilter = newFilter;
        }
        FilterList();
    }

    private void FilterList() {
        var unfiltered = new List<AccountEquipment>(stateManager.CurrentAccountState.AccountEquipment);
        filteredList = unfiltered;
        if (currentFilter == null) {
            filteredList = unfiltered;
        } else {
            filteredList = unfiltered.FindAll((AccountEquipment matchable) => {
                return matchable.Slot == currentFilter;
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
        var currentEquipment = filteredList[currentSelection];

        equipmentLabel.text = BaseEquipmentContainer.GetEquipmentName(currentEquipment);
        equipmentPositionLabel.text = string.Format("({0} of {1})", currentSelection + 1, filteredList.Count);
        if (currentEquipment.EquippedHeroId == null) {
            equipmentOwnerIcon.enabled = false;
        } else {
            equipmentOwnerIcon.enabled = true;
            var equippedHero = stateManager.CurrentAccountState.AccountHeroes.Find((AccountHero hero) => {
                return hero.Id.Equals(currentEquipment.EquippedHeroId);
            });
            if (equippedHero != null) equipmentOwnerIcon.sprite = Resources.Load<Sprite>(equippedHero.GetBaseHero().HeroIconPath);
        }

        equipmentDisplay.sprite = Resources.Load<Sprite>(BaseEquipmentContainer.GetEquipmentIcon(currentEquipment.Slot, currentEquipment.IconIndex));

        equipmentRarityView.SetLevel(0, currentEquipment.Level, true);
        var primaryFormatted = FormatStat(currentEquipment.PrimaryStat, CombatHero.CalculateStatFromEquipment(currentEquipment.PrimaryStat, currentEquipment.PrimaryQuality, currentEquipment.Level));
        primaryLabel.text = $"{BaseEquipmentContainer.GetStatName(currentEquipment.PrimaryStat)}: {primaryFormatted}";
        var secondaryFormatted = FormatStat(currentEquipment.SecondaryStat, CombatHero.CalculateStatFromEquipment(currentEquipment.SecondaryStat, currentEquipment.SecondaryQuality, currentEquipment.Level));
        secondaryLabel.text = $"{BaseEquipmentContainer.GetStatName(currentEquipment.SecondaryStat)}: {secondaryFormatted}";
        var tertiaryFormatted = FormatStat(currentEquipment.TertiaryStat, CombatHero.CalculateStatFromEquipment(currentEquipment.TertiaryStat, currentEquipment.TertiaryQuality, currentEquipment.Level));
        tertiaryLabel.text = $"{BaseEquipmentContainer.GetStatName(currentEquipment.TertiaryStat)}: {tertiaryFormatted}";
        explanationLabel.text = BaseEquipmentContainer.GetStatDescription(currentEquipment.TertiaryStat);
        ToggleStatFusePanels(true);
    }

    private string FormatStat(EquipmentStat stat, double amount) {
        switch (stat) {
            case EquipmentStat.STRENGTH:
            case EquipmentStat.POWER:
            case EquipmentStat.TOUGHNESS:
            case EquipmentStat.RESISTANCE:
            case EquipmentStat.HEALTH:
            case EquipmentStat.SPEED:
            case EquipmentStat.VIGOR:
                return amount.ToString("##0.#");
            default:
                return (amount * 100.0).ToString("##0.#") + "%";
        }
    }

    private void ResetListPosition() {
        var selected = filteredList[currentSelection].Id;
        FilterList();
        currentSelection = filteredList.FindIndex((AccountEquipment equipment) => {
            return equipment.Id.Equals(selected);
        });
    }

    private bool ButtonsBlocked() {
        return fanfarePlaying || loadingFromServer;
    }

    public void OnFuseButtonPressed() {
        if (ButtonsBlocked()) return;
        bool openStatPanel = !statPanel.activeSelf;
        ToggleStatFusePanels(openStatPanel);
    }

    public void OnDetailBackPressed() {
        if (ButtonsBlocked()) return;
        masterPanel.SetActive(true);
        detailPanel.SetActive(false);
    }

    public void OnPreviousPressed() {
        if (ButtonsBlocked()) return;
        if (currentSelection > 0) {
            currentSelection--;
        }
        BindDetailPanel();
    }

    public void OnNextPressed() {
        if (ButtonsBlocked()) return;
        if (currentSelection < filteredList.Count - 1) {
            currentSelection++;
        }
        BindDetailPanel();
    }

    private void ToggleStatFusePanels(bool statPanelOpen) {
        statPanel.SetActive(statPanelOpen);
        fusePanel.SetActive(!statPanelOpen);
        fuseButtonText.text = statPanelOpen ? "Fuse" : "Stats";

        if (!statPanelOpen) SetupFusePanel();
    }

    private void SetupFusePanel() {
        var accountEquipment = filteredList[currentSelection];
        if (accountEquipment.Level >= 10) {
            ToggleStatFusePanels(true);
            return;
        }
        centerFusionButton.SetAccountEquipment(accountEquipment);

        bottomLeftFusionButton.SetCardRequirements(accountEquipment.Slot, accountEquipment.Level);
        bottomLeftFusionButton.SetEmpty();
        bottomRightFusionButton.SetCardRequirements(accountEquipment.Slot, accountEquipment.Level);
        bottomRightFusionButton.SetEmpty();

        HandleCompleteFusionButton();
    }

    private List<AccountEquipment> GetSelectedFusionEquipment() {
        var selected = new List<AccountEquipment>();
        if (bottomLeftFusionButton.GetSelectedEquipment() != null) selected.Add(bottomLeftFusionButton.GetSelectedEquipment());
        if (bottomRightFusionButton.GetSelectedEquipment() != null) selected.Add(bottomRightFusionButton.GetSelectedEquipment());
        return selected;
    }

    public void RequestFusionPopup(EquipmentFusionButton caller, EquipmentSlot slot, int level) {
        if (ButtonsBlocked()) return;
        var alreadySelected = GetSelectedFusionEquipment();
        alreadySelected.Add(centerFusionButton.GetSelectedEquipment());
        var selected = caller.GetSelectedEquipment();
        if (selected != null && alreadySelected.Contains(selected)) alreadySelected.Remove(selected);

        var popup = Instantiate(fusionPopupPrefab, detailPanel.transform).GetComponent<EquipmentFusionPopup>();
        popup.LaunchPopup(slot, level, alreadySelected, caller);
    }

    public void OnFusionEquipmentSelected() {
        HandleCompleteFusionButton();
    }

    public void HandleCompleteFusionButton() {
        var selected = filteredList[currentSelection];
        var destroyedEquipment = GetSelectedFusionEquipment();
        completeFusionButton.gameObject.SetActive(LevelContainer.FusionIsLegal(selected, destroyedEquipment));
    }

    public async void RequestFusion() {
        if (ButtonsBlocked()) return;
        var selected = filteredList[currentSelection];
        var destroyedEquipment = GetSelectedFusionEquipment();
        if (!LevelContainer.FusionIsLegal(selected, destroyedEquipment)) return;

        loadingFromServer = true;
        try {
            var successful = await credentialsManager.RequestFusion(selected, destroyedEquipment);
            loadingFromServer = false;
            OnFusionComplete(successful);
        } catch (Exception e) {
            Debug.LogError(e);
            loadingFromServer = false;
            CredentialsManager.DisplayNetworkError(mainCanvas, "There was an error with the equipment fusion.");
        }
    }

    public void OnFusionComplete(bool successful) {
        if (!successful) return;
        ResetListPosition();
        BindDetailPanel();
        StartCoroutine(PlayFanfare());
    }

    public void OnSuggestFusion() {
        if (ButtonsBlocked()) return;
        SetupFusePanel();
        var alreadySelected = new List<AccountEquipment> {
            filteredList[currentSelection]
        };
        SelectSameEquipment(bottomLeftFusionButton, alreadySelected);
        SelectSameEquipment(bottomRightFusionButton, alreadySelected);
        HandleCompleteFusionButton();
    }

    private void SelectSameEquipment(EquipmentFusionButton fusion, List<AccountEquipment> alreadySelected) {
        var accountEquipment = filteredList[currentSelection];
        var allEquipment = stateManager.CurrentAccountState.AccountEquipment;
        var firstSelectable = allEquipment.Find((AccountEquipment equipment) => {
            return !alreadySelected.Contains(equipment) && equipment.Level == accountEquipment.Level && equipment.Slot == accountEquipment.Slot;
        });
        if (firstSelectable != null) {
            fusion.SetAccountEquipment(firstSelectable);
            alreadySelected.Add(firstSelectable);
        }
    }

    private IEnumerator PlayFanfare() {
        fanfarePlaying = true;
        fusionFanfare.Play();
        yield return new WaitForSeconds(2f);
        fanfarePlaying = false;
    }

    #endregion
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
