using UnityEngine;

public class EndlessBackground : MonoBehaviour {
  [SerializeField] private float _speed = 2f;

  [SerializeField] private bool _isLooping = true;

  [SerializeField] private float _loopHeight;

  private float _screenBottomY;
  private bool _isMoving = false;
  private Vector3 _initialPosition;
  private SpriteRenderer _spriteRenderer;

  private void Awake() {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _initialPosition = transform.position;

    Camera cam = Camera.main;
    if (cam != null) {
      float cameraHeight = cam.orthographicSize;
      _screenBottomY = cam.transform.position.y - cameraHeight;
    }
  }

  private void OnEnable() {
    ResetBackgroundState();

    EventBus.Subscribe<FirstTapEvent>(OnFirstTap);
    EventBus.Subscribe<GameOverEvent>(OnGameOver);
  }

  private void OnDisable() {
    EventBus.Unsubscribe<FirstTapEvent>(OnFirstTap);
    EventBus.Unsubscribe<GameOverEvent>(OnGameOver);
  }

  private void ResetBackgroundState() {
    _isMoving = false;
    transform.position = _initialPosition;
    _spriteRenderer.enabled = true;
  }

  private void Update() {
    if (!_isMoving) return;

    transform.Translate(Vector3.down * _speed * Time.deltaTime);

    float myTopEdge = transform.position.y + (_loopHeight / 2);

    if (myTopEdge < _screenBottomY - 10f) {
      if (_isLooping) {
        LoopPosition();
      }
      else {
        _spriteRenderer.enabled = false;
      }
    }
  }

  private void OnFirstTap(FirstTapEvent e) {
    _isMoving = true;
  }

  private void OnGameOver(GameOverEvent e) {
    _isMoving = false;
  }

  private void LoopPosition() {
    Vector3 newPos = transform.position;
    newPos.y += _loopHeight * 2;
    transform.position = newPos;
  }
}