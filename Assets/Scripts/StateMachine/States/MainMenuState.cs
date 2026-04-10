using UnityEngine;

public class MainMenuState : IState {
  private GameObject _mainMenuCanvas;
  private GameObject _gameWorld;
  private GameObject _gameHUD;

  public MainMenuState(GameObject mainMenuCanvas, GameObject gameWorld, GameObject gameHUD) {
    _mainMenuCanvas = mainMenuCanvas;
    _gameWorld = gameWorld;
    _gameHUD = gameHUD;
  }

  public void Enter() {
    _mainMenuCanvas.SetActive(true);
    Time.timeScale = 1f;

    _gameWorld.SetActive(false);
    _gameHUD.SetActive(false);
  }

  public void Execute() { }

  public void Exit() {
    _mainMenuCanvas.SetActive(false);
  }
}