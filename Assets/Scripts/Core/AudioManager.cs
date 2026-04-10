using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
  [SerializeField] private AudioSource _musicSource;
  [SerializeField] private AudioSource _sfxSource;       

  [SerializeField] private List<AudioClip> _musicPlaylist;
  [SerializeField][Range(0f, 1f)] private float _musicVolume = 0.5f;

  [SerializeField] private AudioClip _buttonClickClip;
  [SerializeField] private AudioClip _scoreClip;       
  [SerializeField] private AudioClip _gameOverClip;    

  private int _currentTrackIndex = 0;

  private void Awake() {
    _musicSource.volume = _musicVolume;
    _musicSource.loop = false;

    PlayerData data = SaveManager.LoadData();
    UpdateMuteState(data);
  }

  private void OnEnable() {
    EventBus.Subscribe<UIButtonClickedEvent>(OnButtonClicked);
    EventBus.Subscribe<MusicMuteEvent>(OnMusicMute);
    EventBus.Subscribe<SfxMuteEvent>(OnSfxMute);
    EventBus.Subscribe<ScorePointEvent>(OnScorePoint);
    EventBus.Subscribe<PlayerHitEvent>(OnPlayerHit);
    EventBus.Subscribe<GameOverEvent>(OnGameOver);
  }

  private void OnDisable() {
    EventBus.Unsubscribe<UIButtonClickedEvent>(OnButtonClicked);
    EventBus.Unsubscribe<MusicMuteEvent>(OnMusicMute);
    EventBus.Unsubscribe<SfxMuteEvent>(OnSfxMute);
    EventBus.Unsubscribe<ScorePointEvent>(OnScorePoint);
    EventBus.Unsubscribe<PlayerHitEvent>(OnPlayerHit);
    EventBus.Unsubscribe<GameOverEvent>(OnGameOver);
  }


  private void OnScorePoint(ScorePointEvent e) {
    PlaySFX(_scoreClip);
  }

  private void OnPlayerHit(PlayerHitEvent e) {
    PlaySFX(_gameOverClip);
  }

  private void OnGameOver(GameOverEvent e) {
  }

  private void OnButtonClicked(UIButtonClickedEvent e) { 
    PlaySFX(_buttonClickClip);
  }

  private void OnMusicMute(MusicMuteEvent e) {
    _musicSource.mute = e.isMuted; 
  }

  private void OnSfxMute(SfxMuteEvent e) {
    _sfxSource.mute = e.isMuted;
  }

  private void PlaySFX(AudioClip clip) {
    if (clip != null) _sfxSource.PlayOneShot(clip);
  }

  private void Start() { if (_musicPlaylist.Count > 0) StartCoroutine(PlayMusicPlaylist()); }
  private IEnumerator PlayMusicPlaylist() { while (true) { _musicSource.clip = _musicPlaylist[_currentTrackIndex]; _musicSource.Play(); yield return new WaitForSeconds(_musicSource.clip.length); _currentTrackIndex = (_currentTrackIndex + 1) % _musicPlaylist.Count; } }

  private void UpdateMuteState(PlayerData data) {
    _musicSource.mute = data.musicMuted;
    _sfxSource.mute = data.sfxMuted;
  }
}