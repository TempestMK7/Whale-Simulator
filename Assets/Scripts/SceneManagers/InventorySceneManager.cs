using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySceneManager : MonoBehaviour {

    public Canvas mainCanvas;
    public RecyclerView inventoryRecycler;

    public GameObject inventoryListItemPrefab;
    public GameObject tooltipPrefab;
    public GameObject itemHeroSelectPrefab;

    private StateManager stateManager;
    private CredentialsManager credentialsManager;
    private InventoryAdapter inventoryAdapter;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
        credentialsManager = FindObjectOfType<CredentialsManager>();
        inventoryAdapter = new InventoryAdapter(stateManager.CurrentAccountState.Inventory, inventoryListItemPrefab, this);
        inventoryRecycler.SetAdapter(inventoryAdapter);
    }

    public void OnBackPressed() {
        SceneManager.LoadSceneAsync("HubScene");
    }

    public void OnClick(AccountInventory inventory) {
        if (ButtonsBlocked()) return;
        var baseInventory = BaseInventoryContainer.GetBaseInventory(inventory.ItemType);
        var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
        tooltip.GetComponent<TooltipPopup>().SetTooltip(baseInventory.Name, baseInventory.Description);
    }

    public void OnLongPress(AccountInventory inventory) {
        if (ButtonsBlocked()) return;
        switch (inventory.ItemType) {
            case ItemEnum.LITTLE_TREAT:
            case ItemEnum.YUMMY_TREAT:
            case ItemEnum.SIZABLE_TREAT:
            case ItemEnum.FANCY_TREAT:
            case ItemEnum.OLD_BOOK:
            case ItemEnum.ANCIENT_BOOK:
                LaunchHeroSelectionPopup(inventory);
                break;
        }
    }

    private void LaunchHeroSelectionPopup(AccountInventory inventory) {
        var itemHeroSelect = Instantiate(itemHeroSelectPrefab, mainCanvas.transform);
        itemHeroSelect.GetComponent<ItemWithHeroSelectPopupBehavior>().SetTreatInventory(inventory);
    }

    private bool ButtonsBlocked() {
        return FindObjectOfType<TooltipPopup>() != null ||
            FindObjectOfType<LoadingPopup>() != null ||
            FindObjectOfType<ItemWithHeroSelectPopupBehavior>() != null;
    }
}

public class InventoryAdapter : RecyclerViewAdapter {

    private List<AccountInventory> inventory;
    private readonly GameObject listItemPrefab;
    private readonly InventorySceneManager sceneManager;

    public InventoryAdapter(List<AccountInventory> inventory, GameObject listItemPrefab, InventorySceneManager sceneManager) {
        this.inventory = inventory;
        this.listItemPrefab = listItemPrefab;
        this.sceneManager = sceneManager;
    }

    public void SetNewList(List<AccountInventory> newInventory) {
        inventory = newInventory;
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        var listItem = Object.Instantiate(listItemPrefab, contentHolder);
        listItem.GetComponent<InventoryListItemBehavior>().SetSceneManager(sceneManager);
        return listItem;
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var inventoryItem = inventory[position];
        viewHolder.GetComponent<InventoryListItemBehavior>().SetAccountInventory(inventoryItem);
    }

    public override int GetItemCount() {
        return inventory == null ? 0 : inventory.Count;
    }
}
