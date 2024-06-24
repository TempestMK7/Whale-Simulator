using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryListItemBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public Image itemIcon;
    public Text quantityLabel;

    private AccountInventory accountInventory;
    private InventorySceneManager inventorySceneManager;

    private Coroutine longPressCoroutine;
    private bool pointerDown = false;

    public void SetSceneManager(InventorySceneManager inventorySceneManager) {
        this.inventorySceneManager = inventorySceneManager;
    }

    public void SetAccountInventory(AccountInventory accountInventory) {
        this.accountInventory = accountInventory;
        var baseInventory = BaseInventoryContainer.GetBaseInventory(accountInventory.ItemType);

        quantityLabel.text = CustomFormatter.Format(accountInventory.Quantity);
        itemIcon.sprite = Resources.Load<Sprite>(baseInventory.IconPath);
    }

    public void OnPointerDown(PointerEventData eventData) {
        pointerDown = true;
        longPressCoroutine = StartCoroutine(StartLongPressTest());
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (pointerDown) {
            StopCoroutine(longPressCoroutine);
            DoClick();
        }
        pointerDown= false;
    }

    private System.Collections.IEnumerator StartLongPressTest() {
        yield return new WaitForSeconds(0.5f);
        if (pointerDown) {
            pointerDown = false;
            DoLongPress();
        }
    }

    private void DoClick() {
        inventorySceneManager.OnClick(accountInventory);
    }

    private void DoLongPress() {
        inventorySceneManager.OnLongPress(accountInventory);
    }
}
