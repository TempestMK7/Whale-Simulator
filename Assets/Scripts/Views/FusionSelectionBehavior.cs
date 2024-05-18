using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.GameObjects;
using Com.Tempest.Whale.ResourceContainers;
using Com.Tempest.Whale.StateObjects;

public class FusionSelectionBehavior : MonoBehaviour {

    private const string addIcon = "Icons/add_circle";

    public Image blurryBorder;
    public Image heroIcon;
    public RarityBehavior rarityView;
    public Text levelText;

    public bool isCenter = false;

    private Sprite addSprite;
    private HeroSceneManager parentManager;
    private AccountHero selectedHero;
    private FactionEnum? requiredFaction;
    private int requiredLevel;
    private HeroEnum? requiredHero;

    public void Awake() {
        addSprite = Resources.Load<Sprite>(addIcon);
        parentManager = FindObjectOfType<HeroSceneManager>();
    }

    public void SetCardRequirements(FactionEnum? faction, int level, HeroEnum? hero) {
        requiredFaction = faction;
        requiredLevel = level;
        requiredHero = hero;
    }

    public void SetAccountHero(AccountHero hero) {
        selectedHero = hero;
        blurryBorder.color = ColorContainer.ColorFromFaction(hero.GetBaseHero().Faction);
        heroIcon.sprite = Resources.Load<Sprite>(hero.GetBaseHero().HeroIconPath);
        heroIcon.color = new Color(1, 1, 1, 1);
        rarityView.SetLevel(hero.GetBaseHero().Rarity, hero.AwakeningLevel, false);
        levelText.text = hero.CurrentLevel.ToString();
    }

    public void SetEmpty() {
        selectedHero = null;
        if (requiredFaction != null) {
            blurryBorder.enabled = true;
            blurryBorder.color = ColorContainer.ColorFromFaction(requiredFaction ?? FactionEnum.WATER);
        } else {
            blurryBorder.enabled = false;
        }
        if (requiredHero == null) {
            heroIcon.sprite = addSprite;
            heroIcon.color = new Color(1, 1, 1, 1);
        } else {
            var baseHero = BaseHeroContainer.GetBaseHero(requiredHero ?? HeroEnum.LEPAQUA);
            heroIcon.sprite = Resources.Load<Sprite>(baseHero.HeroIconPath);
            heroIcon.color = new Color(1, 1, 1, 0.5f);
        }
        rarityView.SetLevel(0, requiredLevel, false);
        levelText.text = "";
    }

    public AccountHero GetSelectedHero() {
        return selectedHero;
    }

    public void OnButtonClicked() {
        if (isCenter) return;
        parentManager.RequestPopup(this, requiredFaction, requiredLevel, requiredHero);
    }
}
