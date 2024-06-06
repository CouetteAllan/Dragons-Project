using UnityEngine;
[CreateAssetMenu(fileName = "PickUp Effect", menuName = "Data/PickUp/PickUp Key")]
public class PickUpKey : PickUpEffect
{
    public override bool DoEffect(PlayerController player, PickUpObject pickUp)
    {
        if (player.KeysNumber >= 3)
            return false;
        player.AddKey(pickUp.transform);
        return true;
    }
}
