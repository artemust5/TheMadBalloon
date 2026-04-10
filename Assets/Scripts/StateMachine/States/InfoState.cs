using UnityEngine;

public class InfoState : IState {
  private GameObject _infoCanvas;

  public InfoState(GameObject infoCanvas) {
    _infoCanvas = infoCanvas;
  }

  public void Enter() {
    _infoCanvas.SetActive(true);
  }

  public void Execute() { }

  public void Exit() {
    _infoCanvas.SetActive(false);
  }
}