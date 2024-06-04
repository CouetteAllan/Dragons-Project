using UnityEngine;

[CreateAssetMenu(fileName = "PickUp Effect",menuName = "Data/PickUp/PickUp Heal")]
public class PickUpHealEffect : PickUpEffect
{
    public override void DoEffect(PlayerController player)
    {
        player.ChangeHealth(EffectValue);
    }
}
