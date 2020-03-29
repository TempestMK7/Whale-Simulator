using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;

public class EquipmentFusionPopup : MonoBehaviour {

    public RecyclerView recyclerView;
    public GameObject listItemPrefab;

    private EquipmentType type;
    private int level;
    private List<AccountEquipment> alreadySelected;
    private EquipmentFusionButton summoner;
    private EquipmentSceneManager sceneManager;

    private EquipmentFusionAdapter adapter;
    private List<AccountEquipment> displayedList;

    public void Awake() {
        transform.localScale = new Vector3();
        sceneManager = FindObjectOfType<EquipmentSceneManager>();
        adapter = new EquipmentFusionAdapter(listItemPrefab, this);
        recyclerView.SetAdapter(adapter);
    }

    public void BuildList() {
        var allEquipment = StateManager.GetCurrentState().AccountEquipment;
        displayedList = allEquipment.FindAll((AccountEquipment equipment) => {
            var baseEquipment = equipment.GetBaseEquipment();
            var matchesRequirements = baseEquipment.Type == type && equipment.Level == level;
            return matchesRequirements && !alreadySelected.Contains(equipment);
        });
        adapter.SetList(displayedList);
        recyclerView.NotifyDataSetChanged();
    }

    public void OnCancelPressed() {
        summoner.SetEmpty();
        if (sceneManager != null) sceneManager.OnFusionEquipmentSelected();
        StartCoroutine(ShrinkToNothing());
    }

    public void OnFusionListItemPressed(int position) {
        var selected = displayedList[position];
        summoner.SetAccountEquipment(selected);
        if (sceneManager != null) sceneManager.OnFusionEquipmentSelected();
        StartCoroutine(ShrinkToNothing());
    }

    public void LaunchPopup(EquipmentType type, int level, List<AccountEquipment> alreadySelected, EquipmentFusionButton summoner) {
        this.type = type;
        this.level = level;
        this.alreadySelected = alreadySelected;
        this.summoner = summoner;
        StartCoroutine(ExpandIntoFrame());
    }

    IEnumerator ExpandIntoFrame() {
        float duration = 12f;
        for (float frame = 0f; frame <= duration; frame++) {
            float ratio = frame / duration;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
        BuildList();
    }

    IEnumerator ShrinkToNothing() {
        float duration = 12f;
        for (float frame = duration; frame >= 0; frame--) {
            float ratio = frame / duration;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
        Destroy(gameObject);
    }
}

public class EquipmentFusionAdapter: RecyclerViewAdapter {

    private GameObject listItemPrefab;
    private EquipmentFusionPopup parent;
    private List<AccountEquipment> equipment;

    public EquipmentFusionAdapter(GameObject listItemPrefab, EquipmentFusionPopup parent) {
        this.listItemPrefab = listItemPrefab;
        this.parent = parent;
    }

    public void SetList(List<AccountEquipment> equipment) {
        this.equipment = equipment;
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var behavior = viewHolder.GetComponent<EquipmentListItem>();
        behavior.SetEquipment(equipment[position], true, parent.OnFusionListItemPressed, position);
    }

    public override int GetItemCount() {
        return equipment == null ? 0 : equipment.Count;
    }
}
