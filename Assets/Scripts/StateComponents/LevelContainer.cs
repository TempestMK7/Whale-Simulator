using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer {

    private const float baseValue = 200f;
    private const float modifier = 1.05f;

    private const float heroBaseValue = 20f;
    private const float heroModifier = 1.05f;

    public static double ExperienceRequirement(float level) {
        return baseValue * Mathf.Pow(modifier, level - 1f);
    }

    public static int HeroExperienceRequirement(float level) {
        return (int)(heroBaseValue * Mathf.Pow(heroModifier, level - 1f));
    }
}
