using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Netch.UtilityScripts {
	[InitializeOnLoad] // Attribute to edit folders whenever we load unity or anything changes
	internal static class ColouredFolderEditor {
		static ColouredFolderEditor() {
			EditorApplication.projectWindowItemOnGUI -= OnGUI;
			EditorApplication.projectWindowItemOnGUI += OnGUI;
		}

		private static void OnGUI(string guid, Rect selectionRect) {
			Color backgroundColour;
			Rect folderRect = GetFolderRect(selectionRect, out backgroundColour);

			string iconGuid = UtilityScriptPrefs.GetString(guid, "");
			if (string.IsNullOrEmpty(iconGuid) || iconGuid == "0000000000000000f000000000000000") return;

			EditorGUI.DrawRect(folderRect, backgroundColour); // Draw a dark background to cover the default icon

			string folderTexturePath = AssetDatabase.GUIDToAssetPath(iconGuid);
			Texture2D folderTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(folderTexturePath);
			GUI.DrawTexture(folderRect, folderTexture);
		}

		private static Rect GetFolderRect(Rect selectionRect, out Color backgroundColour) {
			Rect folderRect;
			backgroundColour = new Color(0.2f, 0.2f, 0.2f);

			if (selectionRect.x < 15) {
				// Second column, small scale
				folderRect = new Rect(selectionRect.x + 3, selectionRect.y, selectionRect.height, selectionRect.height);

			} else if (selectionRect.x >= 15 && selectionRect.height < 30) {
				// First column
				folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.height, selectionRect.height);
				backgroundColour = new Color(0.22f, 0.22f, 0.22f);

			} else {
				// Second column, large scale
				folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.width);
			}

			return folderRect;
		}

		public static void SetIconName(string newIconName) {
			string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			string folderGuid = AssetDatabase.AssetPathToGUID(folderPath);

			string iconPath = $"Assets/Icons/Colored/{newIconName}.png";
			string iconGuid = AssetDatabase.GUIDFromAssetPath(iconPath).ToString();

			UtilityScriptPrefs.SetString(folderGuid, iconGuid);
		}

		public static void ResetIconName() {
			string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			string folderGuid = AssetDatabase.AssetPathToGUID(folderPath);

			UtilityScriptPrefs.DeleteKey(folderGuid);
		}
	}
}
#endif
