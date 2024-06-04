using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerFireProjectile : MonoBehaviour
{
    [SerializeField] private ProjectileData _fireProjectileData;
    [SerializeField] private Transform _firePoint;
    private PlayerInputs _inputs;
    private IObjectPool<Projectile> _objectPool;


    private void Awake()
    {
        _inputs = GetComponent<PlayerInputs>();
        _inputs.OnFireAction += OnFireAction;

        _objectPool = new ObjectPool<Projectile>(OnCreateProjectile, OnGetProjectile, OnReleaseProjectile, OnDestroyProjectile, true);
    }

    private void OnDisable()
    {
        _inputs.OnFireAction -= OnFireAction;
    }

    private void OnDestroyProjectile(Projectile pooledObject)
    {
        Destroy(pooledObject);
    }

    private void OnReleaseProjectile(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnGetProjectile(Projectile pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
        pooledObject.transform.position = this.transform.position;
    }

    private Projectile OnCreateProjectile()
    {
        Projectile newProjectile = Instantiate(_fireProjectileData.ProjectilePrefab);
        newProjectile.Initialize(_fireProjectileData, _objectPool);
        return newProjectile;
    }

    private void OnFireAction(Vector2 direction)
    {
        //Fire an actual fire ball in the direction
        Projectile projectile = _objectPool.Get();
        projectile.transform.position = _firePoint.position;
        projectile.LaunchProjectile(direction);
    }
}
