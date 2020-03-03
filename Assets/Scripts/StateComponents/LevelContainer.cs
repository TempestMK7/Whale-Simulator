using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer {

    private const float baseValue = 200f;
    private const float modifier = 1.05f;

    public static double experienceRequirement(float level) {
        return baseValue * Mathf.Pow(modifier, level - 1f);
    }
}
