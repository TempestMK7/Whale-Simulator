using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager {

    private static BattleEnum currentBattleType;

    public static void SelectBattleType(BattleEnum battleType) {
        currentBattleType = battleType;
    }

    public static BattleEnum GetBattleType() {
        return currentBattleType;
    }

    public static void ClearBattleType() {
        currentBattleType = BattleEnum.NONE;
    }
}

public enum BattleEnum {
    NONE, CAMPAIGN, TOWER
}
