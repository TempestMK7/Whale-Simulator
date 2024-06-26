using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.StateObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWithHeroSelectPopupBehavior : MonoBehaviour, IHeroSelectionListener {

    public RectTransform popupContainer;
    public RecyclerView recyclerView;
    public Text quantityLabel;
    public UnityEngine.UI.Button plusButton;
    public UnityEngine.UI.Button minusButton;

    public GameObject heroListItemPrefab;

    public int expansionFrames = 12;

    private AccountInventory inventory;
    private TreatHeroAdapter adapter;

    private long selectedQuantity = 1;
    private AccountHero selectedHero;

    public void Awake() {
        popupContainer.localScale = new Vector3(0, 0);
        var stateManager = FindObjectOfType<StateManager>();
        var selectableHeroes = stateManager.CurrentAccountState.AccountHeroes.FindAll(matchable => matchable.CurrentLevel < 100);
        adapter = new TreatHeroAdapter(this, heroListItemPrefab, selectableHeroes);
        recyclerView.SetAdapter(adapter);
        StartCoroutine(ExpandIntoFrame());
    }

    private bool ItemRequiresQuantity(ItemEnum itemType) {
        switch (itemType) {
            case ItemEnum.LITTLE_TREAT:
            case ItemEnum.YUMMY_TREAT:
            case ItemEnum.SIZABLE_TREAT:
            case ItemEnum.FANCY_TREAT:
                return true;
            default:
                return false;
        }
    }

    public void SetTreatInventory(AccountInventory inventory) {
        this.inventory = inventory;
        bool requiresQuantity = ItemRequiresQuantity(inventory.ItemType);
        quantityLabel.enabled = requiresQuantity;
        plusButton.enabled = requiresQuantity;
        minusButton.enabled = requiresQuantity;
        recyclerView.GetComponent<RectTransform>().offsetMin = new Vector2(20, requiresQuantity ? 130 : 90);
        HandleQuantityLabel();
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

    private void HandleQuantityLabel() {
        quantityLabel.text = $"{selectedQuantity} / {inventory.Quantity}";
    }

    public void OnPlusPressed() {
        selectedQuantity += 1;
        if (selectedQuantity > inventory.Quantity) selectedQuantity = 1;
        HandleQuantityLabel();
    }

    public void OnMinusPressed() {
        selectedQuantity -= 1;
        if (selectedQuantity <= 0) selectedQuantity = inventory.Quantity;
        HandleQuantityLabel();
    }

    public void OnGivePressed() {
        if (selectedHero == null) return;
        // TODO: await server response from using hero.
        StartCoroutine(ShrinkToNothing());
    }

    public void OnCancelPressed() {
        StartCoroutine(ShrinkToNothing());
    }

    public void OnHeroSelected(int position, AccountHero hero) {
        selectedHero = hero;
        adapter.SetSelectedHero(hero);
        recyclerView.NotifyDataSetChanged();
    }
}

public class TreatHeroAdapter : RecyclerViewAdapter {

    private IHeroSelectionListener listener;
    private GameObject heroListItemPrefab;
    private List<AccountHero> selectableHeroes;
    private AccountHero selectedHero;

    public TreatHeroAdapter(IHeroSelectionListener listener, GameObject heroListItemPrefab, List<AccountHero> selectableHeroes) {
        this.listener = listener;
        this.heroListItemPrefab = heroListItemPrefab;
        this.selectableHeroes = selectableHeroes;
    }

    public void SetSelectedHero(AccountHero selectedHero) {
        this.selectedHero = selectedHero;
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        var heroListItem = Object.Instantiate(heroListItemPrefab, contentHolder);
        heroListItem.GetComponent<HeroListItemBehavior>().SetHeroSelectionListener(listener);
        return heroListItem;
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var behavior = viewHolder.GetComponent<HeroListItemBehavior>();
        var hero = selectableHeroes[position];
        behavior.SetHero(hero, position);
        behavior.SetSelectionStatus(hero.Id.Equals(selectedHero?.Id), true);
    }

    public override int GetItemCount() {
        return selectableHeroes == null ? 0 : selectableHeroes.Count;
    }
}
