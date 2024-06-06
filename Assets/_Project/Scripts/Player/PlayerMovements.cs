using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float _playerSpeed;
    private Rigidbody2D _rb;

    private bool _isDashing = false;
    private bool _isOnCooldown = false;
    private UpdateTimers _cooldownTimer= null;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    public void UpdateMovement(Vector2 direction, float speed)
    {
        if (_isDashing)
            return;
        _rb.velocity = direction * speed;
    }

    public void Disable()
    {
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;

    }

    private void Update()
    {
        if(_cooldownTimer != null)
        {
            _cooldownTimer.Update();
        }
    }

    public void Dash(DarkStrategy dashStats, Vector2 dashDirection)
    {
        if (_isDashing || _isOnCooldown)
            return;
        StartCoroutine(DashCoroutine(dashStats, dashDirection));
    }

    IEnumerator DashCoroutine(DarkStrategy stats, Vector2 dashDirection)
    {
        float baseDrag = _rb.drag;
        _isDashing = true;
        _rb.drag = 0.0f;
        float startTime = Time.time;
        Physics2D.IgnoreLayerCollision(7, this.gameObject.layer, true);
        Physics2D.IgnoreLayerCollision(9, this.gameObject.layer, true);
        while(stats.DashDuration + startTime > Time.time)
        {

            _rb.velocity = dashDirection * stats.DashSpeed;
            yield return new WaitForFixedUpdate();
        }
        _isDashing = false;
        _rb.velocity *= .4f;
        _rb.drag = baseDrag;
        Physics2D.IgnoreLayerCollision(7, this.gameObject.layer, false);
        Physics2D.IgnoreLayerCollision(9, this.gameObject.layer, false);

        Debug.Log(stats.EnemyLayer + " " + (int) stats.EnemyLayer);
        _isOnCooldown = true;
        _cooldownTimer = new UpdateTimers(stats.DashCooldown,() => _isOnCooldown = false);

    }


    public void SetSpeed(float speed) => _playerSpeed = speed;
}
