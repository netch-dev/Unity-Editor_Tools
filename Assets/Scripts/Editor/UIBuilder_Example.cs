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

		Label label = new Label("Example text...");
		root.Add(label);

		Button button = new Button();
		button.text = "Click me!";
		root.Add(button);

		// Instantiate UXML
		VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
		root.Add(labelFromUXML);
	}
}
