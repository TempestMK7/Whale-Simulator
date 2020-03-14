using System;
using System.Collections.Generic;

public class AbilityContainer {

    public static List<DamageInstance> EvaluatePassives(CombatHero hero) {
        List<DamageInstance> output = new List<DamageInstance>();
        switch (hero.baseHero.PassiveAbility) {
            case AbilityEnum.STACKING_ATTACK:
                var status = new StatusContainer(StatusEnum.ATTACK_UP, hero.combatHeroGuid, 0.2, StatusContainer.INDEFINITE);
                hero.AddStatus(status);
                var damageInstance = new DamageInstance(null, null, null, hero.combatHeroGuid, hero.combatHeroGuid);
                damageInstance.AddStatus(status);
                output.Add(damageInstance);
                break;
        }
        return output;
    }
}

public enum AbilityEnum {
    NONE =                  0,
    STACKING_ATTACK =       1,
    STACKING_MAGIC =        2,
    STACKING_DEFENSE =      3,
    STACKING_REFLECTION =   4,
    STACKING_SPEED =        5,
    STACKING_CRIT =         6,
    FASTER_ENERGY =         7
}
