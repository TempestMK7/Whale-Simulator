using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Tempest.Whale.Combat;
using Com.Tempest.Whale.GameObjects;

public class CombatGraphPopup : MonoBehaviour {

    public int animationFrames = 12;
    public GraphEntryView entryPrefab;

    public RecyclerView graphRecycler;

    private CombatReport report;
    private CombatGraphAdapter adapter;

    public void Awake() {
        transform.localScale = new Vector3();
        adapter = new CombatGraphAdapter(entryPrefab);
        graphRecycler.SetAdapter(adapter);
    }

    public void LaunchPopup(CombatReport report) {
        this.report = report;
        StartCoroutine(ExpandIntoFrame());
    }

    public void OnDonePressed() {
        StartCoroutine(ShrinkToNothing());
    }

    private IEnumerator ExpandIntoFrame() {
        for (float x = 1f; x <= animationFrames; x++) {
            float percentage = x / animationFrames;
            transform.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        BindRecycler();
    }

    private void BindRecycler() {
        adapter.SwapReport(report);
        graphRecycler.NotifyDataSetChanged();
    }

    private IEnumerator ShrinkToNothing() {
        for (float x = animationFrames - 1f; x >= 0; x--) {
            float percentage = x / animationFrames;
            transform.localScale = new Vector3(percentage, percentage);
            yield return null;
        }
        Destroy(gameObject);
    }
}

public class CombatGraphAdapter : RecyclerViewAdapter {

    private GraphEntryView entryPrefab;

    private List<CombatHero> heroes;
    private Dictionary<Guid, double> damageDictionary;
    private Dictionary<Guid, double> healingDictionary;
    private double highestNumber;

    public CombatGraphAdapter(GraphEntryView entryPrefab) {
        this.entryPrefab = entryPrefab;
    }

    public void SwapReport(CombatReport report) {
        heroes = new List<CombatHero>();
        damageDictionary = new Dictionary<Guid, double>();
        healingDictionary = new Dictionary<Guid, double>();
        foreach (CombatHero hero in report.allies) {
            if (hero == null) continue;
            heroes.Add(hero);
            damageDictionary[hero.combatHeroGuid] = 0.0;
            healingDictionary[hero.combatHeroGuid] = 0.0;
        }
        foreach (CombatHero hero in report.enemies) {
            if (hero == null) continue;
            heroes.Add(hero);
            damageDictionary[hero.combatHeroGuid] = 0.0;
            healingDictionary[hero.combatHeroGuid] = 0.0;
        }

        foreach (CombatRound round in report.rounds) {
            foreach (CombatTurn turn in round.turns) {
                foreach (CombatStep step in turn.steps) {
                    damageDictionary[step.attackerGuid] += step.damage;
                    healingDictionary[step.attackerGuid] += step.healing;
                }
            }
            foreach (CombatStep step in round.endOfTurn) {
                damageDictionary[step.attackerGuid] += step.damage;
                healingDictionary[step.attackerGuid] += step.healing;
            }
        }

        foreach (CombatHero hero in heroes) {
            var damage = damageDictionary[hero.combatHeroGuid];
            var healing = healingDictionary[hero.combatHeroGuid];
            if (damage > highestNumber) {
                highestNumber = damage;
            }
            if (healing > highestNumber) {
                highestNumber = healing;
            }
        }
    }

    public override GameObject OnCreateViewHolder(RectTransform contentHolder) {
        return UnityEngine.Object.Instantiate(entryPrefab, contentHolder).gameObject;
    }

    public override void OnBindViewHolder(GameObject viewHolder, int position) {
        var entryView = viewHolder.GetComponent<GraphEntryView>();
        var hero = heroes[position];
        var damage = damageDictionary[hero.combatHeroGuid];
        var healing = healingDictionary[hero.combatHeroGuid];
        entryView.SetInformation(hero, damage, healing, highestNumber);
    }

    public override int GetItemCount() {
        return heroes == null ? 0 : heroes.Count;
    }
}
