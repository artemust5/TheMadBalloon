using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour {
  [Header("UI References")]
  [SerializeField] private Slider _loadingSlider; 

  [Header("Settings")]
  [SerializeField] private float _minLoadTime = 2.0f;
  [SerializeField] private string _sceneToLoad = "MainScene";

  void Start() {
    if (_loadingSlider != null) {
      _loadingSlider.value = 0f;
    }

    StartCoroutine(LoadSceneRoutine());
  }

  private IEnumerator LoadSceneRoutine() {
    AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneToLoad);

    operation.allowSceneActivation = false;

    float timer = 0f;

    while (!operation.isDone) {
      timer += Time.deltaTime;

      float progress = Mathf.Clamp01(timer / _minLoadTime);

      if (_loadingSlider != null) {
        _loadingSlider.value = progress;
      }

      if (operation.progress >= 0.9f && progress >= 1.0f) {
        operation.allowSceneActivation = true;
      }

      yield return null;
    }
  }
}