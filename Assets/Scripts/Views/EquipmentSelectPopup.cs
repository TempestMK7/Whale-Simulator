﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        adapter = new EquipmentAdapter(this, listItemPrefab, selectedHero.HeroGuid, selectedSlot);
        StartCoroutine(ExpandIntoFrame());
    }

    public void OnCancelPressed() {
        StartCoroutine(ShrinkToNothing());
    }

    public void OnClearPressed() {
        ClearCurrentSelection();
        var sceneManager = FindObjectOfType<HeroSceneManager>();
        if (sceneManager != null) sceneManager.NotifyEquipmentSelected();
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

    public void ClearCurrentSelection() {
        var equipmentList = StateManager.GetCurrentState().AccountEquipment;
        var matchingSlot = new List<EquipmentSlot>();
        matchingSlot.Add(selectedSlot);
        switch (selectedSlot) {
            case EquipmentSlot.MAIN_HAND:
            case EquipmentSlot.OFF_HAND:
                matchingSlot.Add(EquipmentSlot.TWO_HAND);
                break;
        }
        var selected = equipmentList.FindAll((AccountEquipment matchable) => {
            return selectedHero.HeroGuid.Equals(matchable.EquippedHeroGuid) && matchingSlot.Contains(matchable.EquippedSlot.GetValueOrDefault());
        });
        foreach (AccountEquipment equip in selected) {
            equip.EquippedHeroGuid = null;
            equip.EquippedSlot = null;
        }
    }
}

public class EquipmentAdapter : RecyclerViewAdapter {

    private EquipmentSelectPopup popup;
    private GameObject listItemPrefab;
    private Guid selectedHeroGuid;
    private EquipmentSlot selectedSlot;
    private List<AccountEquipment> equipmentList;

    public EquipmentAdapter(EquipmentSelectPopup popup, GameObject listItemPrefab, Guid selectedHeroGuid, EquipmentSlot selectedSlot) {
        this.popup = popup;
        this.listItemPrefab = listItemPrefab;
        this.selectedHeroGuid = selectedHeroGuid;
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
        equipmentList = StateManager.GetCurrentState().AccountEquipment.FindAll((AccountEquipment matchable) => {
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
        popup.ClearCurrentSelection();
        var selection = equipmentList[position];
        selection.EquippedHeroGuid = selectedHeroGuid;
        selection.EquippedSlot = selection.GetBaseEquipment().Slot == EquipmentSlot.TWO_HAND ? EquipmentSlot.TWO_HAND : selectedSlot;
        var sceneManager = UnityEngine.Object.FindObjectOfType<HeroSceneManager>();
        if (sceneManager != null) sceneManager.NotifyEquipmentSelected();
        popup.OnCancelPressed();
    }
}