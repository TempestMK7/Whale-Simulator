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
    public GameObject combineItemPrefab;

    private StateManager stateManager;
    private CredentialsManager credentialsManager;
    private InventoryAdapter inventoryAdapter;

    public void Awake() {
        stateManager = FindObjectOfType<StateManager>();
        credentialsManager = FindObjectOfType<CredentialsManager>();
        inventoryAdapter = new InventoryAdapter(inventoryListItemPrefab, this);
        inventoryAdapter.SetNewList(stateManager.CurrentAccountState.Inventory);
        inventoryRecycler.SetAdapter(inventoryAdapter);
    }

    public void OnBackPressed() {
        SceneManager.LoadSceneAsync("HubScene");
    }

    public void OnClick(AccountInventory inventory) {
        if (ButtonsBlocked()) return;
        var baseInventory = BaseInventoryContainer.GetBaseInventory(inventory.ItemType);
        var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
        tooltip.GetComponent<TooltipPopup>().SetTooltip(baseInventory.NameSingular, baseInventory.Description);
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
            case ItemEnum.OLD_PAGES:
            case ItemEnum.ANCIENT_PAGES:
                var baseInventory = BaseInventoryContainer.GetBaseInventory(inventory.ItemType);
                if (inventory.Quantity < baseInventory.QuantityRequired) {
                    var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
                    tooltip.GetComponent<TooltipPopup>().SetTooltip("Not Enough", $"You do not have the required {baseInventory.QuantityRequired} {baseInventory.NamePlural}.");
                } else {
                    LaunchCombineItemPopup(inventory);
                }
                break;
        }
    }

    private void LaunchHeroSelectionPopup(AccountInventory inventory) {
        var itemHeroSelect = Instantiate(itemHeroSelectPrefab, mainCanvas.transform);
        itemHeroSelect.GetComponent<ItemWithHeroSelectPopup>().SetTreatInventory(inventory);
    }

    private void LaunchCombineItemPopup(AccountInventory inventory) {
        var combinePopup = Instantiate(combineItemPrefab, mainCanvas.transform);
        combinePopup.GetComponent<CombineItemPopup>().SetSelectedInventory(inventory);
    }

    public async void OnGivePressed(AccountInventory inventory, AccountHero targetHero, long quantity) {
        var response = await credentialsManager.UseItem(inventory, targetHero, quantity);
        if (response != null && response.Success) {
            inventoryAdapter.SetNewList(stateManager.CurrentAccountState.Inventory);
            inventoryRecycler.NotifyDataSetChanged();
            var heroName = BaseHeroContainer.GetBaseHero(targetHero.HeroType).HeroName;

            string successMessage;
            if (response.NewAttack != null) {
                var attackName = AttackInfoContainer.GetAttackInfo((AttackEnum)response.NewAttack).AttackName;
                successMessage = $"{heroName} learned {attackName}.";
            } else {
                var experienceTotal = BaseExperience(response.UsedInventory.ItemType) * quantity;
                successMessage = $"{heroName} gained {experienceTotal} experience and is now level {response.TargetHero.CurrentLevel}.";
            }

            var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
            tooltip.GetComponent<TooltipPopup>().SetTooltip("Success!", successMessage);
        } else {
            var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
            tooltip.GetComponent<TooltipPopup>().SetTooltip("Can't Do That", "Your item could not be used in this way.");
        }
    }

    public async void OnCombinePressed(AccountInventory inventory) {
        var response = await credentialsManager.UseItem(inventory, null, 100);
        if (response != null && response.Success ) {
            inventoryAdapter.SetNewList(stateManager.CurrentAccountState.Inventory);
            inventoryRecycler.NotifyDataSetChanged();
            var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
            var resultBaseInventory = BaseInventoryContainer.GetBaseInventory(response.ResultInventory.ItemType);
            var resultName = response.ResultInventory.Quantity == 1 ? resultBaseInventory.NameSingular : resultBaseInventory.NamePlural;
            tooltip.GetComponent<TooltipPopup>().SetTooltip("Success!", $"You now have {response.ResultInventory.Quantity} {resultName}.");
        } else {
            var tooltip = Instantiate(tooltipPrefab, mainCanvas.transform);
            tooltip.GetComponent<TooltipPopup>().SetTooltip("Can't Do That", "Your item could not be used in this way.");
        }
    }

    private long BaseExperience(ItemEnum treatType) {
        switch (treatType) {
            case ItemEnum.LITTLE_TREAT:
                return 100;
            case ItemEnum.YUMMY_TREAT:
                return 1000;
            case ItemEnum.SIZABLE_TREAT:
                return 10000;
            case ItemEnum.FANCY_FOOD:
                return 100000;
            default:
                return 0;
        }
    }

    private bool ButtonsBlocked() {
        return FindObjectOfType<TooltipPopup>() != null ||
            FindObjectOfType<LoadingPopup>() != null ||
            FindObjectOfType<ItemWithHeroSelectPopup>() != null;
    }
}

public class InventoryAdapter : RecyclerViewAdapter {

    private List<AccountInventory> inventory;
    private readonly GameObject listItemPrefab;
    private readonly InventorySceneManager sceneManager;

    public InventoryAdapter(GameObject listItemPrefab, InventorySceneManager sceneManager) {
        this.listItemPrefab = listItemPrefab;
        this.sceneManager = sceneManager;
    }

    public void SetNewList(List<AccountInventory> newInventory) {
        inventory = newInventory.FindAll(matchable => matchable.Quantity > 0);
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
