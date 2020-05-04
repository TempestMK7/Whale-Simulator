﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class HarshByteAnimation : MonoBehaviour {

    public Animator heroAnimator;

    public virtual void OnCreate(Vector3 localScale) {
        var particles = GetComponentsInChildren<ParticleSystem>();
        Vector3 particleScale = localScale * 0.8f;
        foreach (ParticleSystem particle in particles) {
            particle.transform.localScale = particleScale;
        }
    }

    public virtual void Attack() {
        heroAnimator.SetTrigger("Attack");
    }

    public virtual void Special() {
        heroAnimator.SetTrigger("Special");
    }

    public virtual void Hurt() {
        heroAnimator.SetTrigger("Hurt");
    }

    public virtual void Death() {
        heroAnimator.SetTrigger("Death");
    }
}