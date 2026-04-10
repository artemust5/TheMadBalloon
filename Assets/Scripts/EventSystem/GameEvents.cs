using UnityEngine;

public struct PlayButtonPressedEvent { }
public struct SettingsButtonPressedEvent { }
public struct CloseSettingsPressedEvent { }
public struct PauseButtonPressedEvent { }
public struct ResumeButtonPressedEvent { }
public struct QuitToMenuButtonPressedEvent { }
public struct RestartButtonPressedEvent { }

public struct NewGameEvent { }
public struct GameStartedEvent { }
public struct ScoreChangedEvent {
  public int NewScore;
}
public struct ScorePointEvent { }
public struct InfoButtonPressedEvent { }
public struct CloseInfoPressedEvent { }
public struct UIButtonClickedEvent { }
public struct MusicMuteEvent {
  public bool isMuted;
}
public struct SfxMuteEvent {
  public bool isMuted;
}
public struct PlayerHitEvent {
  public int DamageAmount;
}
public struct GameOverEvent {
  public int FinalScore;
  public int HighScore;
  public int CoinsEarned;
}
public struct PlayerTotalCoinsChangedEvent {
  public int NewTotalCoins;
}
public struct FirstTapEvent { }
public struct ShopButtonPressedEvent { }
public struct CloseShopPressedEvent { }
public struct SkinEquippedEvent {
  public Sprite NewSkinSprite;
}
