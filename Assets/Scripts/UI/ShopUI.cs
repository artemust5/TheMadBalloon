using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour {
  [SerializeField] private List<SkinData> _allSkins; 
  [SerializeField] private GameObject _skinItemPrefab; 
  [SerializeField] private Transform _skinListContainer; 

  [SerializeField] private TextMeshProUGUI _totalCoinsText;
  [SerializeField] private Button _closeButton;

  private PlayerData _playerData;
  private List<ShopItemUI> _uiItems = new List<ShopItemUI>();

  private void Awake() {
    _closeButton.onClick.AddListener(HandleCloseButton);
    EventBus.Subscribe<PlayerTotalCoinsChangedEvent>(OnTotalCoinsChanged);

    InstantiateShopItems();
  }

  private void OnEnable() {
    _playerData = SaveManager.LoadData();
    RefreshShopItems();
  }

  private void OnDestroy() {
    _closeButton.onClick.RemoveListener(HandleCloseButton);
    EventBus.Unsubscribe<PlayerTotalCoinsChangedEvent>(OnTotalCoinsChanged);
  }

  private void HandleCloseButton() {
    EventBus.Raise(new UIButtonClickedEvent());
    EventBus.Raise(new CloseShopPressedEvent());
  }

  private void OnTotalCoinsChanged(PlayerTotalCoinsChangedEvent e) {
    _totalCoinsText.text = e.NewTotalCoins.ToString();
  }

  private void InstantiateShopItems() {
    foreach (SkinData skin in _allSkins) {
      GameObject itemGO = Instantiate(_skinItemPrefab, _skinListContainer);
      ShopItemUI itemUI = itemGO.GetComponent<ShopItemUI>();
      itemUI.Initialize(skin, this);
      _uiItems.Add(itemUI);
    }
  }

  private void RefreshShopItems() {
    _totalCoinsText.text = _playerData.totalCoins.ToString();

    foreach (ShopItemUI item in _uiItems) {
      bool isPurchased = _playerData.purchasedSkinIDs.Contains(item.Skin.SkinID);
      bool isEquipped = _playerData.equippedSkinID == item.Skin.SkinID;
      item.UpdateStatus(isPurchased, isEquipped);
    }
  }

  public void TryBuySkin(SkinData skin) {
    if (_playerData.totalCoins >= skin.Price) {
      _playerData.totalCoins -= skin.Price;
      _playerData.purchasedSkinIDs.Add(skin.SkinID);
      SaveManager.SaveData(_playerData);

      EventBus.Raise(new PlayerTotalCoinsChangedEvent { NewTotalCoins = _playerData.totalCoins });
      RefreshShopItems();
    }
  }

  public void EquipSkin(SkinData skin) {
    _playerData.equippedSkinID = skin.SkinID;
    SaveManager.SaveData(_playerData);

    EventBus.Raise(new SkinEquippedEvent { NewSkinSprite = skin.SkinSprite });
    RefreshShopItems();
  }
}