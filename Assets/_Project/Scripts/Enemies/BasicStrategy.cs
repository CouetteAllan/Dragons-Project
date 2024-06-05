using System;
using UnityEngine;

public class BasicStrategy : IEnemyStrategy
{
    protected PlayerController _player;
    protected EnemyController _controller;
    protected Animator _animator;
    protected Rigidbody2D _rb;
    protected EnemyConfig _datas;
    protected LayerMask _playerLayer;
    protected Transform _controllerTransform;

    public BasicStrategy(EnemyController controller, PlayerController player, Animator anim)
    {
        _player = player;
        _controller = controller;
        _animator = anim;
        _rb = _controller.GetComponent<Rigidbody2D>();
        _datas = _controller.GetDatas();
        _playerLayer = _controller.GetPlayerLayer();
        _controllerTransform = _controller.transform;
    }

    public virtual AnimatorOverrideController ChoseAttack()
    {
        return null;
    }

    public virtual void DoAttack(Vector2 attackDirection)
    {
        var attack = Physics2D.CircleCast(_controller.transform.position, _datas.AttackRadius, attackDirection, _datas.AttackLenght, _playerLayer);
        if (attack)
        {
            if (attack.rigidbody.TryGetComponent(out PlayerController playerHit))
            {
                playerHit.ReceiveDamage(_controller, _datas.BaseDamage);
            }
        }
    }

    public virtual void DoWalkInRange()
    {
        if (_controller.gameObject == null)
            return;
        if (_animator == null)
            return;
        var directionTowardPlayer = _player.transform.position - _controller.transform.position;
        _rb.velocity = directionTowardPlayer.normalized * _datas.BaseSpeed;
        
        _animator.SetFloat("Y Velocity", _rb.velocity.normalized.y);
        if (IsInAttackRange())
        {
            _controller.ChangeEnemyState(EnemyState.Attack);
            return;
        }
    }

    public virtual void SpawnBehaviour(Action OnFinishSpawnCallBack)
    {
        //RegularSpawn
        OnFinishSpawnCallBack();
    }

    protected bool IsInAttackRange()
    {
        var cast = Physics2D.OverlapCircle(_controller.transform.position, _datas.Range, _playerLayer);
        return cast;
    }
}
