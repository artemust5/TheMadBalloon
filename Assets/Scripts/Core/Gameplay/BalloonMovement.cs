using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BalloonMovement : MonoBehaviour {
  [SerializeField] private float _upwardSpeed = 5f; 
  [SerializeField] private float _downwardSpeed = 3f; 

  [SerializeField] private float _horizontalSmoothTime = 0.1f; 

  [SerializeField] private PlayerInput _input;
  private Rigidbody2D _rb;

  private float _horizontalVelocityDamp;

  private void Awake() {
    _rb = GetComponent<Rigidbody2D>();

    _rb.isKinematic = false;
    _rb.gravityScale = 0;
    _rb.freezeRotation = true; 
  }

  private void FixedUpdate() {
    HandleVerticalMovement();
    HandleHorizontalMovement();
  }

  private void HandleVerticalMovement() {
    float targetVerticalSpeed = _input.IsTapHeld ? _upwardSpeed : -_downwardSpeed;
    _rb.velocity = new Vector2(_rb.velocity.x, targetVerticalSpeed);
  }

  private void HandleHorizontalMovement() {
    float targetHorizontalSpeed;

    if (_input.IsTapHeld) {
      float targetX = _input.TargetWorldX;

      targetHorizontalSpeed = (targetX - _rb.position.x) / _horizontalSmoothTime;
    }
    else {
      targetHorizontalSpeed = 0;
    }

    float smoothHorizontalSpeed = Mathf.SmoothDamp(
        _rb.velocity.x,
        targetHorizontalSpeed,
        ref _horizontalVelocityDamp,
        _horizontalSmoothTime
    );

    _rb.velocity = new Vector2(smoothHorizontalSpeed, _rb.velocity.y);
  }

  public void ResetVelocity() {
    _horizontalVelocityDamp = 0f;
  }
}