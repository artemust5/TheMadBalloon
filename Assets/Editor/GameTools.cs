using UnityEditor;

public class GameTools {
  [MenuItem("Tools/Clear Player Data")]
  public static void ClearPlayerData() {
    if (EditorUtility.DisplayDialog(
        "Очистити дані гравця?",
        "Ви впевнені, що хочете видалити 'playerData.json'? Це неможливо буде скасувати.",
        "Так, видалити",
        "Скасувати")) {
      SaveManager.ClearData();
    }
  }
}