using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private bool _godMode = false;

    public int KeysNumber => _keyNumber;
    private int _keyNumber = 0;

    private bool _dashUnlocked = false;

    private IInteractable _currentInteractable;
    private List<CompanionController> _companions = new List<CompanionController>();

    private DarkStrategy _dashData;
    public DarkStrategy DashData => _dashData;

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
        _inputs.OnDash += OnDash;
    }

    private void OnDash()
    {
        _movements.Dash(_dashData, _inputs.LastValidDir);
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
                _movements.Disable();
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
        _keyNumber = 0;
        OnPlayerUpdateKeyNumber?.Invoke(_keyNumber);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            _godMode = !_godMode;
            Debug.Log("God mode toggle: " + _godMode);
        }
#endif
        if (!_canMove)
            return;
        if(Mathf.Abs(_inputs.Dir.x) > .1f)
            _anims.SwapGraphScale(_inputs.Dir.x > 0);
    }

    private void FixedUpdate()
    {
        if (!_canMove)
            return;
        _movements.UpdateMovement(_inputs.Dir.normalized, _baseDatas.BaseSpeed);
    }

    public void ChangeHealth(float healthChange)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healthChange, 0, MaxHealth);
        //Update UI
        OnPlayerUpdateHealth?.Invoke(_currentHealth,MaxHealth);
        if (IsPlayerDead())
        {
            OnPlayerDeath?.Invoke();
            //Disable the player;
            var rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
        }
    }

    private bool IsPlayerDead() => _currentHealth <= 0;

    public void ReceiveDamage(IHitSource source, float damage)
    {
        if (_isInvincible || _godMode)
            return;
        //receive damage
        _anims.AnimTakeDamage();
        ChangeHealth(-damage);
        _isInvincible = true;
        FunctionTimer.Create(() => _isInvincible = false, _baseDatas.InvincibleTime);

    }

    public bool PickUpPowerUp(PickUpEffect effect)
    {
        if (effect.DoEffect(this))
        {
            _anims.PickUpObject(effect);
            return true;
        }
        return false;
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

    public void RemoveKey()
    {
        _keyNumber--;
        OnPlayerUpdateKeyNumber?.Invoke(_keyNumber);
    }

    public int AddCompanion(CompanionController companion)
    {
        _companions.Add(companion);
        int currentCompanionIndex = _companions.IndexOf(companion);
        return currentCompanionIndex;
    }

    public void UnlockDash(DarkStrategy dark)
    {
        if (_dashUnlocked)
            return;
        _inputs.UnlockDash();
        _dashData = dark;
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
        _inputs.OnDash -= OnDash;

    }
}
