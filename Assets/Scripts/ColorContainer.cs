using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorContainer {
    private static Color32 waterColor = new Color32(16, 128, 240, 255);
    private static Color32 grassColor = new Color32(16, 240, 64, 255);
    private static Color32 fireColor = new Color32(240, 48, 32, 255);
    private static Color32 iceColor = new Color32(92, 240, 216, 255);
    private static Color32 earthColor = new Color32(224, 144, 32, 255);
    private static Color32 electricColor = new Color32(224, 240, 56, 255);

    public static Color ColorFromFaction(FactionEnum faction) {
        switch (faction) {
            case FactionEnum.WATER: return waterColor;
            case FactionEnum.GRASS: return grassColor;
            case FactionEnum.FIRE: return fireColor;
            case FactionEnum.ICE: return iceColor;
            case FactionEnum.EARTH: return earthColor;
            case FactionEnum.ELECTRIC: return electricColor;
            default: return waterColor;
        }
    }
}
