using PlasticGui.WorkspaceWindow.Locks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AutoSaveEditor : EditorWindow {
	private const string menuOption = "File/Autosave";
	private static EditorWindow window;

	private int selectedChoiceIndex;
	private const string ONE_SECOND = "1 sec";
	private const string THIRTY_SECONDS = "30 sec";
	private const string ONE_MINUTE = "1 min";
	private const string FIVE_MINUTES = "5 min";
	private string[] choices = { ONE_SECOND, THIRTY_SECONDS, ONE_MINUTE, FIVE_MINUTES };
	private float saveTime = 1;
	private float nextSave = 0;

	public static bool IsAutoSaveEnabled {
		get => EditorPrefs.GetBool(menuOption, false);
		set => EditorPrefs.SetBool(menuOption, value);
	}

	public static bool IsDebugLoggingEnabled {
		get => EditorPrefs.GetBool("EnableDebugLogs", false);
		set => EditorPrefs.SetBool("EnableDebugLogs", value);
	}


	[MenuItem(menuOption, false, 175)]
	public static void ToggleAutoSave() {
		IsAutoSaveEnabled = !IsAutoSaveEnabled;
		if (IsAutoSaveEnabled) {
			ShowWindow();
		} else {
			CloseWindow();
		}
	}

	[MenuItem(menuOption, true)]
	private static bool ToggleAutoSaveValidate() {
		Menu.SetChecked(menuOption, IsAutoSaveEnabled);
		return true;
	}

	private static void ShowWindow() {
		window = GetWindow(typeof(AutoSaveEditor));
		GUIContent guiContent = new GUIContent("Auto Save Settings");
		window.titleContent = guiContent;
		window.Show();
	}

	private static void CloseWindow() {
		if (window != null) {
			window.Close();
		}
	}

	private void OnGUI() {
		EditorGUILayout.LabelField("Interval: ");
		EditorGUILayout.Space();

		EditorGUI.BeginChangeCheck();
		selectedChoiceIndex = EditorGUILayout.Popup("", selectedChoiceIndex, choices);
		if (EditorGUI.EndChangeCheck()) {
			switch (choices[selectedChoiceIndex]) {
				case ONE_SECOND:
					saveTime = 1;
					break;

				case THIRTY_SECONDS:
					saveTime = 30;
					break;

				case ONE_MINUTE:
					saveTime = 60;
					break;

				case FIVE_MINUTES:
					saveTime = 300;
					break;
			}

			nextSave = (float)EditorApplication.timeSinceStartup + saveTime;
		}

		IsDebugLoggingEnabled = EditorGUILayout.Toggle("Enable Debug Logs", IsDebugLoggingEnabled);
		EditorPrefs.SetBool("EnableDebugLogs", IsDebugLoggingEnabled);

		if (IsAutoSaveEnabled) {
			if (EditorApplication.timeSinceStartup > nextSave) {
				string[] path = EditorSceneManager.GetActiveScene().path.Split('/');
				bool saveSuccess = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), string.Join("/", path));
				nextSave = (float)EditorApplication.timeSinceStartup + saveTime;
				if (saveSuccess) {
					if (IsDebugLoggingEnabled) Debug.Log("Auto Save: Saved successfully");
				} else {
					if (IsDebugLoggingEnabled) Debug.LogError("Auto Save: Could not be saved");
				}
			}
		}
		Repaint();
	}
}
