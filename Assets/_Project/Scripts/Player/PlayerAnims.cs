using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnims : MonoBehaviour
{
    [Header("Moving Transform")]
    [SerializeField] private Transform _graphTransform, _bodyTransform, _shadowTransform;

    [SerializeField] private SpriteRenderer _headRenderer;
    [SerializeField] private Sprite[] _heads = new Sprite[2];

    private float _timerBeforeReswap = 0.0f;
    private Coroutine _currentCoroutine = null;
    private PlayerInputs _inputs;

    private void Awake()
    {
        _inputs = GetComponent<PlayerInputs>();
        _inputs.OnFireAction += OnFireAction;
    }
    private void OnDisable()
    {
        _inputs.OnFireAction -= OnFireAction;
    }

    private void OnFireAction(Vector2 obj)
    {
        SwapHeadSprite();
    }

    private void Start()
    {
        FloatAnim();
    }

    public void SwapHeadSprite()
    {
        _timerBeforeReswap = .2f;
        _headRenderer.sprite = _heads[1];
        _headRenderer.transform.DOBlendableScaleBy(Vector2.one * .2f,.1f).SetEase(Ease.OutFlash).SetLoops(2,LoopType.Yoyo);
        _headRenderer.transform.DOBlendableLocalMoveBy(Vector2.up * -.2f, .1f).SetEase(Ease.OutFlash).SetLoops(2, LoopType.Yoyo);
        if (_currentCoroutine == null)
            _currentCoroutine = StartCoroutine(DelaySwapSpriteCoroutine());

    }

    public void FloatAnim()
    {
        _bodyTransform.DOBlendableLocalMoveBy(Vector3.up * -.5f, .5f).SetEase(Ease.InOutCirc).SetLoops(-1,LoopType.Yoyo);
        _shadowTransform.DOBlendableScaleBy(Vector3.right * .5f, .5f).SetEase(Ease.InOutCirc).SetLoops(-1, LoopType.Yoyo);
        _shadowTransform.DOBlendableScaleBy(Vector3.up * .1f, .5f).SetEase(Ease.InOutCirc).SetLoops(-1, LoopType.Yoyo);
    }

    public void SwapGraphScale(bool facingRight)
    {
        if(facingRight && _graphTransform.localScale.x < 0)
        {
            Swap();
        }
        else if(!facingRight && _graphTransform.localScale.x > 0)
        {
            Swap();
        }
    }

    private void Swap()
    {
        Vector3 localScale = _graphTransform.localScale;
        localScale.x *= -1;
        _graphTransform.localScale = localScale;
    }

    IEnumerator DelaySwapSpriteCoroutine()
    {

        while(_timerBeforeReswap > 0.0f)
        {
            _timerBeforeReswap -= Time.deltaTime;
            yield return null;
        }
        _headRenderer.sprite = _heads[0];
        _currentCoroutine = null;
    }


}
