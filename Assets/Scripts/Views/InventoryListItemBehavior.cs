using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryListItemBehavior : MonoBehaviour, IPointerClickHandler {

    public Image itemIcon;
    public Text quantityLabel;

    private AccountInventory accountInventory;
    private InventorySceneManager inventorySceneManager;

    public void SetSceneManager(InventorySceneManager inventorySceneManager) {
        this.inventorySceneManager = inventorySceneManager;
    }

    public void SetAccountInventory(AccountInventory accountInventory) {
        this.accountInventory = accountInventory;
        var baseInventory = BaseInventoryContainer.GetBaseInventory(accountInventory.ItemType);
        quantityLabel.text = baseInventory.name;
        itemIcon.sprite = Resources.Load<Sprite>(baseInventory.iconName);
    }

    public void OnPointerClick(PointerEventData eventData) {
        inventorySceneManager.NotifyListSelection(accountInventory);
    }
}
