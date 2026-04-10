using UnityEngine;
using DG.Tweening;

public class ColumnPair : MonoBehaviour, IPoolableObject {
  [SerializeField] private Transform _leftColumn;
  [SerializeField] private Transform _rightColumn;

  [SerializeField] private float _gapWidth = 5f;
  [SerializeField] private float _minColumnWidth = 2f;

  [SerializeField] private Collider2D _scoreTrigger; 

  private ObjectPool _pool;
  private Tween _moveTween;

  private void Awake() {
    _pool = FindObjectOfType<ObjectPool>();
  }

  public void OnObjectSpawn() {
    _scoreTrigger.enabled = true;
  }

  public void OnObjectReturn() {
    _moveTween?.Kill();
  }

  public void Setup(float horizontalPosition) {
    transform.position = new Vector3(horizontalPosition, transform.position.y, 0);
  }

  public void StartMovement(Vector3 endPosition, float duration) {
    _moveTween = transform.DOMoveY(endPosition.y, duration)
        .SetEase(Ease.Linear)
        .OnComplete(ReturnToPool);
  }

  private void ReturnToPool() {
    _pool.Return(gameObject);
  }
}