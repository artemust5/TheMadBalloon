using UnityEngine;

public class GameSessionManager : MonoBehaviour {
  [SerializeField] private float _scoreToCoinCoefficient = 0.1f;

  private int _currentScore;
  private bool _isGameActive = false;

  private void OnEnable() {
    UnsubscribeAll();
    ResetSessionState();

    EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
    EventBus.Subscribe<PlayerHitEvent>(OnPlayerHit);
    EventBus.Subscribe<ScorePointEvent>(OnScorePoint);
    EventBus.Subscribe<FirstTapEvent>(OnFirstTap);
  }

  private void OnDisable() {
    UnsubscribeAll();
  }

  private void UnsubscribeAll() {
    EventBus.Unsubscribe<GameStartedEvent>(OnGameStarted);
    EventBus.Unsubscribe<PlayerHitEvent>(OnPlayerHit);
    EventBus.Unsubscribe<FirstTapEvent>(OnFirstTap);
    EventBus.Unsubscribe<ScorePointEvent>(OnScorePoint);
  }

  private void ResetSessionState() {
    _isGameActive = false;
    _currentScore = 0;

    EventBus.Raise(new ScoreChangedEvent { NewScore = 0 });
  }

  private void OnGameStarted(GameStartedEvent e) {
    _isGameActive = false; 
  }

  private void OnFirstTap(FirstTapEvent e) {
    _isGameActive = true; 
  }

  private void OnScorePoint(ScorePointEvent e) {
    if (!_isGameActive) return;

    _currentScore += 1;
    EventBus.Raise(new ScoreChangedEvent { NewScore = _currentScore });
  }

  private void OnPlayerHit(PlayerHitEvent e) { 
    if (!_isGameActive) return;
    _isGameActive = false; int coinsEarned = Mathf.FloorToInt(_currentScore * _scoreToCoinCoefficient); 
    PlayerData data = SaveManager.LoadData();
    int highScore = data.highScore;
    if (_currentScore > highScore) {
      data.highScore = _currentScore; 
      highScore = _currentScore;
    } 
    data.totalCoins += coinsEarned;
    SaveManager.SaveData(data);
    EventBus.Raise(new PlayerTotalCoinsChangedEvent { NewTotalCoins = data.totalCoins });
    EventBus.Raise(new GameOverEvent { FinalScore = _currentScore, HighScore = highScore, CoinsEarned = coinsEarned }); 
  }
}