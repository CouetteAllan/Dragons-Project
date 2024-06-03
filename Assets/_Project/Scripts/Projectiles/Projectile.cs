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

    public void Initialize(ProjectileData datas, IObjectPool<Projectile> pool)
    {
        _rb = GetComponent<Rigidbody2D>();
        _datas = datas;
        _projectilePool = pool;
    }

    public void LaunchProjectile(Vector2 direction)
    {
        _rb.velocity = direction * _datas.ProjectileSpeed;
        Invoke(nameof(EndProjectile), _datas.ProjectileDuration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IHittable hittable))
        {
            hittable.DealDamage(this, _datas.ProjectileDamage);
            EndProjectile();
        }
    }

    private void EndProjectile() => _projectilePool.Release(this);

}
