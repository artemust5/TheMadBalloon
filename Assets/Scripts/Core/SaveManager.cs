using UnityEngine;
using System.IO;

public static class SaveManager {
  private static string _savePath = Path.Combine(Application.persistentDataPath, "playerData.json");

  public static void SaveData(PlayerData data) {
    string json = JsonUtility.ToJson(data, true);
    File.WriteAllText(_savePath, json);
  }

  public static PlayerData LoadData() {
    if (File.Exists(_savePath)) {
      string json = File.ReadAllText(_savePath);
      return JsonUtility.FromJson<PlayerData>(json);
    }
    else {
      PlayerData newData = new PlayerData();
      SaveData(newData);
      return newData;
    }
  }

  public static void ClearData() {
    if (File.Exists(_savePath)) {
      File.Delete(_savePath);
      Debug.Log($"<color=orange>Save file deleted from:</color> {_savePath}");
    }
  }
}