using log4net.Util;
using UnityEngine;

public abstract class ProjectileStrategy : ScriptableObject
{
    public string FxName;

    public abstract void ProjectileDamageBehaviour(Collider2D collision, Projectile projectile);
    public virtual void ProjectileLaunch(Vector2 direction, Projectile projectile)
    {
        projectile.transform.rotation = Quaternion.FromToRotation(projectile.transform.right, direction);
    }
}
