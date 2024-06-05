using UnityEngine;

[CreateAssetMenu(fileName = "BossTantrum", menuName = "Data/Enemy/Boss Pattern/Tantrum")]
public class BossTantrum : BossPattern
{
    public float AreaRadius = 7.0f;
    
    public override void ExecutePattern(Vector2 direction, EnemyController enemy)
    {
        enemy.GetRB().AddForce(direction * 10.0f, ForceMode2D.Impulse);
        //Deal Damage around him
        var cast = Physics2D.OverlapCircleAll(enemy.transform.position, AreaRadius);
        if(cast.Length != 0)
        {
            foreach (var item in cast)
            {
                if (item.gameObject.TryGetComponent(out IHittable playerController))
                {
                    playerController.ReceiveDamage(enemy, enemy.GetDatas().BaseDamage);
                }
            }
            
        }
    }
}
