using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombineItemPopup : MonoBehaviour {

    public RectTransform popupContainer;
    public Text topLabel;
    public Text messageLabel;

    public int expansionFrames = 12;

    private AccountInventory selectedInventory;

    public void Awake() {
        popupContainer.localScale = new Vector3(0, 0);
        StartCoroutine(ExpandIntoFrame());
    }

    public void SetSelectedInventory(AccountInventory inventory) {
        selectedInventory = inventory;
        ItemEnum resultingItem;
        switch (inventory.ItemType) {
            case ItemEnum.OLD_PAGES:
                resultingItem = ItemEnum.OLD_BOOK;
                break;
            case ItemEnum.ANCIENT_PAGES:
                resultingItem = ItemEnum.ANCIENT_BOOK;
                break;
            default:
                return;
        }
        int requiredQuantity = BaseInventoryContainer.GetBaseInventory(inventory.ItemType).QuantityRequired;
        var selectedBaseItem = BaseInventoryContainer.GetBaseInventory(inventory.ItemType);
        var resultingBaseItem = BaseInventoryContainer.GetBaseInventory(resultingItem);
        topLabel.text = $"Combine {selectedBaseItem.NameSingular}";
        messageLabel.text = $"Would you like to combine {requiredQuantity} {selectedBaseItem.NamePlural} into 1 {resultingBaseItem.NameSingular}?";
    }

    private IEnumerator ExpandIntoFrame() {
        for (float x = 1f; x <= expansionFrames; x++) {
            float percentage = x / expansionFrames;
            popupContainer.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
    }

    private IEnumerator ShrinkToNothing() {
        for (float x = expansionFrames; x >= 0; x--) {
            float percentage = x / expansionFrames;
            popupContainer.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void OnYesPressed() {
        FindObjectOfType<InventorySceneManager>().OnCombinePressed(selectedInventory);
        StartCoroutine(ShrinkToNothing());
    }

    public void OnNoPressed() {
        StartCoroutine(ShrinkToNothing());
    }
}
