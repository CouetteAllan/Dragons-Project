
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ThunderStrategy",menuName = "Data/Companion/Strategy/Thunder")]
public class ThunderCompanionStrategy : CompanionAttackStrategy
{
    public static event Action<Vector2> OnCompanionThunder;

    public float AttackRange = 7.0f;
    public float AreaRange = 3.0f;
    public float ThunderDamage = 30.0f;
    public float ThunderDelay = 1.0f;
    public LayerMask AimLayer;

    public ParticleSystem ThunderParticles;
    public override bool ShootStrategy()
    {
        //Check all enemies in range
        var enemiesInRange = Physics2D.OverlapCircleAll(GameManager.Instance.Player.transform.position, AttackRange, AimLayer);
        if (enemiesInRange.Length != 0)
        {
            FunctionTimer.StopAllTimersWithName("Thunder");
            int randomIndex = UnityEngine.Random.Range(0, enemiesInRange.Length - 1);
            EnemyController chosenEnemy = enemiesInRange[randomIndex].GetComponent<EnemyController>();
            OnCompanionThunder?.Invoke(chosenEnemy.transform.position);
            OnCompanionShoot?.Invoke();
            var particles = Instantiate(ThunderParticles, chosenEnemy.transform.position, Quaternion.identity);
            particles.Play();
            FunctionTimer.Create(() => DelayDealDamageInArea(chosenEnemy.transform.position), ThunderDelay);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DelayDealDamageInArea(Vector2 areaPos)
    {
        FXManager.Instance.CreateFX("thunder", Vector2.zero);
        var enemiesInRange = Physics2D.OverlapCircleAll(areaPos, AreaRange);
        if(enemiesInRange != null)
        {
            foreach(var enemy in enemiesInRange)
            {
                if(enemy.TryGetComponent(out IHittable hittable))
                {
                    hittable.ReceiveDamage(GameManager.Instance.Player, ThunderDamage);
                }
            }
        }
    }
}
