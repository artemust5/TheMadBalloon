using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour {
  [SerializeField] private Image _skinIcon;
  [SerializeField] private TextMeshProUGUI _priceText;
  [SerializeField] private GameObject _priceObject;
  [SerializeField] private Button _buyButton;
  [SerializeField] private Button _equipButton;
  [SerializeField] private GameObject _equippedIndicator;

  private SkinData _skin;
  private ShopUI _shop;

  public SkinData Skin => _skin;

  private void Awake() {
    _buyButton.onClick.AddListener(OnBuy);
    _equipButton.onClick.AddListener(OnEquip);
  }

  public void Initialize(SkinData skin, ShopUI shop) {
    _skin = skin;
    _shop = shop;
    _skinIcon.sprite = skin.SkinSprite;
    _priceText.text = skin.Price.ToString();
  }

  public void UpdateStatus(bool isPurchased, bool isEquipped) {
    _buyButton.gameObject.SetActive(!isPurchased);
    _priceObject.SetActive(!isPurchased);

    _equipButton.gameObject.SetActive(isPurchased && !isEquipped);
    _equippedIndicator.SetActive(isEquipped);
  }

  private void OnBuy() {
    EventBus.Raise(new UIButtonClickedEvent());
    _shop.TryBuySkin(_skin);
  }

  private void OnEquip() {
    EventBus.Raise(new UIButtonClickedEvent());
    _shop.EquipSkin(_skin);
  }
}