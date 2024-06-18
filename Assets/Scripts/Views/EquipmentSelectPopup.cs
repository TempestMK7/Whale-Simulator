using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

public class EquipmentSelectPopup : MonoBehaviour {

    public Text equipmentLabel;
    public RecyclerView equipmentRecycler;

    public GameObject listItemPrefab;
    public int animationFrames = 12;

    private AccountHero selectedHero;
    private EquipmentSlot selectedSlot;
    private EquipmentAdapter adapter;

    public void Awake() {
        transform.localScale = new Vector3();
    }

    public void SetHeroAndSlot(AccountHero selectedHero, EquipmentSlot selectedSlot) {
        this.selectedHero = selectedHero;
        this.selectedSlot = selectedSlot;

        string title;
        switch (selectedSlot) {
            case EquipmentSlot.HEAD:
                title = "Head";
                break;
            case EquipmentSlot.NECK:
                title = "Neck";
                break;
            case EquipmentSlot.BELT:
                title = "Belt";
                break;
            default:
                title = "None";
                break;
        }
        equipmentLabel.text = string.Format("Equip {0}", title);

        adapter = new EquipmentAdapter(FindObjectOfType<StateManager>(), this, listItemPrefab, selectedHero, selectedSlot);
        StartCoroutine(ExpandIntoFrame());
    }

    public void OnCancelPressed() {
        StartCoroutine(ShrinkToNothing());
    }

    public void OnClearPressed() {
        var sceneManager = FindObjectOfType<HeroSceneManager>();
        if (sceneManager != null) sceneManager.NotifyEquipmentSelected(GetCurrentSelection(), null);
        StartCoroutine(ShrinkToNothing());
    }

    private IEnumerator ExpandIntoFrame() {
        for (float x = 1; x <= animationFrames; x++) {
            var percentage = x / animationFrames;
            transform.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        equipmentRecycler.SetAdapter(adapter);
        equipmentRecycler.NotifyDataSetChanged();
    }

    private IEnumerator ShrinkToNothing() {
        for (float x = animationFrames; x >= 0; x--) {
            var percentage = x / animationFrames;
            transform.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        Destroy(gameObject);
    }

    public AccountEquipment GetCurrentSelection() {
        var equipmentList = FindObjectOfType<StateManager>().CurrentAccountState.AccountEquipment;
        return equipmentList.Find((AccountEquipment matchable) => {
            return selectedHero.Id.Equals(matchable.EquippedHeroId) && matchable.Slot == selectedSlot;
        });
    }
}

public class EquipmentAdapter : RecyclerViewAdapter {

    private readonly EquipmentSelectPopup popup;
    private readonly GameObject listItemPrefab;
    private readonly AccountHero selectedHero;
    private readonly List<AccountEquipment> equipmentList;

    public EquipmentAdapter(StateManager stateManager, EquipmentSelectPopup popup, GameObject listItemPrefab, AccountHero selectedHero, EquipmentSlot selectedSlot) {
        this.popup = popup;
        this.listItemPrefab = listItemPrefab;
        this.selectedHero = selectedHero;
        
        equipmentList = stateManager.CurrentAccountState.AccountEquipment.FindAll((AccountEquipment matchable) => {
            return selectedSlot == matchable.Slot;
        });
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return UnityEngine.Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var listItem = viewHolder.GetComponent<EquipmentListItem>();
        listItem.SetEquipment(equipmentList[position], true, OnViewHolderClicked, position);
    }

    public override int GetItemCount() {
        return equipmentList == null ? 0 : equipmentList.Count;
    }

    public void OnViewHolderClicked(int position) {
        var selection = equipmentList[position];
        var sceneManager = UnityEngine.Object.FindObjectOfType<HeroSceneManager>();
        if (sceneManager != null) sceneManager.NotifyEquipmentSelected(selection, selectedHero);
        popup.OnCancelPressed();
    }
}
