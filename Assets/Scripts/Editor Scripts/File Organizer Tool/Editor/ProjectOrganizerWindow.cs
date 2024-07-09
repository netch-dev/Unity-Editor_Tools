using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ProjectOrganizerWindow : EditorWindow {
	private int selectedTabIndex = 0;
	private string[] tabs = { "Organizer", "Asset Type Mappings" };
	private int countOfAssetTypeRows = 0;
	private List<AssetTypeRow> assetTypeRows;
	private int totalNumberOfFileExtensions;
	private bool isDirty;
	private string[] assetTypeNames;
	private int countOfOrganizerRows = 0;
	private List<OrganizerRow> organizerRows;

	private class OrganizerRow {
		public int selectedOptionIndex;
		public string folderPath;
		public Object obj;
	}

	private class AssetTypeRow {
		public string name;
		public string fileExtension;
	}

	private Dictionary<string, List<string>> assetTypes = new Dictionary<string, List<string>> {
		["Prefabs"] = new List<string> { ".prefab" },
		["Animations"] = new List<string> { ".anim" },
		["Images"] = new List<string> { ".png", ".jpeg" }
	};

	private void Awake() {
		InitializeFields();
	}

	[MenuItem("Custom Tools/Project Organizer Tool")]
	public static void ShowWindow() {
		EditorWindow window = GetWindow(typeof(ProjectOrganizerWindow));
		GUIContent guiContent = new GUIContent("Project Organizer Tool");
		window.titleContent = guiContent;
		window.Show();
	}

	private void OnGUI() {
		DrawToolbarTabs();
		EditorGUILayout.Space(20);

		if (selectedTabIndex == 0) { // Currently on the organizer tab
			if (isDirty) { // The user has made a change in the editor window
				isDirty = false;
				UpdateAssetTypes(assetTypeNames.Length);
			}
			for (int i = 0; i < countOfOrganizerRows; i++) {
				DrawOrganizerRow(i);
			}

			DrawAddAndRemoveControls();

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();

			if (GUILayout.Button("Organize")) {
				OrganizeFilesIntoFolders();
			}
		} else { // Asset type mappings tab
			for (int i = 0; i < countOfAssetTypeRows; i++) {
				DrawAssetTypeRow(i);
			}

			DrawAddAndRemoveControls();
		}

	}

	private void DrawToolbarTabs() {
		GUILayout.BeginHorizontal();
		selectedTabIndex = GUILayout.Toolbar(selectedTabIndex, tabs);
		GUILayout.EndHorizontal();
	}

	private void DrawAssetTypeRow(int currentIndex) {
		GUILayout.BeginHorizontal();
		EditorGUILayout.Space();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Name");

		EditorGUI.BeginChangeCheck(); // Listen for a change to any UI elements
		if (assetTypeRows != null) {
			assetTypeRows[currentIndex].name = EditorGUILayout.TextField(assetTypeRows[currentIndex].name);
		}
		if (EditorGUI.EndChangeCheck()) {
			isDirty = true;
		}
		GUILayout.EndVertical();

		EditorGUILayout.Space();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("File Extension");
		EditorGUI.BeginChangeCheck();
		if (assetTypeRows != null) {
			assetTypeRows[currentIndex].fileExtension = EditorGUILayout.TextField(assetTypeRows[currentIndex].fileExtension);
		}
		if (EditorGUI.EndChangeCheck() && assetTypes.ContainsKey(assetTypeRows[currentIndex].name)) {
			isDirty = true;
		}
		GUILayout.EndVertical();
		EditorGUILayout.Space();
		GUILayout.EndHorizontal();
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
	}

	private void InitializeFields() {
		foreach (string key in assetTypes.Keys) {
			totalNumberOfFileExtensions += assetTypes[key].Count;
		}

		countOfAssetTypeRows = totalNumberOfFileExtensions;

		assetTypeRows = new List<AssetTypeRow>();

		assetTypeNames = new string[totalNumberOfFileExtensions];
		assetTypes.Keys.CopyTo(assetTypeNames, 0);

		for (int i = 0; i < totalNumberOfFileExtensions; i++) {
			string key = assetTypeNames[i];
			if (key != null) {
				int numberOfFileExtensionsForAssetType = assetTypes[key].Count;
				for (int j = 0; j < numberOfFileExtensionsForAssetType; j++) {
					assetTypeRows.Add(new AssetTypeRow() {
						name = assetTypeNames[i],
						fileExtension = assetTypes[assetTypeNames[i]][j]
					});
				}
			}
		}

		countOfOrganizerRows = assetTypes.Keys.Count;
		organizerRows = new List<OrganizerRow>();
		for (int i = 0; i < countOfOrganizerRows; i++) {
			organizerRows.Add(new OrganizerRow() {
				selectedOptionIndex = i,
				folderPath = "Assets/" + assetTypeNames[i]
			});
		}
	}

	private void UpdateAssetTypes(int currentIndex) {
		assetTypes.Add(assetTypeRows[currentIndex].name, new List<string>());
		assetTypes[assetTypeRows[currentIndex].name].Add(assetTypeRows[currentIndex].fileExtension);
		totalNumberOfFileExtensions = 0;
		foreach (string key in assetTypes.Keys) {
			totalNumberOfFileExtensions += assetTypes[key].Count;
		}
		assetTypeNames = new string[totalNumberOfFileExtensions - 1];
		assetTypes.Keys.CopyTo(assetTypeNames, 0);
	}

	private void DrawOrganizerRow(int currentIndex) {
		GUILayout.BeginHorizontal();

		EditorGUILayout.Space();

		GUILayout.BeginVertical();
		EditorGUILayout.LabelField("Asset Type");
		EditorGUI.BeginChangeCheck();
		organizerRows[currentIndex].selectedOptionIndex = EditorGUILayout.Popup("", organizerRows[currentIndex].selectedOptionIndex, assetTypeNames);
		if (EditorGUI.EndChangeCheck()) {
			organizerRows[currentIndex].folderPath = "Assets/" + assetTypeNames[organizerRows[currentIndex].selectedOptionIndex];
		}
		GUILayout.EndVertical();

		EditorGUILayout.Space();

		GUILayout.BeginVertical();
		EditorGUILayout.LabelField("Folder Path");
		organizerRows[currentIndex].folderPath = EditorGUILayout.TextField(organizerRows[currentIndex].folderPath);
		GUILayout.EndVertical();
		EditorGUILayout.Space();

		GUILayout.BeginVertical();
		EditorGUILayout.LabelField("Select Folder");
		EditorGUI.BeginChangeCheck();
		organizerRows[currentIndex].obj = EditorGUILayout.ObjectField(organizerRows[currentIndex].obj, typeof(UnityEditor.DefaultAsset), true);
		if (EditorGUI.EndChangeCheck()) {
			organizerRows[currentIndex].folderPath = "Assets/" + organizerRows[currentIndex].obj.name;
		}
		GUILayout.EndVertical();
		EditorGUILayout.Space();
		GUILayout.EndHorizontal();
		EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
	}

	private void DrawAddAndRemoveControls() {
		GUILayout.BeginHorizontal();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		GUIContent add = new GUIContent();
		add.text = "+";
		if (GUILayout.Button(add)) {
			if (selectedTabIndex == 0) { // Organizer tab
				countOfOrganizerRows++;
				organizerRows.Add(new OrganizerRow());
			} else { // Asset type mappings tab
				countOfAssetTypeRows++;
				assetTypeRows.Add(new AssetTypeRow());
			}
		}

		GUIContent remove = new GUIContent();
		remove.text = "-";
		if (GUILayout.Button(remove)) {
			if (selectedTabIndex == 0) {
				countOfOrganizerRows--;
				organizerRows.RemoveAt(organizerRows.Count - 1); // Remove the last row
			} else {
				countOfAssetTypeRows--;
				assetTypeRows.RemoveAt(assetTypeRows.Count - 1);
			}
		}
		GUILayout.EndHorizontal();
	}

	private void OrganizeFilesIntoFolders() {
		Dictionary<string, string> fileExtensionsToFolderPathsMap = new Dictionary<string, string>();
		foreach (string assetTypeName in assetTypes.Keys) {
			for (int i = 0; i < assetTypes[assetTypeName].Count; i++) {
				string folderPath = "Assets/" + assetTypeName + "/";
				fileExtensionsToFolderPathsMap.Add(assetTypes[assetTypeName][i], folderPath);
			}
		}

		DirectoryInfo dir = new DirectoryInfo("Assets/");
		foreach (string fileExtension in fileExtensionsToFolderPathsMap.Keys) {
			string query = "*" + fileExtension;
			FileInfo[] info = dir.GetFiles(query);
			foreach (FileInfo file in info) {
				string filePath = fileExtensionsToFolderPathsMap[fileExtension] + file.Name;
				AssetDatabase.MoveAsset("Assets/" + file.Name, filePath);
			}
		}
	}
}
