using MoreMountains.Feedbacks;
using System;
using System.Collections;
using UnityEngine;

public enum EnemyState
{
    WalkInRange,
    Attack,
    ReceiveDamage,
    IsDead
}

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IHittable, IHitSource, IHealth
{
    public static event Action<EnemyController> OnEnemyDeath;

    [SerializeField] private MMF_Player _feedbackHit,_deathFeedback;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private EnemyConfig _datas;
    public Transform Transform => this.transform;
    public float MaxHealth => _datas.BaseHealth;
    public float CurrentHealth => _currentHealth;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _currentHealth;
    private EnemyState _currentState = EnemyState.WalkInRange;

    private PlayerController _player;
    private Vector2 _attackDirection;

    private IEnemyStrategy _strategy;

    public void Initialize(EnemyConfig datas)
    {
        _datas = datas;
        _currentHealth = _datas.BaseHealth;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.Player;
        _animator = GetComponent<Animator>();
        _strategy = _datas.GetStrategy(this, _player, _animator);
    }

    private void FixedUpdate()
    {
        if (_currentState != EnemyState.WalkInRange)
        {
            _rb.velocity *= .9f;
            return;
        }

        _strategy.DoWalkInRange();
    }

    public void ReceiveDamage(IHitSource source, float damage)
    {
        //Some hit feedback
        _feedbackHit.PlayFeedbacks();
        _rb.AddForce((this.transform.position - source.Transform.position).normalized * 15.0f);
        ChangeHealth(-damage);

    }

    //TODO: Enemy walk in range, then attack => repeat
    public void ChangeEnemyState(EnemyState state)
    {
        if (state == _currentState)
            return;

        switch (state)
        {
            case EnemyState.WalkInRange:
                _strategy.DoWalkInRange();
                break;
            case EnemyState.Attack:
                StartAttack();
                break;
            case EnemyState.ReceiveDamage:
                break;
        }
        _currentState = state;
    }

    public void ChangeHealth(float healthChange)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healthChange,0,MaxHealth);
        //Display health on top of the enemy
        if (IsDead())
        {
            Destroy(_animator);
            //TODO: Explosion feedback
            OnEnemyDeath?.Invoke(this);
            _deathFeedback.PlayFeedbacks();
            ChangeEnemyState(EnemyState.IsDead);
        }
    }

    private bool IsDead()
    {
        return _currentHealth <= 0.0f;
    }


    private void StartAttack()
    {
        //Change animator state
        _attackDirection = (_player.transform.position - this.transform.position).normalized;
        _animator.SetFloat("Y Velocity", _rb.velocity.normalized.y);
        if(_datas.Type == EnemyConfig.EnemyType.Boss)
        {
            _animator.runtimeAnimatorController = _strategy.ChoseAttack();
        }
        this._rb.velocity = _attackDirection * 15.0f;
        _animator.SetTrigger("Attack");
    }


    public void DoAttack()
    {
        //Actual attack
        _strategy.DoAttack(_attackDirection);
    }

    public void EndAttackAnimation()
    {
        ChangeEnemyState(EnemyState.WalkInRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, _datas.Range);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position,_datas.AttackRadius);
        Gizmos.DrawWireSphere(this.transform.position + (transform.right * (_datas.AttackLenght)),_datas.AttackRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_datas.Type != EnemyConfig.EnemyType.Boss)
            return;

        if(collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.ReceiveDamage(this, _datas.BaseDamage);
        }
    }

    public EnemyConfig GetDatas() => _datas;
    public LayerMask GetPlayerLayer() => _playerLayer;
    public Rigidbody2D GetRB() => _rb;
}
