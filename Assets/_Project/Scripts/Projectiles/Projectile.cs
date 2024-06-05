using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IHitSource
{
    private IObjectPool<Projectile> _projectilePool;
    private ProjectileData _datas;
    private Rigidbody2D _rb;

    public Transform Transform => this.transform;

    private bool _isDisabled = false;

    private FunctionTimer.FunctionTimerObject _destroyFunction;

    public void Initialize(ProjectileData datas, IObjectPool<Projectile> pool = null)
    {
        _rb = GetComponent<Rigidbody2D>();
        _datas = datas;
        _projectilePool = pool;
    }

    public void LaunchProjectile(Vector2 direction)
    {
        _isDisabled = false;
        _rb.velocity = direction * _datas.ProjectileSpeed;
        _datas.ProjectileStrategy.ProjectileLaunch(direction,this);
        _destroyFunction = FunctionTimer.CreateObject(() => EndProjectile(), _datas.ProjectileDuration);
    }

    private void Update()
    {
        if (_destroyFunction == null)
            return;
        _destroyFunction.Update();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _datas.ProjectileStrategy.ProjectileDamageBehaviour(collision, this);
    }

    public void EndProjectile()
    {
        if (_projectilePool == null)
        {
            Destroy(this.gameObject);
            return;
        }
        _destroyFunction = null;
        if (_isDisabled)
            return;
        transform.rotation = Quaternion.identity;
        _projectilePool.Release(this);
        _isDisabled = true;

    }

    public ProjectileData GetDatas() => _datas;

}
