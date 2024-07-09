using UnityEditor;
using UnityEngine;

public class AutoSaveEditor : EditorWindow {
	private const string menuOption = "File/Autosave";
	public static bool IsEnabled {
		get => EditorPrefs.GetBool(menuOption, false);
		set => EditorPrefs.SetBool(menuOption, value);
	}

	[MenuItem(menuOption, false, 175)]
	public static void ToggleAutoSave() {
		IsEnabled = !IsEnabled;
	}

	[MenuItem(menuOption, true)]
	private static bool ToggleAutoSaveValidate() {
		Menu.SetChecked(menuOption, IsEnabled);
		return true;
	}
}
