using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnims : MonoBehaviour
{
    [SerializeField] private Transform _graphTransform;


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
}
