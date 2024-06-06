using UnityEngine;

[CreateAssetMenu(fileName = "DarkStrategy", menuName = "Data/Companion/Strategy/Dark")]
public class DarkStrategy : CompanionAttackStrategy
{

    public float DashDuration = .2f;
    public float DashSpeed = 10.0f;
    public LayerMask EnemyLayer;
    public LayerMask ProjectileLayer;
    public override bool ShootStrategy()
    {
        GameManager.Instance.Player.UnlockDash(this);
        return true;
    }
}
