using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialHintUI : MonoBehaviour {
  [SerializeField] private GameObject _hintPanelRoot;
  [SerializeField] private Image _fingerIcon;
  [SerializeField] private TextMeshProUGUI _swipeText;

  [SerializeField] private float _animDuration = 1.8f;
  [SerializeField] private float _animStartX = -150f;
  [SerializeField] private float _animEndX = 150f;

  private Sequence _animSequence;

  private void Awake() {
    _hintPanelRoot.SetActive(false);

    EventBus.Subscribe<GameStartedEvent>(OnGameStarted);
    EventBus.Subscribe<FirstTapEvent>(OnFirstTap);
    EventBus.Subscribe<GameOverEvent>(OnGameOver);
  }

  private void OnDestroy() {
    EventBus.Unsubscribe<GameStartedEvent>(OnGameStarted);
    EventBus.Unsubscribe<FirstTapEvent>(OnFirstTap);
    EventBus.Unsubscribe<GameOverEvent>(OnGameOver);
    _animSequence?.Kill();
  }

  private void OnGameStarted(GameStartedEvent e) {
    _hintPanelRoot.SetActive(true);
    StartHintAnimation();
  }

  private void OnFirstTap(FirstTapEvent e) {
    _animSequence?.Kill();
    _hintPanelRoot.SetActive(false);
  }

  private void OnGameOver(GameOverEvent e) {
    _animSequence?.Kill();
    _hintPanelRoot.SetActive(false);
  }

  private void StartHintAnimation() {
    RectTransform fingerRect = _fingerIcon.GetComponent<RectTransform>();

    fingerRect.anchoredPosition = new Vector2(_animStartX, fingerRect.anchoredPosition.y);
    _fingerIcon.color = new Color(1, 1, 1, 1);

    _animSequence = DOTween.Sequence();

    _animSequence.Append(fingerRect.DOAnchorPosX(_animEndX, _animDuration)
        .SetEase(Ease.Linear));

    _animSequence.Join(_fingerIcon.DOFade(0f, _animDuration)
        .SetEase(Ease.InCubic));

    _animSequence.SetLoops(-1);
  }
}