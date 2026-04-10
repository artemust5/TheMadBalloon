using UnityEngine;

public class PauseState : IState {
  private GameObject _pauseMenuCanvas;

  public PauseState(GameObject pauseMenuCanvas) {
    _pauseMenuCanvas = pauseMenuCanvas;
  }

  public void Enter() {
    _pauseMenuCanvas.SetActive(true);
  }

  public void Execute() {
  }

  public void Exit() {
    _pauseMenuCanvas.SetActive(false);
  }
}