using UnityEngine;

// Menu Item - https://docs.unity3d.com/ScriptReference/MenuItem.html

#if UNITY_EDITOR
using UnityEditor;

namespace Netch.UtilityScripts {
	internal static class MenuItems {

		private const int rightClickMenuPriority = 100000; // High value so it's at the bottom of the list closest to the mouse. Use -1 to put it at the top
		private const int separatorPriorityDifference = 11; // Need at least 11 priority difference to add a line separator between menu items

		[MenuItem("Assets/UtilityScripts/Red", false, rightClickMenuPriority)]
		public static void Red() {
			ColouredFolderEditor.SetIconName("Red");
		}

		[MenuItem("Assets/UtilityScripts/Green", false, rightClickMenuPriority)]
		public static void Green() {
			ColouredFolderEditor.SetIconName("Green");
		}

		[MenuItem("Assets/UtilityScripts/Blue", false, rightClickMenuPriority)]
		public static void Blue() {
			ColouredFolderEditor.SetIconName("Blue");
		}

		[MenuItem("Assets/UtilityScripts/Custom Icon...", false, rightClickMenuPriority + separatorPriorityDifference)]
		public static void Custom() {
			IconFolderEditor.ChooseCustomIcon();
		}

		[MenuItem("Assets/UtilityScripts/Reset Icon", false, rightClickMenuPriority + (separatorPriorityDifference * 2))]
		public static void ResetIcon() {
			ColouredFolderEditor.ResetIconName();
		}

		[MenuItem("Assets/UtilityScripts/Red", true)]
		[MenuItem("Assets/UtilityScripts/Green", true)]
		[MenuItem("Assets/UtilityScripts/Blue", true)]
		[MenuItem("Assets/UtilityScripts/Custom Icon...", true)]
		[MenuItem("Assets/UtilityScripts/Reset Icon", true)]
		public static bool ValidateFolder() {
			if (Selection.activeObject == null) return false;

			Object selectedObject = Selection.activeObject;

			string objectPath = AssetDatabase.GetAssetPath(selectedObject);
			return AssetDatabase.IsValidFolder(objectPath);
		}

		// Create a new gameobject from a menu item
		[MenuItem("GameObject/Custom GO")]
		public static void CreateCustomGO() {
			GameObject go = new GameObject("Custom GO");

			Rigidbody rb = go.AddComponent<Rigidbody>();
			rb.mass = 225;
		}

		// Update the mass of the selected Rigidbody
		[MenuItem("CONTEXT/Rigidbody/Triple Mass")]
		public static void TripleMass(MenuCommand menuCommand) {
			Rigidbody rb = menuCommand.context as Rigidbody;
			rb.mass *= 3;
		}
	}
}
#endif
