using UnityEngine;
[CreateAssetMenu(fileName = "PickUp Effect", menuName = "Data/PickUp/PickUp Key")]
public class PickUpKey : PickUpEffect
{
    public override void DoEffect(PlayerController player)
    {
        player.AddKey();
    }
}
