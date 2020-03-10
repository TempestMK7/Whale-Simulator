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

    private static readonly MissionInfo[][] allChapters = { chapter1 };

    public static MissionInfo GetMission(int chapter, int mission) {
        return allChapters[chapter - 1][mission - 1];
    }
    
    public static GenerationInfo GetGenerationInfo() {
        var state = StateManager.GetCurrentState();
        return new GenerationInfo(state.CurrentChapter);
    }
}

public struct MissionInfo {
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
