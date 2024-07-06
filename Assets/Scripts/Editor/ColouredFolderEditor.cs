using System;
using Unity.VisualScripting;
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
			Rect folderRect;
			Color backgroundColor = new Color(0.2f, 0.2f, 0.2f);

			if (selectionRect.x < 15) {
				// Second column, small scale
				folderRect = new Rect(selectionRect.x + 3, selectionRect.y, selectionRect.height, selectionRect.height);

			} else if (selectionRect.x >= 15 && selectionRect.height < 30) {
				// First column
				folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.height, selectionRect.height);
				backgroundColor = new Color(0.22f, 0.22f, 0.22f);

			} else {
				// Second column, large scale
				folderRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.width);
			}

			if (Selection.activeObject == null) return;

			string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			string activeObjectGuid = AssetDatabase.GUIDFromAssetPath(assetPath).ToString();
			if (activeObjectGuid != guid) return;

			EditorGUI.DrawRect(folderRect, backgroundColor); // Overlay a dark background to cover the previous icon
			Texture2D folderTexture = AssetDatabase.LoadAssetAtPath<Texture2D>($"Assets/Icons/Colored/{_iconName}.png");
			GUI.DrawTexture(folderRect, folderTexture);

			//EditorGUI.DrawRect(selectionRect, Color.red);
		}

		public static void SetIconName(string newIconName) {
			_iconName = newIconName;
		}
	}
}
#endif
