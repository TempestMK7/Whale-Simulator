using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RarityBehavior : MonoBehaviour {

    private const string emptyStar = "Icons/star_empty";
    private const string silverStar = "Icons/star_silver";
    private const string goldStar = "Icons/star_gold";

    public Image[] starImages;

    private Sprite emptySprite;
    private Sprite silverSprite;
    private Sprite goldSprite;

    public void Awake() {
        emptySprite = Resources.Load<Sprite>(emptyStar);
        silverSprite = Resources.Load<Sprite>(silverStar);
        goldSprite = Resources.Load<Sprite>(goldStar);
    }

    public void SetLevel(int rarity, int awakening, bool showEmptyStars) {
        for (int x = 0; x < starImages.Length; x++) {
            if (x < rarity) starImages[x].sprite = silverSprite;
            else if (x < awakening) starImages[x].sprite = goldSprite;
            else if (showEmptyStars) starImages[x].sprite = emptySprite;
            else starImages[x].enabled = false;
        }
    }
}
