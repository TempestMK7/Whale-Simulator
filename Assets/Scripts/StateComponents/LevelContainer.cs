using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer {

    private const float baseValue = 200f;
    private const float modifier = 1.05f;

    private const float heroBaseValue = 20f;
    private const float heroModifier = 1.05f;

    private static FusionRequirement[] fusionRequirements = {
        new FusionRequirement(1, 1, 1, 2, 1, true),
        new FusionRequirement(2, 1, 2, 2, 2, true),
        new FusionRequirement(3, 1, 3, 2, 3, true),
        new FusionRequirement(4, 1, 4, 2, 4, true),
        new FusionRequirement(5, 2, 5, 2, 5, true),
        new FusionRequirement(6, 0, 5, 1, 6, true),
        new FusionRequirement(7, 0, 5, 2, 6, true),
        new FusionRequirement(8, 0, 5, 3, 6, true),
        new FusionRequirement(9, 2, 5, 1, 9, false)
    };

    public static double ExperienceRequirement(float level) {
        return baseValue * Mathf.Pow(modifier, level - 1f);
    }

    public static int HeroExperienceRequirement(float level) {
        return (int)(heroBaseValue * Mathf.Pow(heroModifier, level - 1f));
    }

    public static int MaxLevelForAwakeningValue(int awakeningValue) {
        if (awakeningValue <= 5) return 50 + (awakeningValue * 10);
        awakeningValue -= 5;
        return 100 + (awakeningValue * 20);
    }

    public static FusionRequirement? GetFusionRequirementForLevel(int awakeningLevel) {
        if (awakeningLevel >= 10) return null;
        return fusionRequirements[awakeningLevel - 1];
    }
}

public struct FusionRequirement {

    public int CurrentAwakeningLevel { get; }
    public int SameHeroRequirement { get; }
    public int SameHeroLevel { get; }
    public int FactionHeroRequirement { get; }
    public int FactionHeroLevel { get; }
    public bool RequireSameFaction { get; }

    public FusionRequirement(int currentAwakeningLevel, int sameHeroRequirement, int sameHeroLevel, int factionHeroRequirement, int factionHeroLevel, bool requireSameFaction) {
        CurrentAwakeningLevel = currentAwakeningLevel;
        SameHeroRequirement = sameHeroRequirement;
        SameHeroLevel = sameHeroLevel;
        FactionHeroRequirement = factionHeroRequirement;
        FactionHeroLevel = factionHeroLevel;
        RequireSameFaction = requireSameFaction;
    }
}
