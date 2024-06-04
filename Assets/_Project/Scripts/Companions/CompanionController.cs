using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CompanionState
{
    Caged,
    Free
}

[RequireComponent(typeof(CompanionAnims))]
public class CompanionController : MonoBehaviour, ICompanion
{
    [SerializeField] private CompanionData _datas;
    CompanionAnims _anims;
    PlayerController _player;
    CompanionState _currentState = CompanionState.Caged;

    private float _currentCooldownAttack = 0.0f;

    private void Awake()
    {
        _anims = GetComponent<CompanionAnims>();
        _currentCooldownAttack = _datas.CompanionStrategyAttack.TimeBetweenShots;
    }

    public void Deliver(PlayerController player)
    {
        _player = player;
        //Change companion state
        _currentState = CompanionState.Free;
    }

    public bool Shoot()
    {
        if (_datas.CompanionStrategyAttack.ShootStrategy())
        {
            _anims.CompanionPlayAnim();
            return true;
        }
        else
            return false;
    }

    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame && _currentState == CompanionState.Caged)
        {
            Deliver(GameManager.Instance.Player);
        }

        if (_player == null || _currentState == CompanionState.Caged)
            return;
        var directionTowardPlayer = (this.transform.position - _player.transform.position).normalized;
        this.transform.position = Vector2.Lerp(this.transform.position,((Vector2)_player.transform.position + Vector2.up), Time.deltaTime * 2.0f);
        _anims.SwapGraphScale(directionTowardPlayer.x > 0.01f);

        _currentCooldownAttack -= Time.deltaTime;
        if(_currentCooldownAttack <= 0 && Shoot())
        {
            _currentCooldownAttack = _datas.CompanionStrategyAttack.TimeBetweenShots;
        }
    }

}
