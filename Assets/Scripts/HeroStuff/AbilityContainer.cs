using System;
using System.Collections.Generic;

public class AbilityContainer {

    public static void EvaluatePassives(CombatHero hero) {
        switch (hero.baseHero.PassiveAbility) {
            case AbilityEnum.STACKING_ATTACK:
                hero.AddStatus(new StatusContainer(StatusEnum.ATTACK_UP, hero.combatHeroGuid, 1.1, StatusContainer.INDEFINITE));
                break;
        }
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
