using UnityEngine;

public class BossStrategy : BasicStrategy
{
    private BossPattern[] _patterns;
    private BossPattern _chosenPattern;
    public BossStrategy(EnemyController controller, PlayerController player, Animator anim, params BossPattern[] patterns) : base(controller, player, anim)
    {
        _patterns = patterns;
    }

    public override void DoAttack(Vector2 attackDirection)
    {
        _chosenPattern.ExecutePattern(attackDirection,_controller);
    }

    public override AnimatorOverrideController ChoseAttack()
    {
        _chosenPattern = PickRandomPattern();
        return _chosenPattern.BossAnimPattern;
    }

    private BossPattern PickRandomPattern()
    {
        int randomIndex = Random.Range(0, _patterns.Length);
        return _patterns[randomIndex];
    }


}
