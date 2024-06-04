using UnityEngine;

[CreateAssetMenu(fileName = "BossThrow", menuName = "Data/Enemy/Boss Pattern/Throw")]
public class BossThrowKey : BossPattern
{
    public GameObject BossProjectile;

    public override void ExecutePattern(Vector2 direction, EnemyController enemy)
    {
        //Throw big key
        var proj = Instantiate(BossProjectile, enemy.transform.position, Quaternion.identity);
        
    }
}
