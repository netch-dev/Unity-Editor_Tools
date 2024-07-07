using System.IO;
using UnityEditor;
using UnityEngine;

namespace Netch.UtilityScripts {
	public class BatchRenameToolWindow : EditorWindow {
		private string batchName = "";
		private string batchNumber = "";

		[MenuItem("Window/Batch Rename")]
		public static void ShowWindow() {
			EditorWindow editorWindow = GetWindow(typeof(BatchRenameToolWindow));
			editorWindow.maxSize = new Vector2(500, 150);
			editorWindow.minSize = editorWindow.maxSize;

			GUIContent guiContent = new GUIContent();
			guiContent.text = "Batch Rename";
			editorWindow.titleContent = guiContent;

			editorWindow.Show();
		}

		private void OnGUI() {
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Step 1: Select objects in the hierarchy", EditorStyles.boldLabel);
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Step 3: Click the rename button", EditorStyles.boldLabel);

			EditorGUILayout.BeginHorizontal(); // Change the layout to horizontal, so the text fields are next to each other
			EditorGUILayout.LabelField("\tEnter name for batch");
			batchName = EditorGUILayout.TextField(batchName);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("\tEnter starting number");
			batchNumber = EditorGUILayout.TextField(batchNumber);
			EditorGUILayout.Space();
			EditorGUILayout.EndHorizontal();
			Repaint();
		}
	}
}
