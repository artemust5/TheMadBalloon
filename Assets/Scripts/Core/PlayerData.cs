using System.Collections.Generic;
using UnityEngine.Scripting;

[System.Serializable]
[Preserve]
public class PlayerData {
  public bool musicMuted;
  public bool sfxMuted;
  public int highScore;
  public int totalCoins;

  public List<string> purchasedSkinIDs; 
  public string equippedSkinID;      

  public PlayerData() {
    musicMuted = false;
    sfxMuted = false;
    highScore = 0;
    totalCoins = 0;

    purchasedSkinIDs = new List<string> { "default_skin" };
    equippedSkinID = "default_skin";
  }
}