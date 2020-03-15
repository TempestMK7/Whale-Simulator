using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MissionContainer {

    private static readonly MissionInfo[] chapter1 = {
        new MissionInfo(1, 1, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD),
        new MissionInfo(2, 1, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER),
        new MissionInfo(3, 1, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN),
        new MissionInfo(4, 1, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN, HeroEnum.SNOW_MAN),
        new MissionInfo(5, 1, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING, HeroEnum.STATIC_CLING),
        new MissionInfo(6, 1, HeroEnum.PEBBLE_ELEMENTAL, HeroEnum.PEBBLE_ELEMENTAL, HeroEnum.PEBBLE_ELEMENTAL, HeroEnum.PEBBLE_ELEMENTAL, HeroEnum.PEBBLE_ELEMENTAL, HeroEnum.PEBBLE_ELEMENTAL),
        new MissionInfo(7, 1, HeroEnum.RAIN_MAN, HeroEnum.RAIN_MAN, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD, HeroEnum.VAPOR_CLOUD),
        new MissionInfo(8, 1, HeroEnum.BUSH_WHACKER, HeroEnum.BUSH_WHACKER, HeroEnum.BUSH_WHACKER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER, HeroEnum.HAPPY_FLOWER),
        new MissionInfo(9, 1, HeroEnum.EMBER, HeroEnum.EMBER, HeroEnum.EMBER, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN, HeroEnum.CANDLE_MAN),
        new MissionInfo(10, 1, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD)
    };

    private static readonly MissionInfo[] chapter2 = {
        new MissionInfo(11, 1, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE),
        new MissionInfo(12, 1, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.BATTERY),
        new MissionInfo(13, 1, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.BOULDER),
        new MissionInfo(14, 1, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD),
        new MissionInfo(15, 1, HeroEnum.BALL_OF_ROOTS, HeroEnum.BALL_OF_ROOTS, HeroEnum.BUSH_WHACKER, HeroEnum.HAPPY_FLOWER, HeroEnum.BUSH_WHACKER, HeroEnum.HAPPY_FLOWER),
        new MissionInfo(16, 1, HeroEnum.TORCH, HeroEnum.TORCH, HeroEnum.EMBER, HeroEnum.CANDLE_MAN, HeroEnum.EMBER, HeroEnum.CANDLE_MAN),
        new MissionInfo(17, 1, HeroEnum.ICICLE_FLINGER, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.SNOW_MAN, HeroEnum.ICICLE_FLINGER, HeroEnum.SNOW_MAN),
        new MissionInfo(18, 1, HeroEnum.SPARK_ELEMENTAL, HeroEnum.SPARK_ELEMENTAL, HeroEnum.BATTERY, HeroEnum.STATIC_CLING, HeroEnum.BATTERY, HeroEnum.STATIC_CLING),
        new MissionInfo(19, 1, HeroEnum.PEBBLE_FLINGER, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.PEBBLE_ELEMENTAL, HeroEnum.PEBBLE_FLINGER, HeroEnum.PEBBLE_ELEMENTAL),
        new MissionInfo(20, 1, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.HAPPY_FLOWER, HeroEnum.CANDLE_MAN, HeroEnum.EMBER, HeroEnum.BUSH_WHACKER)
    };

    private static readonly MissionInfo[] chapter3 = {
        new MissionInfo(21, 1, HeroEnum.RAIN_MAN, HeroEnum.ICE_CUBE, HeroEnum.SNOW_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.ICICLE_FLINGER, HeroEnum.RAIN_MAN),
        new MissionInfo(22, 1, HeroEnum.BALL_OF_ROOTS, HeroEnum.BALL_OF_ROOTS, HeroEnum.BOULDER, HeroEnum.HAPPY_FLOWER, HeroEnum.PEBBLE_FLINGER, HeroEnum.BUSH_WHACKER),
        new MissionInfo(23, 1, HeroEnum.TORCH, HeroEnum.TORCH, HeroEnum.EMBER, HeroEnum.BATTERY, HeroEnum.SPARK_ELEMENTAL, HeroEnum.STATIC_CLING),
        new MissionInfo(24, 1, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.HAPPY_FLOWER, HeroEnum.VAPOR_CLOUD, HeroEnum.RAIN_MAN, HeroEnum.BUSH_WHACKER),
        new MissionInfo(25, 1, HeroEnum.BUSH_WHACKER, HeroEnum.BALL_OF_ROOTS, HeroEnum.EMBER, HeroEnum.HAPPY_FLOWER, HeroEnum.BUSH_WHACKER, HeroEnum.TORCH),
        new MissionInfo(26, 1, HeroEnum.EMBER, HeroEnum.ICE_CUBE, HeroEnum.EMBER, HeroEnum.SNOW_MAN, HeroEnum.TORCH, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(27, 1, HeroEnum.ICICLE_FLINGER, HeroEnum.ICE_CUBE, HeroEnum.BATTERY, HeroEnum.SNOW_MAN, HeroEnum.SPARK_ELEMENTAL, HeroEnum.ICICLE_FLINGER),
        new MissionInfo(28, 1, HeroEnum.SPARK_ELEMENTAL, HeroEnum.BOULDER, HeroEnum.SPARK_ELEMENTAL, HeroEnum.STATIC_CLING, HeroEnum.PEBBLE_FLINGER, HeroEnum.BATTERY),
        new MissionInfo(29, 1, HeroEnum.BOULDER, HeroEnum.BOULDER, HeroEnum.RAIN_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.PEBBLE_FLINGER, HeroEnum.PEBBLE_ELEMENTAL),
        new MissionInfo(30, 1, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BALL_OF_ROOTS, HeroEnum.FEESH, HeroEnum.LIGHTNING_WRAITH, HeroEnum.ICICLE_FLINGER, HeroEnum.TORCH)
    };

    private static readonly MissionInfo[] chapter4 = {
        new MissionInfo(31, 1, HeroEnum.MIST_CALLER, HeroEnum.FEESH, HeroEnum.VAPOR_CLOUD, HeroEnum.RAIN_MAN, HeroEnum.MIST_CALLER, HeroEnum.RAIN_MAN),
        new MissionInfo(32, 1, HeroEnum.DRYAD, HeroEnum.BALL_OF_ROOTS, HeroEnum.HAPPY_FLOWER, HeroEnum.BUSH_WHACKER, HeroEnum.DRYAD, HeroEnum.BUSH_WHACKER),
        new MissionInfo(33, 1, HeroEnum.LAVA_GOLEM, HeroEnum.LAVA_GOLEM, HeroEnum.EMBER, HeroEnum.CANDLE_MAN, HeroEnum.TORCH, HeroEnum.CANDLE_MAN),
        new MissionInfo(34, 1, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.ICE_CUBE, HeroEnum.ICE_CUBE, HeroEnum.ICICLE_FLINGER, HeroEnum.BLIZZARD_WIZZARD, HeroEnum.SNOW_MAN),
        new MissionInfo(35, 1, HeroEnum.LIGHTNING_WRAITH, HeroEnum.BATTERY, HeroEnum.BATTERY, HeroEnum.SPARK_ELEMENTAL, HeroEnum.LIGHTNING_WRAITH, HeroEnum.STATIC_CLING),
        new MissionInfo(36, 1, HeroEnum.LIVING_WALL, HeroEnum.LIVING_WALL, HeroEnum.BOULDER, HeroEnum.PEBBLE_ELEMENTAL, HeroEnum.PEBBLE_FLINGER, HeroEnum.BOULDER),
        new MissionInfo(37, 1, HeroEnum.FEESH, HeroEnum.FEESH, HeroEnum.MIST_CALLER, HeroEnum.ICICLE_FLINGER, HeroEnum.RAIN_MAN, HeroEnum.BLIZZARD_WIZZARD),
        new MissionInfo(38, 1, HeroEnum.BALL_OF_ROOTS, HeroEnum.BALL_OF_ROOTS, HeroEnum.LIVING_WALL, HeroEnum.BUSH_WHACKER, HeroEnum.DRYAD, HeroEnum.PEBBLE_FLINGER),
        new MissionInfo(39, 1, HeroEnum.TORCH, HeroEnum.LAVA_GOLEM, HeroEnum.BATTERY, HeroEnum.EMBER, HeroEnum.LIGHTNING_WRAITH, HeroEnum.TORCH),
        new MissionInfo(40, 1, HeroEnum.EARTHZERKER, HeroEnum.EARTHZERKER, HeroEnum.SNOW_MAN, HeroEnum.VAPOR_CLOUD, HeroEnum.HAPPY_FLOWER, HeroEnum.CANDLE_MAN)
    };

    private static readonly MissionInfo[][] allChapters = { chapter1, chapter2, chapter3, chapter4 };

    public static MissionInfo GetMission(int chapter, int mission) {
        if (chapter > allChapters.Length) return null;
        return allChapters[chapter - 1][mission - 1];
    }
    
    public static GenerationInfo GetGenerationInfo() {
        var state = StateManager.GetCurrentState();
        return new GenerationInfo(state.CurrentChapter);
    }
}

public class MissionInfo {

    public int HeroLevel { get; }
    public int HeroAwakening { get; }
    public HeroEnum FaceOfMission { get; }

    public HeroEnum[] MissionHeroes { get; }

    public MissionInfo(int heroLevel, int heroAwakening, HeroEnum faceOfMission, params HeroEnum[] missionHeroes) {
        HeroLevel = heroLevel;
        HeroAwakening = heroAwakening;
        FaceOfMission = faceOfMission;
        MissionHeroes = missionHeroes;
    }
}

public struct GenerationInfo {

    public int GoldPerMinute { get; }
    public int SoulsPerMinute { get; }
    public int ExperiencePerMinute { get; }

    public GenerationInfo(int chapter) {
        GoldPerMinute = 20 * chapter;
        SoulsPerMinute = 10 * chapter;
        ExperiencePerMinute = 10 * chapter;
    }

    public static double GenerationPerMillisecond(int generation) {
        return generation / (60.0 * 1000.0);
    }
}
