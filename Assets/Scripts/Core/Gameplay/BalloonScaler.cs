using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BalloonScaler : MonoBehaviour {
  [SerializeField] private Vector3 _minSize = new Vector3(0.5f, 0.5f, 1f);
  [SerializeField] private Vector3 _maxSize = new Vector3(2f, 2f, 1f);
  [SerializeField] private float _inflateRate = 1.5f;
  [SerializeField] private float _deflateRate = 1.0f;

  [SerializeField] private float _baseColliderRadius = 0.5f;

  [SerializeField] private PlayerInput _input;
  private CircleCollider2D _collider;

  private void Awake() {
    _collider = GetComponent<CircleCollider2D>();
  }

  private void FixedUpdate() {
    Vector3 targetScale = _input.IsTapHeld ? _maxSize : _minSize;
    float rate = _input.IsTapHeld ? _inflateRate : _deflateRate;

    transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.fixedDeltaTime * rate);

    _collider.radius = transform.localScale.x * _baseColliderRadius;
  }

  public void ResetSize() {
    transform.localScale = _minSize;
    if (_collider != null) {
      _collider.radius = transform.localScale.x * _baseColliderRadius;
    }
  }
}