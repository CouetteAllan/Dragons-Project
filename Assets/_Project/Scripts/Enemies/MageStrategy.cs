using UnityEngine;

public class MageStrategy : BasicStrategy
{
    private ProjectileData _mageProjectile;
    public MageStrategy(EnemyController controller, PlayerController player, Animator anim) : base(controller, player, anim)
    {
        _mageProjectile = AssetsManager.Instance.GetAssetSO("mageProjectile") as ProjectileData;
    }

    public override void DoAttack(Vector2 attackDirection)
    {
        //Spawn a projectile
        var proj = GameObject.Instantiate(_mageProjectile.ProjectilePrefab, _controller.transform.position,Quaternion.identity);
        proj.Initialize(_mageProjectile);
        proj.LaunchProjectile(attackDirection);
    }
}
