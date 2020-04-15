using System.Collections;
using System.Collections.Generic;
using Com.Tempest.Whale.GameObjects;

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
