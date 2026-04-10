using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MonoBehaviour {
  [SerializeField] private Button _closeButton;
  [SerializeField] private UILever _musicLever; 
  [SerializeField] private UILever _sfxLever; 

  private void Awake() {
    _closeButton.onClick.AddListener(HandleCloseButton);

    _musicLever.OnLeverToggled.AddListener(OnMusicLeverToggled);
    _sfxLever.OnLeverToggled.AddListener(OnSfxLeverToggled);
  }

  private void OnEnable() {
    PlayerData data = SaveManager.LoadData();

    _musicLever.SetStateWithoutNotify(!data.musicMuted);
    _sfxLever.SetStateWithoutNotify(!data.sfxMuted);
  }

  private void OnDestroy() {
    _closeButton.onClick.RemoveListener(HandleCloseButton);
    _musicLever.OnLeverToggled.RemoveListener(OnMusicLeverToggled);
    _sfxLever.OnLeverToggled.RemoveListener(OnSfxLeverToggled);
  }

  private void HandleCloseButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new CloseSettingsPressedEvent());
  }

  private void OnMusicLeverToggled(bool isOn) {
    EventBus.Raise(new UIButtonClickedEvent());

    bool isMuted = !isOn; 

    EventBus.Raise(new MusicMuteEvent { isMuted = isMuted });

    PlayerData data = SaveManager.LoadData();
    data.musicMuted = isMuted;
    SaveManager.SaveData(data);
  }

  private void OnSfxLeverToggled(bool isOn) {
    EventBus.Raise(new UIButtonClickedEvent()); 

    bool isMuted = !isOn;

    EventBus.Raise(new SfxMuteEvent { isMuted = isMuted });

    PlayerData data = SaveManager.LoadData();
    data.sfxMuted = isMuted;
    SaveManager.SaveData(data);
  }
}