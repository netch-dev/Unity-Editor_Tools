using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuilder_Example : EditorWindow {
	[SerializeField]
	private VisualTreeAsset m_VisualTreeAsset = default;

	[MenuItem("Window/UI Toolkit/UIBuilder_Example")]
	public static void ShowExample() {
		UIBuilder_Example wnd = GetWindow<UIBuilder_Example>();
		wnd.titleContent = new GUIContent("UIBuilder_Example");
	}

	public void CreateGUI() {
		// Each editor window contains a root VisualElement object
		VisualElement root = rootVisualElement;

		/*        // VisualElements objects can contain other VisualElement following a tree hierarchy.
				VisualElement label = new Label("Hello World! From C#");
				root.Add(label);*/

		// Instantiate UXML
		VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
		root.Add(labelFromUXML);
	}
}
