using UnityEngine;

public class MadBalloonController : MonoBehaviour {
  [SerializeField] private Vector3 _startPosition = new Vector3(0, -2, 0);
  [SerializeField] private Vector2 _verticalBounds = new Vector2(-10f, 100f);
  [SerializeField] private Vector2 _horizontalBounds = new Vector2(-7f, 7f);
  [SerializeField] private PlayerInput _input;
  [SerializeField] private BalloonMovement _movement;
  [SerializeField] private BalloonScaler _scaler;

  private bool _isGameActive = false;

  private void Awake() {
    ToggleComponents(false); 
    _input.enabled = false;    
  }

  private void Update() { 
    if (!_isGameActive) 
      return;
    CheckForOutOfBounds();
  }

  private void OnEnable() {
    ResetPlayerState();

    EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
    EventBus.Subscribe<GameOverEvent>(OnGameOver);
    EventBus.Subscribe<FirstTapEvent>(OnFirstTap);
  }

  private void OnDisable() {
    EventBus.Unsubscribe<GameStartedEvent>(OnGameStarted);
    EventBus.Unsubscribe<GameOverEvent>(OnGameOver);
    EventBus.Unsubscribe<FirstTapEvent>(OnFirstTap);
  }

  private void ResetPlayerState() {
    _isGameActive = false;
    ToggleComponents(false);
    _input.enabled = false;

    transform.position = _startPosition;
    _movement.ResetVelocity();
    _scaler.ResetSize();
    _input.ResetFirstTap();
  }

  private void OnGameStarted(GameStartedEvent e) {
    _isGameActive = false;
    _input.enabled = true;
  }

  private void OnFirstTap(FirstTapEvent e) {
    _isGameActive = true;
    ToggleComponents(true);
  }

  private void OnGameOver(GameOverEvent e) {
    _isGameActive = false;
    ToggleComponents(false);
    _input.enabled = false;
  }

  private void ToggleComponents(bool isActive) {
    _movement.enabled = isActive;
    _scaler.enabled = isActive;
  }

  private void CheckForOutOfBounds() { 
    float y = transform.position.y;
    float x = transform.position.x;
    if (y < _verticalBounds.x || y > _verticalBounds.y || x < _horizontalBounds.x || x > _horizontalBounds.y) { 
      if (!_isGameActive) return;
      _isGameActive = false; 
      EventBus.Raise(new PlayerHitEvent { DamageAmount = 999 }); 
    } 
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (!_isGameActive) return;
    if (collision.gameObject.CompareTag("Obstacle")) {
      _isGameActive = false; EventBus.Raise(new PlayerHitEvent { DamageAmount = 1 });
    } 
  }
}