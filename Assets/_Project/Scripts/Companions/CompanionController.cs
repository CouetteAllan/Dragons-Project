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
    CompanionAnims _anims;
    PlayerController _player;
    CompanionState _currentState = CompanionState.Caged;

    private void Awake()
    {
        _anims = GetComponent<CompanionAnims>();
    }

    public void Deliver(PlayerController player)
    {
        _player = player;
        //Change companion state
        _currentState = CompanionState.Free;
    }

    public void Shoot()
    {
        //Shoot using the correctStrategy
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
    }

}
