using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHUDUI : MonoBehaviour {
  [SerializeField] private Button _pauseButton;
  [SerializeField] private TextMeshProUGUI _scoreText;

  private void Awake() {
    EventBus.Subscribe<ScoreChangedEvent>(OnScoreChanged);
    _pauseButton.onClick.AddListener(HandlePauseButton);
  }

  private void OnDestroy() {
    EventBus.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
    _pauseButton.onClick.RemoveListener(HandlePauseButton);
  }

  private void OnScoreChanged(ScoreChangedEvent e) {
    if (_scoreText) _scoreText.text = e.NewScore.ToString("D6");
  }

  private void HandlePauseButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new PauseButtonPressedEvent());
  }
}