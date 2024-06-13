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
            case EquipmentSlot.MAIN_HAND:
                title = "Main Hand";
                break;
            case EquipmentSlot.OFF_HAND:
                title = "Off Hand";
                break;
            case EquipmentSlot.CHEST:
                title = "Chest";
                break;
            case EquipmentSlot.LEGS:
                title = "Legs";
                break;
            case EquipmentSlot.HEAD:
                title = "Head";
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
        if (sceneManager != null) sceneManager.NotifyEquipmentSelected(GetCurrentSelection(), null, null);
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
        var matchingSlot = new List<EquipmentSlot>();
        matchingSlot.Add(selectedSlot);
        switch (selectedSlot) {
            case EquipmentSlot.MAIN_HAND:
            case EquipmentSlot.OFF_HAND:
                matchingSlot.Add(EquipmentSlot.TWO_HAND);
                break;
        }
        return equipmentList.Find((AccountEquipment matchable) => {
            return selectedHero.Id.Equals(matchable.EquippedHeroId) && matchingSlot.Contains(matchable.EquippedSlot.GetValueOrDefault());
        });
    }
}

public class EquipmentAdapter : RecyclerViewAdapter {

    private readonly StateManager stateManager;
    private readonly EquipmentSelectPopup popup;
    private readonly GameObject listItemPrefab;
    private readonly AccountHero selectedHero;
    private readonly EquipmentSlot selectedSlot;
    private readonly List<AccountEquipment> equipmentList;

    public EquipmentAdapter(StateManager stateManager, EquipmentSelectPopup popup, GameObject listItemPrefab, AccountHero selectedHero, EquipmentSlot selectedSlot) {
        this.stateManager = stateManager;
        this.popup = popup;
        this.listItemPrefab = listItemPrefab;
        this.selectedHero = selectedHero;
        this.selectedSlot = selectedSlot;
        var allowedSlots = new List<EquipmentSlot>();
        switch (selectedSlot) {
            case EquipmentSlot.HEAD:
            case EquipmentSlot.CHEST:
            case EquipmentSlot.LEGS:
                allowedSlots.Add(selectedSlot);
                break;
            case EquipmentSlot.MAIN_HAND:
                allowedSlots.Add(EquipmentSlot.MAIN_HAND);
                allowedSlots.Add(EquipmentSlot.ONE_HAND);
                allowedSlots.Add(EquipmentSlot.TWO_HAND);
                break;
            case EquipmentSlot.OFF_HAND:
                allowedSlots.Add(EquipmentSlot.OFF_HAND);
                allowedSlots.Add(EquipmentSlot.ONE_HAND);
                allowedSlots.Add(EquipmentSlot.TWO_HAND);
                break;
        }
        
        equipmentList = stateManager.CurrentAccountState.AccountEquipment.FindAll((AccountEquipment matchable) => {
            return allowedSlots.Contains(matchable.GetBaseEquipment().Slot);
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
        var slot = selection.GetBaseEquipment().Slot == EquipmentSlot.TWO_HAND ? EquipmentSlot.TWO_HAND : selectedSlot;
        if (sceneManager != null) sceneManager.NotifyEquipmentSelected(selection, selectedHero, slot);
        popup.OnCancelPressed();
    }
}
