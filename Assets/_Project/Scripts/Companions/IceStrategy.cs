using UnityEngine;

[CreateAssetMenu(fileName = "IceStrategy", menuName = "Data/Companion/Strategy/Ice")]

public class IceStrategy : CompanionAttackStrategy
{


    public override bool ShootStrategy()
    {
        GameManager.Instance.Player.UnlockShield(this);
        return false;
    }
}
