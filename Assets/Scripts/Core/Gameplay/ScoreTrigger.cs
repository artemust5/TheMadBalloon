using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreTrigger : MonoBehaviour {
  private Collider2D _collider;

  private void Awake() {
    _collider = GetComponent<Collider2D>();
  }

  private void OnEnable() {
    _collider.enabled = true;
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {
      _collider.enabled = false;
      EventBus.Raise(new ScorePointEvent());
    }
  }
}