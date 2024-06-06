using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void UpdateMovement(Vector2 direction)
    {
        _rb.velocity = direction * _playerSpeed;
    }

    public void Disable()
    {
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;

    }

    public void SetSpeed(float speed) => _playerSpeed = speed;
}
