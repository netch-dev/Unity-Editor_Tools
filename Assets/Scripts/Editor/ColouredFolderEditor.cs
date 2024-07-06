using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Netch.UtilityScripts {
	[InitializeOnLoad] // Attribute to edit folders whenever we load unity or anything changes
	internal static class ColouredFolderEditor {
		private static string _iconName;
		static ColouredFolderEditor() {
			EditorApplication.projectWindowItemOnGUI -= OnGUI;
			EditorApplication.projectWindowItemOnGUI += OnGUI;
		}

		private static void OnGUI(string guid, Rect selectionRect) {
			Color backgroundColour;
			Rect folderRect = GetFolderRect(selectionRect, out backgroundColour);

			/*			if (Selection.activeObject == null) return;

						string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
						string activeObjectGuid = AssetDatabase.GUIDFromAssetPath(assetPath).ToString();
						if (activeObjectGuid != guid) return;*/

			string iconGuid = EditorPrefs.GetString(guid, "");
			if (string.IsNullOrEmpty(iconGuid) || iconGuid == "0000000000000000f000000000000000") return;

			EditorGUI.DrawRect(folderRect, backgroundColour); // Draw a dark background to cover the default icon

			string folderTexturePath = AssetDatabase.GUIDToAssetPath(iconGuid);
			Texture2D folderTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(folderTexturePath);
			GUI.DrawTexture(folderRect, folderTexture);

			//EditorGUI.DrawRect(selectionRect, Color.red);
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
			//string folderGuid = AssetDatabase.GUIDFromAssetPath(folderPath).ToString();

			string iconPath = $"Assets/Icons/Colored/{newIconName}.png";
			string iconGuid = AssetDatabase.GUIDFromAssetPath(iconPath).ToString();

			EditorPrefs.SetString(folderGuid, iconGuid);

			//_iconName = newIconName;
		}

		public static void ResetIconName() {
			string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			string folderGuid = AssetDatabase.AssetPathToGUID(folderPath);
			//string folderGuid = AssetDatabase.GUIDFromAssetPath(folderPath).ToString();

			EditorPrefs.DeleteKey(folderGuid);
		}
	}
}
#endif
