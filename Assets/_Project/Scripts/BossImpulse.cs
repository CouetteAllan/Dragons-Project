using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossImpulse : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
    }
    public void Impulse(float impulse)
    {
        Vector2 dir =  GameManager.Instance.Player.transform.position - transform.position;
        _rb.velocity += dir.normalized * impulse;
    }
}
