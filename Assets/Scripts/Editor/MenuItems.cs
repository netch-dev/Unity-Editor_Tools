using UnityEditor;
using UnityEngine;

// Menu Item - https://docs.unity3d.com/ScriptReference/MenuItem.html

#if UNITY_EDITOR
namespace Netch.UtilityScripts {
	internal static class MenuItems {

		private const int rightClickMenuPriority = 100000; // High value so it's at the bottom of the list closest to the mouse. Use -1 to put it at the top
		private const int separatorPriorityDifference = 11; // Need at least 11 priority difference to add a line separator between menu items

		[MenuItem("Assets/UtilityScripts/Red", false, rightClickMenuPriority)]
		private static void Red() {
			Debug.Log("Colouring the folder red");
		}

		[MenuItem("Assets/UtilityScripts/Green", false, rightClickMenuPriority)]
		private static void Green() {
			Debug.Log("Colouring the folder green");
		}

		[MenuItem("Assets/UtilityScripts/Blue", false, rightClickMenuPriority)]
		private static void Blue() {
			Debug.Log("Colouring the folder blue");
		}

		[MenuItem("Assets/UtilityScripts/Custom Icon...", false, rightClickMenuPriority + separatorPriorityDifference)]
		private static void Custom() {
			Debug.Log("Choosing a custom icon");
		}

		[MenuItem("Assets/UtilityScripts/Reset Icon", false, rightClickMenuPriority + (separatorPriorityDifference * 2))]
		private static void ResetIcon() {
			Debug.Log("Reset the folder icon");
		}

		[MenuItem("Assets/UtilityScripts/Red", true)]
		[MenuItem("Assets/UtilityScripts/Green", true)]
		[MenuItem("Assets/UtilityScripts/Blue", true)]
		[MenuItem("Assets/UtilityScripts/Custom Icon...", true)]
		[MenuItem("Assets/UtilityScripts/Reset Icon", true)]
		private static bool ValidateFolder() {
			if (Selection.activeObject == null) return false;

			Object selectedObject = Selection.activeObject;

			string objectPath = AssetDatabase.GetAssetPath(selectedObject);
			return AssetDatabase.IsValidFolder(objectPath);
		}
	}
}
#endif
