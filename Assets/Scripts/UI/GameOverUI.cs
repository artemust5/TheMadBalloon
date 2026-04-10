using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour {
  [SerializeField] private Button _restartButton;
  [SerializeField] private Button _quitToMenuButton;

  [SerializeField] private TextMeshProUGUI _finalScoreText;
  [SerializeField] private TextMeshProUGUI _coinsEarnedText;
  [SerializeField] private TextMeshProUGUI _highScoreText;

  private void Awake() {
    _restartButton.onClick.AddListener(HandleRestartButton);
    _quitToMenuButton.onClick.AddListener(HandleQuitToMenuButton);
  }

  private void OnDestroy() {
    _restartButton.onClick.RemoveListener(HandleRestartButton);
    _quitToMenuButton.onClick.RemoveListener(HandleQuitToMenuButton);
  }

  private void HandleRestartButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new RestartButtonPressedEvent());
  }

  private void HandleQuitToMenuButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new QuitToMenuButtonPressedEvent());
  }

  public void ShowResults(int finalScore, int highScore, int coinsEarned) {
    _finalScoreText.text = "SCORE: " + finalScore.ToString("D6");
    _highScoreText.text = "HIGH: " + highScore.ToString("D6");
    _coinsEarnedText.text = "COINS: +" + coinsEarned.ToString();
  }
}