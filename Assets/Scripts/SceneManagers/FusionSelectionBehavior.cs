using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private FactionEnum requiredFaction;
    private int requiredLevel;
    private HeroEnum? requiredHero;

    public void Awake() {
        addSprite = Resources.Load<Sprite>(addIcon);
        parentManager = FindObjectOfType<HeroSceneManager>();
    }

    public void SetEmpty(FactionEnum faction, int levelRequirement, HeroEnum? heroRequirement) {
        requiredFaction = faction;
        requiredLevel = levelRequirement;
        requiredHero = heroRequirement;

        blurryBorder.color = ColorContainer.ColorFromFaction(faction);
        heroIcon.sprite = addSprite;
        rarityView.SetLevel(0, levelRequirement, false);
        levelText.text = "";
    }

    public AccountHero GetSelectedHero() {
        return selectedHero;
    }

    public void SetAccountHero(AccountHero hero) {
        blurryBorder.color = ColorContainer.ColorFromFaction(hero.GetBaseHero().Faction);
        heroIcon.sprite = (hero.GetBaseHero().HeroIcon);
        rarityView.SetLevel(hero.GetBaseHero().Rarity, hero.AwakeningLevel, false);
        levelText.text = hero.CurrentLevel.ToString();
    }

    public void OnButtonClicked() {
        if (isCenter) return;
        parentManager.RequestPopup(this, requiredFaction, requiredLevel, requiredHero);
    }
}
