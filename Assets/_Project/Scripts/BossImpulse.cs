using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossImpulse : MonoBehaviour
{
    public float _impulseForce = 60.0f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
    }
    public void Impulse()
    {
        Vector2 dir =  GameManager.Instance.Player.transform.position - transform.position;
        _rb.AddForce(dir.normalized * _impulseForce, ForceMode2D.Impulse);
    }
}
