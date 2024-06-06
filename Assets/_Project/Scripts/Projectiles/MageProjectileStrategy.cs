using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSTrategy", menuName = "Data/Projectile/Strategy/Mage")]
public class MageProjectileStrategy : ProjectileStrategy
{
    public override void ProjectileDamageBehaviour(Collider2D collision, Projectile projectile)
    {
        if (collision.gameObject.TryGetComponent(out IHittable hittable))
        {
            var hitPoint = collision.ClosestPoint(projectile.transform.position);
            hittable.ReceiveDamage(projectile, projectile.GetDatas().ProjectileDamage);

            //FXManager.Instance.CreateFX(FxName, hitPoint);
            projectile.EndProjectile();
        }
    }
}
