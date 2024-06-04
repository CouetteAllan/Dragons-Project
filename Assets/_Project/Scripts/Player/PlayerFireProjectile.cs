using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerFireProjectile : MonoBehaviour
{
    public static event Action OnFireBallLaunched;

    [SerializeField] private ProjectileData _fireProjectileData;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireBallCooldown = .4f;
    private bool _fireBallOnCooldown = false;
    private float _cooldown;
    private PlayerInputs _inputs;
    private IObjectPool<Projectile> _objectPool;
    private GameObject _fireProjectileParent;

    private void Awake()
    {
        _inputs = GetComponent<PlayerInputs>();
        _inputs.OnFireAction += OnFireAction;

        _objectPool = new ObjectPool<Projectile>(OnCreateProjectile, OnGetProjectile, OnReleaseProjectile, OnDestroyProjectile, true);
        _cooldown = _fireBallCooldown;
    }

    private void OnDisable()
    {
        _inputs.OnFireAction -= OnFireAction;
    }
    private void Update()
    {
        if (!_fireBallOnCooldown)
            return;
        _cooldown -= Time.deltaTime;
        if(_cooldown <= 0.0f)
        {
            _fireBallOnCooldown = false;
            _cooldown = _fireBallCooldown;
        }
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
        if(_fireProjectileParent == null)
            _fireProjectileParent = new GameObject("Instantiated Fire Balls");


        Projectile newProjectile = Instantiate(_fireProjectileData.ProjectilePrefab,_fireProjectileParent.transform);
        newProjectile.Initialize(_fireProjectileData, _objectPool);
        return newProjectile;
    }

    private void OnFireAction(Vector2 direction)
    {
        if (_fireBallOnCooldown)
            return;
        //Fire an actual fire ball in the direction
        Projectile projectile = _objectPool.Get();
        projectile.transform.position = _firePoint.position;
        projectile.LaunchProjectile(direction);
        _fireBallOnCooldown = true;
        OnFireBallLaunched?.Invoke();
    }
}
