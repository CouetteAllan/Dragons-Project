using UnityEngine;

[CreateAssetMenu(fileName = "PickUp Effect", menuName = "Data/PickUp/PickUp Hot Chili")]
public class PickUpHotChiliEffect : PickUpEffect
{
    public float BonusDuration = 5.0f;
    public override bool DoEffect(PlayerController player, PickUpObject pickUp)
    {
        //Reduce CD
        player.UpgradeCD(BonusDuration);
        return true;
    }
}