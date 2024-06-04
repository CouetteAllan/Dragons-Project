using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovements),typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour, IHealth, IHittable
{
    public static event Action<float> OnPlayerUpdateHealth;
    public static event Action OnPlayerDeath;

    [SerializeField] private PlayerData _baseDatas;
    private PlayerMovements _movements;
    private PlayerInputs _inputs;
    private PlayerAnims _anims;

    public float MaxHealth => _baseDatas.BaseHealth;
    private float _currentHealth;
    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        _movements = GetComponent<PlayerMovements>();
        _inputs = GetComponent<PlayerInputs>();
        _anims = GetComponent<PlayerAnims>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(_inputs.Dir.x) > .1f)
            _anims.SwapGraphScale(_inputs.Dir.x > 0);
    }

    private void FixedUpdate()
    {
        _movements.UpdateMovement(_inputs.Dir.normalized);
    }

    public void ChangeHealth(float healthChange)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healthChange, 0, MaxHealth);
        //Update UI
        OnPlayerUpdateHealth?.Invoke(_currentHealth);
        if (IsPlayerDead())
            OnPlayerDeath?.Invoke();
    }

    private bool IsPlayerDead() => _currentHealth <= 0;

    public void ReceiveDamage(IHitSource source, float damage)
    {
        //receive damage
    }
}
