using System.Collections;
using UnityEngine;

public class EliteStrategy : BasicStrategy
{
    private bool _animDone = false;

    public EliteStrategy(EnemyController controller, PlayerController player, Animator anim) : base(controller, player, anim)
    {
        _controller.OnAnimDone += OnAnimDone;
    }

    private void OnAnimDone()
    {
        _animDone = true;
    }

    public override void DoAttack(Vector2 attackDirection)
    {
        //Dash attack and check if the player is around;
        _animDone = false;
        _rb.velocity = attackDirection * _datas.DashAttackStrenght * 6.2f;
        _rb.freezeRotation = false;
        _controller.transform.right = attackDirection;
        GameManager.Instance.StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        while (!_animDone)
        {
            var attack = Physics2D.OverlapCircle((Vector2)_controller.transform.position, _datas.AttackRadius, _playerLayer);
            if(attack != null)
            {
                if (attack.gameObject.TryGetComponent(out PlayerController playerHit))
                {
                    playerHit.ReceiveDamage(_controller, _datas.BaseDamage);
                }
            }

            yield return new WaitForSeconds(.2f);

        }

        _controller.graphTransform.rotation = Quaternion.identity;
        _rb.freezeRotation = true;

    }
}
