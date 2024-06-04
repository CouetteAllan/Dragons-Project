using UnityEngine;

[CreateAssetMenu(fileName = "BossTantrum", menuName = "Data/Enemy/Boss Pattern/Tantrum")]
public class BossTantrum : BossPattern
{
    public float AreaRadius = 6.0f;
    
    public override void ExecutePattern(Vector2 direction, EnemyController enemy)
    {
        //Deal Damage around him
        var cast = Physics2D.OverlapCircle(enemy.transform.position, AreaRadius, enemy.GetPlayerLayer());
        if(cast)
        {
            cast.attachedRigidbody.velocity = Vector3.zero;
            if(cast.gameObject.TryGetComponent(out PlayerController playerController))
            {
                playerController.ReceiveDamage(enemy, enemy.GetDatas().BaseDamage);
            }
        }
    }
}
