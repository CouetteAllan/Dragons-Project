using UnityEngine;

[CreateAssetMenu(fileName = "PickUp Key", menuName = "Data/PickUp/PickUp Key")]
public class Key : PickUpEffect
{
    public override void DoEffect(PlayerController player)
    {
        //Player now possess a key
        player.AddKey();
    }
}