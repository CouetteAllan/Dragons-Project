using UnityEngine;
[CreateAssetMenu(fileName = "ProjectileSTrategy",menuName = "Data/Projectile/Strategy/Boss")]
public class BossProjectileStrategy : ProjectileStrategy
{
    public override void ProjectileLaunch(Vector2 direction, Projectile projectile)
    {
        
    }

    public override void ProjectileDamageBehaviour(Collider2D collision, Projectile projectile)
    {
        if (collision.gameObject.TryGetComponent(out IHittable hittable))
        {
            var hitPoint = collision.ClosestPoint(projectile.transform.position);
            hittable.ReceiveDamage(projectile, projectile.GetDatas().ProjectileDamage);

            FXManager.Instance.CreateFX("fireExplosion", hitPoint);
            projectile.EndProjectile();
        }
    }
}
