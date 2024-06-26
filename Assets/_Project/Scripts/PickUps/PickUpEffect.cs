﻿using UnityEngine;

public abstract class PickUpEffect : ScriptableObject
{
    public float EffectValue = 2.0f;
    public string EffectName = "heal";
    public abstract bool DoEffect(PlayerController player, PickUpObject pickUp);
}
