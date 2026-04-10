using UnityEngine;

public class GameplayState : IState {
  private GameObject _gameWorld;
  private GameObject _gameHUD;

  public GameplayState(GameObject gameWorld, GameObject gameHUD) {
    _gameWorld = gameWorld;
    _gameHUD = gameHUD;
  }

  public void Enter() {
    if (!_gameWorld.activeSelf) {
      _gameHUD.SetActive(true);
      _gameWorld.SetActive(true);
      EventBus.Raise(new GameStartedEvent());
    }
  }

  public void Execute() {

  }

  public void Exit() {

  }
}