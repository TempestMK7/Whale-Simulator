using System;
using System.Collections.Generic;

public class AbilityContainer {

    public static void EvaluatePassives(CombatHero hero) {
        switch (hero.baseHero.PassiveAbility) {
            case AbilityEnum.STACKING_ATTACK:
                hero.AddStatus(new StatusContainer(StatusEnum.ATTACK_UP, 1.1, StatusContainer.INDEFINITE));
                break;
        }
    }
}

public enum AbilityEnum {
    NONE, FASTER_ENERGY, STACKING_ATTACK
}
