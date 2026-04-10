using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour {
  public bool IsTapHeld { get; private set; }
  public float TargetWorldX { get; private set; }

  [SerializeField] private Camera _mainCamera;

  private bool _firstTapDone = false;

  private void OnEnable() {
    if (_mainCamera == null) _mainCamera = Camera.main;
    TargetWorldX = transform.position.x;
  }

  void Update() {
    bool isPointerOverUI = false;
    if (Input.touchCount > 0) {
      isPointerOverUI = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
    }
    else if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
      isPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    if (!_firstTapDone) {
      bool isFirstTapDown = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0);

      if (isFirstTapDown && !isPointerOverUI) {
        _firstTapDone = true;
        EventBus.Raise(new FirstTapEvent());
      }
      else {
        IsTapHeld = false;
        TargetWorldX = transform.position.x;
        return;
      }
    }

    bool isInputActive = Input.touchCount > 0 || Input.GetMouseButton(0);

    if (isInputActive && !isPointerOverUI) {
      IsTapHeld = true;

      Vector2 screenPos = (Input.touchCount > 0) ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
      Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(screenPos);
      TargetWorldX = worldPoint.x;
    }
    else {
      IsTapHeld = false;
      TargetWorldX = transform.position.x; 
    }
  }

  public void ResetFirstTap() {
    _firstTapDone = false;
  }
}