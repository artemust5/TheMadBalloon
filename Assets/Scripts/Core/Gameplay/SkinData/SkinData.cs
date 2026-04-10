using UnityEngine;

[CreateAssetMenu(fileName = "NewSkin", menuName = "Game/Skin Data")]
public class SkinData : ScriptableObject {
  [SerializeField] private string _skinID; 

  [SerializeField] private Sprite _skinSprite; 
  [SerializeField] private int _price;

  public string SkinID => _skinID;
  public Sprite SkinSprite => _skinSprite;
  public int Price => _price;
}