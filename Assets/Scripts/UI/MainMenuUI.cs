using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
  [SerializeField] private Button _playButton;
  [SerializeField] private Button _settingsButton;
  [SerializeField] private Button _shopButton;
  [SerializeField] private Button _infoButton;

  private void Awake() {
    _playButton.onClick.AddListener(HandlePlayButton);
    _settingsButton.onClick.AddListener(HandleSettingsButton);
    _infoButton.onClick.AddListener(HandleInfoGameButton);
    _shopButton.onClick.AddListener(HandleShopButton);
  }

  private void OnDestroy() {
    _playButton.onClick.RemoveListener(HandlePlayButton);
    _settingsButton.onClick.RemoveListener(HandleSettingsButton);
    _infoButton.onClick.RemoveListener(HandleInfoGameButton);
    _shopButton.onClick.RemoveListener(HandleShopButton);
  }

  private void HandlePlayButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new PlayButtonPressedEvent());
  }
  private void HandleSettingsButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new SettingsButtonPressedEvent()); 
  }
  private void HandleInfoGameButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new InfoButtonPressedEvent());
  }
  private void HandleShopButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new ShopButtonPressedEvent());
  }
}