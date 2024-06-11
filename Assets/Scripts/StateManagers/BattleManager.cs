using Com.Tempest.Whale.GameObjects;

public class BattleManager {

    private static BattleEnum currentBattleType;

    public static void SelectBattleType(BattleEnum battleType) {
        currentBattleType = battleType;
    }

    public static BattleEnum GetBattleType() {
        return currentBattleType;
    }
}
