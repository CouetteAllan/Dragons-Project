using UnityEngine;

[CreateAssetMenu(fileName = "BossThrow", menuName = "Data/Enemy/Boss Pattern/Throw")]
public class BossThrow : BossPattern
{
    public ProjectileData BossProjectile;

    public override void ExecutePattern(Vector2 direction, EnemyController enemy)
    {
        //Throw big key
        var proj = Instantiate(BossProjectile.ProjectilePrefab, enemy.transform.position + (Vector3.up * 1.3f), Quaternion.identity);
        proj.Initialize(BossProjectile);
        proj.LaunchProjectile(direction);
        
    }
}
