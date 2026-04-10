using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerSkinManager : MonoBehaviour {
  [SerializeField] private SpriteRenderer _playerSpriteRenderer;

  [SerializeField] private List<SkinData> _allSkins;

  private Dictionary<string, Sprite> _skinDatabase;

  private void Awake() {
    _skinDatabase = _allSkins.ToDictionary(skin => skin.SkinID, skin => skin.SkinSprite);

    EventBus.Subscribe<NewGameEvent>(OnNewGame);
    EventBus.Subscribe<SkinEquippedEvent>(OnSkinEquipped);
  }

  private void OnDestroy() {
    EventBus.Unsubscribe<NewGameEvent>(OnNewGame);
    EventBus.Unsubscribe<SkinEquippedEvent>(OnSkinEquipped);
  }

  private void Start() {
    ApplyEquippedSkin();
  }

  private void OnNewGame(NewGameEvent e) {
    ApplyEquippedSkin();
  }

  private void OnSkinEquipped(SkinEquippedEvent e) {
    _playerSpriteRenderer.sprite = e.NewSkinSprite;
  }

  private void ApplyEquippedSkin() {
    PlayerData data = SaveManager.LoadData();
    if (_skinDatabase.TryGetValue(data.equippedSkinID, out Sprite sprite)) {
      _playerSpriteRenderer.sprite = sprite;
    }
  }
}