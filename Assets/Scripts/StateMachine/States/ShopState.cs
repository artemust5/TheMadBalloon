using UnityEngine;

public class ShopState : IState {
  private GameObject _shopCanvas;

  public ShopState(GameObject shopCanvas) {
    _shopCanvas = shopCanvas;
  }

  public void Enter() {
    _shopCanvas.SetActive(true);
  }

  public void Execute() { }

  public void Exit() {
    _shopCanvas.SetActive(false);
  }
}