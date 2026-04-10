using UnityEngine;

public class GameManager : MonoBehaviour {
  [Header("UI Canvases")]
  [SerializeField] private GameObject _mainMenuCanvas;
  [SerializeField] private GameObject _settingsMenuCanvas;
  [SerializeField] private GameObject _gameHUDCanvas;
  [SerializeField] private GameObject _pauseMenuCanvas;
  [SerializeField] private GameObject _infoCanvas;
  [SerializeField] private GameObject _gameOverCanvas;
  [SerializeField] private GameObject _shopCanvas;

  [Header("Game World")]
  [SerializeField] private GameObject _gameWorld;

  private IState _currentState;
  private IState _previousState;

  private MainMenuState _mainMenuState;
  private GameplayState _gameplayState;
  private PauseState _pauseState;
  private SettingsState _settingsState;
  private InfoState _infoState;
  private GameOverState _gameOverState;
  private ShopState _shopState;

  void Start() {
    _mainMenuCanvas.SetActive(false);
    _settingsMenuCanvas.SetActive(false);
    _gameHUDCanvas.SetActive(false);
    _pauseMenuCanvas.SetActive(false);
    _infoCanvas.SetActive(false);
    _gameWorld.SetActive(false);
    _gameOverCanvas.SetActive(false);
    _shopCanvas.SetActive(false);

    _mainMenuState = new MainMenuState(_mainMenuCanvas, _gameWorld, _gameHUDCanvas);
    _gameplayState = new GameplayState(_gameWorld, _gameHUDCanvas);
    _pauseState = new PauseState(_pauseMenuCanvas);
    _settingsState = new SettingsState(_settingsMenuCanvas, _gameHUDCanvas);
    _infoState = new InfoState(_infoCanvas);
    _gameOverState = new GameOverState(_gameOverCanvas, _gameWorld, _gameHUDCanvas);
    _shopState = new ShopState(_shopCanvas);

    ChangeState(_mainMenuState);
  }

  private void OnEnable() {
    EventBus.Subscribe<PlayButtonPressedEvent>(HandlePlayButton);
    EventBus.Subscribe<PauseButtonPressedEvent>(HandlePauseButton);
    EventBus.Subscribe<ResumeButtonPressedEvent>(HandleResumeButton);
    EventBus.Subscribe<QuitToMenuButtonPressedEvent>(HandleQuitToMenuButton);
    EventBus.Subscribe<SettingsButtonPressedEvent>(HandleSettingsButton);
    EventBus.Subscribe<CloseSettingsPressedEvent>(HandleCloseSettingsButton);
    EventBus.Subscribe<RestartButtonPressedEvent>(HandleRestartButton);
    EventBus.Subscribe<InfoButtonPressedEvent>(HandleInfoButton);
    EventBus.Subscribe<CloseInfoPressedEvent>(HandleCloseInfoButton);
    EventBus.Subscribe<GameOverEvent>(HandleGameOver);
    EventBus.Subscribe<ShopButtonPressedEvent>(HandleShopButton);
    EventBus.Subscribe<CloseShopPressedEvent>(HandleCloseShopButton);
  }

  private void OnDisable() {
    EventBus.Unsubscribe<PlayButtonPressedEvent>(HandlePlayButton);
    EventBus.Unsubscribe<PauseButtonPressedEvent>(HandlePauseButton);
    EventBus.Unsubscribe<ResumeButtonPressedEvent>(HandleResumeButton);
    EventBus.Unsubscribe<QuitToMenuButtonPressedEvent>(HandleQuitToMenuButton);
    EventBus.Unsubscribe<SettingsButtonPressedEvent>(HandleSettingsButton);
    EventBus.Unsubscribe<CloseSettingsPressedEvent>(HandleCloseSettingsButton);
    EventBus.Unsubscribe<RestartButtonPressedEvent>(HandleRestartButton);
    EventBus.Unsubscribe<InfoButtonPressedEvent>(HandleInfoButton);
    EventBus.Unsubscribe<CloseInfoPressedEvent>(HandleCloseInfoButton);
    EventBus.Unsubscribe<GameOverEvent>(HandleGameOver);
    EventBus.Unsubscribe<ShopButtonPressedEvent>(HandleShopButton);
    EventBus.Unsubscribe<CloseShopPressedEvent>(HandleCloseShopButton);

  }

  void Update() { _currentState?.Execute(); }

  public void ChangeState(IState newState) {
    if (_currentState == newState) return;
    _currentState?.Exit();

    if (_currentState != _settingsState && _currentState != _infoState && _currentState != _shopState) {
      _previousState = _currentState;
    }

    _currentState = newState;
    _currentState.Enter();
  }

  private void HandleGameOver(GameOverEvent e) {
    _gameOverState.SetResults(e.FinalScore, e.HighScore, e.CoinsEarned);
    ChangeState(_gameOverState);
  }

  private void HandleShopButton(ShopButtonPressedEvent e) {
    if (_currentState is MainMenuState) {
      ChangeState(_shopState);
    }
  }

  private void HandleCloseShopButton(CloseShopPressedEvent e) {
    if (_previousState != null) {
      ChangeState(_previousState);
    }
    else {
      ChangeState(_mainMenuState);
    }
  }

  private void HandleInfoButton(InfoButtonPressedEvent e) {
    ChangeState(_infoState);
  }

  private void HandleCloseInfoButton(CloseInfoPressedEvent e) {
    if (_previousState != null) {
      ChangeState(_previousState);
    }
    else {
      ChangeState(_mainMenuState);
    }
  }

  private void HandlePlayButton(PlayButtonPressedEvent e) {
    EventBus.Raise(new NewGameEvent());
    ChangeState(_gameplayState);
  }

  private void HandlePauseButton(PauseButtonPressedEvent e) { 
    if (_currentState is GameplayState) {
      Time.timeScale = 0f;
      ChangeState(_pauseState);
    } 
  }

  private void HandleResumeButton(ResumeButtonPressedEvent e) {
    if (_currentState is PauseState) {
      Time.timeScale = 1f;
      ChangeState(_gameplayState); 
    } 
  }

  private void HandleQuitToMenuButton(QuitToMenuButtonPressedEvent e) {
    if (_currentState is PauseState) {
      _gameWorld.SetActive(false);
      _gameHUDCanvas.SetActive(false);
    }
    Time.timeScale = 1f;
    ChangeState(_mainMenuState);
  }

  private void HandleSettingsButton(SettingsButtonPressedEvent e) {
    ChangeState(_settingsState); 
  }

  private void HandleCloseSettingsButton(CloseSettingsPressedEvent e) {
    if (_previousState != null) {
      ChangeState(_previousState); 
    } else {
      ChangeState(_mainMenuState);
    } 
  }

  private void HandleRestartButton(RestartButtonPressedEvent e) { 
    Time.timeScale = 1f;
    HandlePlayButton(new PlayButtonPressedEvent());
  }
}