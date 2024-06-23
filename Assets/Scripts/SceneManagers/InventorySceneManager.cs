using Com.Tempest.Whale.StateObjects;
using System.Collections.Generic;
using UnityEngine;

public class InventorySceneManager : MonoBehaviour {

    public RecyclerView inventoryRecycler;

    public GameObject inventoryListItemPrefab;

    private StateManager stateManager;
    private CredentialsManager credentialsManager;
    private InventoryAdapter inventoryAdapter;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
        credentialsManager = FindObjectOfType<CredentialsManager>();
        inventoryAdapter = new InventoryAdapter(stateManager.CurrentAccountState.Inventory, inventoryListItemPrefab, this);
        inventoryRecycler.SetAdapter(inventoryAdapter);
    }

    public void NotifyListSelection(AccountInventory inventory) {
        // TODO: Build a tooltip popup with the message in the base inventory object.
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
        return Object.Instantiate(listItemPrefab, contentHolder);
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var inventoryItem = inventory[position];
        viewHolder.GetComponent<InventoryListItemBehavior>().SetAccountInventory(inventoryItem);
    }

    public override int GetItemCount() {
        return inventory == null ? 0 : inventory.Count;
    }
}
