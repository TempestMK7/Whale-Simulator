using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionPopupBehavior : MonoBehaviour {

    private FactionEnum faction;
    private int level;
    private HeroEnum? filteredHero;
    private List<AccountHero> alreadySelected;
    private FusionSelectionBehavior summoner;

    public void Awake() {
        transform.localScale = new Vector3(0f, 0f);
        BuildList();
    }

    private void BuildList() {

    }

    public void OnCancelPressed() {
        summoner.SetEmpty();
        StartCoroutine("ShrinkToNothing");
    }

    public void OnFusionListItemPressed(AccountHero hero) {
        summoner.SetAccountHero(hero);
        StartCoroutine("ShrinkToNothing");
    }

    public void LaunchPopup(FactionEnum faction, int level, HeroEnum? filteredHero, List<AccountHero> alreadySelected, FusionSelectionBehavior summoner) {
        this.faction = faction;
        this.level = level;
        this.filteredHero = filteredHero;
        this.alreadySelected = alreadySelected;
        this.summoner = summoner;

        StartCoroutine("ExpandIntoFrame");
    }

    IEnumerator ExpandIntoFrame() {
        float duration = 12f;
        for (float frame = 0f; frame <= duration; frame++) {
            float ratio = frame / duration;
            transform.localScale = new Vector3(ratio, ratio);
            yield return null;
        }
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
