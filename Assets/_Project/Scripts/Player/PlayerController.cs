using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovements),typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour, IHealth, IHittable, IHitSource
{
    public static event Action<float> OnPlayerUpdateHealth;
    public static event Action OnPlayerDeath;

    [SerializeField] private PlayerData _baseDatas;

    public float MaxHealth => _baseDatas.BaseHealth;
    public float CurrentHealth => _currentHealth;

    public Transform Transform => this.transform;

    private PlayerMovements _movements;
    private PlayerInputs _inputs;
    private PlayerAnims _anims;

    private float _currentHealth;
    private bool _canMove = true;

    private void Awake()
    {
        _movements = GetComponent<PlayerMovements>();
        _inputs = GetComponent<PlayerInputs>();
        _anims = GetComponent<PlayerAnims>();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.StartGame)
            GameManager.Instance.SetPlayer(this);
        if (newState == GameState.Pause)
            _canMove = false;
        else
            _canMove = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canMove)
            return;
        if(Mathf.Abs(_inputs.Dir.x) > .1f)
            _anims.SwapGraphScale(_inputs.Dir.x > 0);
    }

    private void FixedUpdate()
    {
        if (!_canMove)
            return;
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
        _anims.AnimTakeDamage();
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    }
}
