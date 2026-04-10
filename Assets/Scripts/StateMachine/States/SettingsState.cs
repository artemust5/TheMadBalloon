using UnityEngine;

public class SettingsState : IState {
  private GameObject _settingsMenuCanvas;
  private GameObject _hud;

  public SettingsState(GameObject settingsMenuCanvas, GameObject hud) {
    _settingsMenuCanvas = settingsMenuCanvas;
    _hud = hud;
  }

  public void Enter() {
    _settingsMenuCanvas.SetActive(true);
    _hud.SetActive(false);
  }

  public void Execute() { }

  public void Exit() {
    _settingsMenuCanvas.SetActive(false);
    _hud.SetActive(true);
  }
}