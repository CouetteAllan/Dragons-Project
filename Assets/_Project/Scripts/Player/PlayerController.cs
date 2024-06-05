using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMovements),typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour, IHealth, IHittable, IHitSource
{
    public static event Action<float,float> OnPlayerUpdateHealth;
    public static event Action OnPlayerDeath;
    public static event Action<int> OnPlayerUpdateKeyNumber;

    [SerializeField] private PlayerData _baseDatas;

    public float MaxHealth => _baseDatas.BaseHealth;
    public float CurrentHealth => _currentHealth;

    public Transform Transform => this.transform;

    private PlayerMovements _movements;
    private PlayerInputs _inputs;
    private PlayerAnims _anims;
    private PlayerFireProjectile _fireScript;

    private float _currentHealth;
    private bool _canMove = true;

    private bool _isInvincible = false;

    public int KeysNumber => _keyNumber;
    private int _keyNumber = 0;

    private IInteractable _currentInteractable;
    private List<CompanionController> _companions = new List<CompanionController>();

    private void Awake()
    {
        _movements = GetComponent<PlayerMovements>();
        _inputs = GetComponent<PlayerInputs>();
        _anims = GetComponent<PlayerAnims>();
        _fireScript = GetComponent<PlayerFireProjectile>();
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
        _movements.SetSpeed(_baseDatas.BaseSpeed);
        _keyNumber = 0;

        _inputs.OnInteractAction += OnInteractAction;
    }

    private void OnInteractAction()
    {
        if(_currentInteractable!= null && _canMove)
        {
            _currentInteractable.Interact(this);
        }
    }

    private void GameManager_OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.StartGame)
            GameManager.Instance.SetPlayer(this);
        switch(newState)
        {
            case GameState.Pause:
            case GameState.GameOver:
            case GameState.Victory:
                _canMove = false;
                _inputs.DisableInputs(true);
                break;
            case GameState.InGame:
            case GameState.StartGame:
                _canMove = true;
                _inputs.DisableInputs(false);
                break;
        }

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
        OnPlayerUpdateHealth?.Invoke(_currentHealth,MaxHealth);
        if (IsPlayerDead())
            OnPlayerDeath?.Invoke();
    }

    private bool IsPlayerDead() => _currentHealth <= 0;

    public void ReceiveDamage(IHitSource source, float damage)
    {
        if (_isInvincible)
            return;
        //receive damage
        _anims.AnimTakeDamage();
        ChangeHealth(-damage);
        _isInvincible = true;
        FunctionTimer.Create(() => _isInvincible = false, _baseDatas.InvincibleTime);

    }

    public void PickUpPowerUp(PickUpEffect effect)
    {
        effect.DoEffect(this);
        _anims.PickUpObject(effect);
    }

    public void UpgradeCD(float duration)
    {
        _fireScript.ChangeCD(duration);
    }

    public void AddKey()
    {
        _keyNumber++;
        OnPlayerUpdateKeyNumber?.Invoke(_keyNumber);
    }

    public void AddCompanion(CompanionController companion)
    {
        _companions.Add(companion);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.DisplayInteraction();
            _currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.HideInteraction();
            _currentInteractable = null;
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
        _inputs.OnInteractAction -= OnInteractAction;
    }
}
