using System;
using System.Collections.Generic;
using UnityEngine;

public class HarshByteAnimation : MonoBehaviour {

    public Animator heroAnimator;

    public void SetTrigger(string triggerName) {
        heroAnimator.SetTrigger(triggerName);
    }
}
