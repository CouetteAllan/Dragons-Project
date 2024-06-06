using UnityEngine;

[CreateAssetMenu(fileName = "PickUp Effect",menuName = "Data/PickUp/PickUp Heal")]
public class PickUpHealEffect : PickUpEffect
{
    public override bool DoEffect(PlayerController player, PickUpObject pickUp)
    {
        if (player.CurrentHealth == player.MaxHealth)
            return false;

        player.ChangeHealth(EffectValue);
        return true;
    }
}
