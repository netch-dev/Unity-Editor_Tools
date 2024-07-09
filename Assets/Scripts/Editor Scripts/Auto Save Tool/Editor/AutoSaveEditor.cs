using UnityEditor;
using UnityEngine;

public class AutoSaveEditor : EditorWindow {
	private const string menuOption = "File/Autosave";
	private static EditorWindow window;
	public static bool IsEnabled {
		get => EditorPrefs.GetBool(menuOption, false);
		set => EditorPrefs.SetBool(menuOption, value);
	}

	[MenuItem(menuOption, false, 175)]
	public static void ToggleAutoSave() {
		IsEnabled = !IsEnabled;
		if (IsEnabled) {
			ShowWindow();
		} else {
			CloseWindow();
		}
	}

	[MenuItem(menuOption, true)]
	private static bool ToggleAutoSaveValidate() {
		Menu.SetChecked(menuOption, IsEnabled);
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
}
