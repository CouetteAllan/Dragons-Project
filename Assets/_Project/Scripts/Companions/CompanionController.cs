using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshPro _tutoText;
    CompanionAnims _anims;
    PlayerController _player;
    CompanionState _currentState = CompanionState.Caged;

    private float _currentCooldownAttack = 0.0f;
    private int _currentIndex = 0;

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
        //Display tuto text

        _tutoText.text = _datas.TutoText;
        _tutoText.transform.DOScale(Vector3.one, 1.0f).SetEase(Ease.OutBounce);
        FunctionTimer.Create(() =>
        {
            _tutoText.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutFlash).OnComplete(() => _tutoText.gameObject.SetActive(false));
        }
        , 10.0f);
    }

    public void SetCompanionIndex(int index) => _currentIndex = index;

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
#if UNITY_EDITOR
        if (Keyboard.current.pKey.wasPressedThisFrame && _currentState == CompanionState.Caged)
        {
            Deliver(GameManager.Instance.Player);
        }
#endif

        if (_player == null || _currentState == CompanionState.Caged)
            return;
        var directionTowardPlayer = (this.transform.position - _player.transform.position).normalized;
        this.transform.position = Vector2.Lerp(this.transform.position,(((Vector2)_player.transform.position + Vector2.up) + Vector2.right * (float)_currentIndex * 2), Time.deltaTime * 2.0f);
        _anims.SwapGraphScale(directionTowardPlayer.x > 0.01f);

        _currentCooldownAttack -= Time.deltaTime;
        if(_currentCooldownAttack <= 0 && Shoot())
        {
            _currentCooldownAttack = _datas.CompanionStrategyAttack.TimeBetweenShots;
        }
    }

}
