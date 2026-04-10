using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class UILever : MonoBehaviour {
  [SerializeField] private Sprite _offSprite;
  [SerializeField] private Sprite _onSprite;

  public UnityEvent<bool> OnLeverToggled;

  private Image _image;
  private Button _button;
  private bool _isOn;

  private void Awake() {
    _image = GetComponent<Image>();
    _button = GetComponent<Button>();
    _button.onClick.AddListener(Toggle);
  }

  private void OnDestroy() {
    _button.onClick.RemoveListener(Toggle);
  }

  public void Toggle() {
    _isOn = !_isOn;
    UpdateVisuals();

    OnLeverToggled.Invoke(_isOn);
  }

  private void UpdateVisuals() {
    _image.sprite = _isOn ? _onSprite : _offSprite;
  }

  public void SetStateWithoutNotify(bool state) {
    _isOn = state;
    UpdateVisuals();
  }
}