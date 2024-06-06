using MoreMountains.Feedbacks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public enum EnemyState
{
    WalkInRange,
    Attack,
    ReceiveDamage,
    IsDead,
    IsStun,
    IsSpawning
}

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour, IHittable, IHitSource, IHealth, IReceiveEffect
{
    public static event Action<EnemyController> OnEnemyDeath;
    public event Action OnAnimDone;

    [SerializeField] private MMF_Player _feedbackHit,_deathFeedback, _stunFeedback;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private EnemyConfig _datas;
    public Transform graphTransform;
    public Transform Transform => this.transform;
    public float MaxHealth => _datas.BaseHealth;
    public float CurrentHealth => _currentHealth;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _currentHealth;
    private EnemyState _currentState = EnemyState.IsSpawning;

    private PlayerController _player;
    private Vector2 _attackDirection;

    private IEnemyStrategy _strategy;
    private IObjectPool<Projectile> _projectiles;

    public void Initialize(EnemyConfig datas)
    {
        _datas = datas;
        _currentHealth = _datas.BaseHealth;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance.Player;
        _animator = GetComponent<Animator>();
        _strategy = _datas.GetStrategy(this, _player, _animator);
        _currentState = EnemyState.IsSpawning;
        _strategy.SpawnBehaviour(() => ChangeEnemyState(EnemyState.WalkInRange));
    }

    private void FixedUpdate()
    {
        if (_currentState != EnemyState.WalkInRange)
            return;
        
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
            case EnemyState.IsDead:
                _rb.velocity = Vector2.zero;
                break;
            case EnemyState.IsStun:
                _rb.velocity = Vector2.zero;
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

    public void RegisterPlayerPosition()
    {
        _attackDirection = (_player.transform.position - this.transform.position).normalized;
    }


    private void StartAttack()
    {
        _rb.velocity *= .5f;
        _attackDirection = (_player.transform.position - this.transform.position).normalized;
        _animator.SetFloat("Y Velocity", _rb.velocity.normalized.y);
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, this.gameObject.layer,true);
        if(_datas.Type == EnemyConfig.EnemyType.Boss)
        {
            _animator.runtimeAnimatorController = _strategy.ChoseAttack();
        }
        this._rb.velocity = _attackDirection * _datas.DashAttackStrenght;
        _animator.SetTrigger("Attack");
    }


    public void DoAttack()
    {
        //Actual attack dealing damage
        _strategy.DoAttack(_attackDirection);
    }

    public void EndAttackAnimation()
    {
        OnAnimDone?.Invoke();
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, this.gameObject.layer, false);
        if (_currentState == EnemyState.IsStun)
            return;
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

    public void ReceiveEffect()
    {
        //Receive thunder effect
        ChangeEnemyState(EnemyState.IsStun);
        if(_stunFeedback != null)
            _stunFeedback.PlayFeedbacks();
        _rb.velocity = Vector2.zero;
    }

    public void EndEffect()
    {
        ChangeEnemyState(EnemyState.WalkInRange);
    }

    public Transform GetGraphTransform => graphTransform;
}
