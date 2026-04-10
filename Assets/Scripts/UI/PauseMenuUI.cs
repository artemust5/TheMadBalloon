using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
  [SerializeField] private Button _resumeButton;
  [SerializeField] private Button _settingsButton;
  [SerializeField] private Button _quitToMenuButton;

  private void Awake() {
    _resumeButton.onClick.AddListener(HandleResumeButton);
    _settingsButton.onClick.AddListener(HandleSettingsButton);
    _quitToMenuButton.onClick.AddListener(HandleQuitToMenuButton);
  }

  private void OnDestroy() {
    _resumeButton.onClick.RemoveListener(HandleResumeButton);
    _settingsButton.onClick.RemoveListener(HandleSettingsButton);
    _quitToMenuButton.onClick.RemoveListener(HandleQuitToMenuButton);
  }

  private void HandleResumeButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new ResumeButtonPressedEvent());
  }

  private void HandleSettingsButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new SettingsButtonPressedEvent());
  }

  private void HandleQuitToMenuButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new QuitToMenuButtonPressedEvent());
  }
}