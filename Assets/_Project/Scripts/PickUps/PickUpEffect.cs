using UnityEngine;

public abstract class PickUpEffect : ScriptableObject
{
    public float EffectValue = 2.0f;
    public abstract void DoEffect(PlayerController player);
}
