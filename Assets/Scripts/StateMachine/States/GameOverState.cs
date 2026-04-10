using UnityEngine;

public class GameOverState : IState {
  private GameObject _gameOverCanvas;
  private GameObject _gameWorld;
  private GameObject _gameHUD;

  private int _finalScore;
  private int _highScore;
  private int _coinsEarned;

  public GameOverState(GameObject gameOverCanvas, GameObject gameWorld, GameObject gameHUD) {
    _gameOverCanvas = gameOverCanvas;
    _gameWorld = gameWorld;
    _gameHUD = gameHUD;
  }

  public void SetResults(int finalScore, int highScore, int coinsEarned) {
    _finalScore = finalScore;
    _highScore = highScore;
    _coinsEarned = coinsEarned;
  }

  public void Enter() {
    _gameOverCanvas.SetActive(true);
    _gameWorld.SetActive(false);
    _gameHUD.SetActive(false); 
    Time.timeScale = 1f;

    GameOverUI uiController = _gameOverCanvas.GetComponentInChildren<GameOverUI>();
    if (uiController != null) {
      uiController.ShowResults(_finalScore, _highScore, _coinsEarned);
    }
  }

  public void Execute() { }

  public void Exit() {
    _gameOverCanvas.SetActive(false);
  }
}