using UnityEngine;
[CreateAssetMenu(fileName = "ProjectileSTrategy", menuName = "Data/Projectile/Strategy/Fire")]

public class FireStrategy : ProjectileStrategy
{
    public override void ProjectileDamageBehaviour(Collider2D collision, Projectile projectile)
    {
        if (collision.gameObject.TryGetComponent(out IHittable hittable))
        {
            var hitPoint = collision.ClosestPoint(projectile.transform.position);
            var hit = Physics2D.OverlapCircleAll(hitPoint, 1.5f);
            if (hit != null)
            {
                foreach (var hitTarget in hit)
                {
                    if (hitTarget.gameObject.TryGetComponent(out IHittable hittableTarget))
                        hittableTarget.ReceiveDamage(projectile, projectile.GetDatas().ProjectileDamage);

                }
            }
            FXManager.Instance.CreateFX("fireExplosion", hitPoint);
            projectile.EndProjectile();
        }
    }
}
