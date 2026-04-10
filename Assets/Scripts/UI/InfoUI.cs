using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour {
  [SerializeField] private Button _closeButton;

  private void Awake() {
    _closeButton.onClick.AddListener(HandleCloseButton);
  }

  private void OnDestroy() {
    _closeButton.onClick.RemoveListener(HandleCloseButton);
  }

  private void HandleCloseButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new CloseInfoPressedEvent());
  }
}