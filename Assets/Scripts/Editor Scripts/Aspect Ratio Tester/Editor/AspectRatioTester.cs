using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class AspectRatioTester : Editor {
	private static string applicationDataPath = Application.dataPath + "/Screenshots/";
	private static int numberofScreenshotsSaved = 0;
	private static bool menuOptionWasClicked = false;
	private static bool startedScreenshot = false;
	private static List<string> aspectRatios = new List<string> {
		applicationDataPath + "Free Aspect.png",
		applicationDataPath + "16 by 9 Aspect.png",
		applicationDataPath + "16 by 10 Aspect.png",
		applicationDataPath + "Full HD (1920x1080).png",
		applicationDataPath + "WXGA.png",
		applicationDataPath + "QHD.png",
		applicationDataPath + "4K UHD.png",
	};

	static AspectRatioTester() {
		EditorApplication.update += EditorToolLoop; // Similar to unity's Update method but for editor scripts
	}

	[MenuItem("Custom Tools/Test Aspect Ratios")]
	private static void TestAspectRatios() {
		menuOptionWasClicked = true;
		startedScreenshot = false;
		numberofScreenshotsSaved = 0;
	}

	private static void EditorToolLoop() {
		if (menuOptionWasClicked && !startedScreenshot) {
			startedScreenshot = true;
			SaveScreenshotAtAspectRatio(numberofScreenshotsSaved, aspectRatios[numberofScreenshotsSaved]);
		}

		// Check if the screenshot was saved
		if (numberofScreenshotsSaved < aspectRatios.Count && System.IO.File.Exists(aspectRatios[numberofScreenshotsSaved])) {
			numberofScreenshotsSaved++;
			startedScreenshot = false;
			Refresh();
		}

		// Check if all of the screenshots were saved
		if (numberofScreenshotsSaved == aspectRatios.Count) {
			menuOptionWasClicked = false;
		}
	}

	public static void Refresh() {
		AssetDatabase.Refresh();
	}

	public static void SetGameViewAspectRatio(int index) {
		System.Type gameViewWindowType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
		EditorWindow gameViewWindow = EditorWindow.GetWindow(gameViewWindowType);
		MethodInfo sizeSelectionCallback = gameViewWindowType.GetMethod("SizeSelectionCallback", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		sizeSelectionCallback.Invoke(gameViewWindow, new object[] { index, null });
	}

	public static void TakeScreenshot(string fileName) {
		ScreenCapture.CaptureScreenshot(fileName);
	}

	public static void SaveScreenshotAtAspectRatio(int index, string fileName) {
		SetGameViewAspectRatio(index);
		TakeScreenshot(fileName);
	}
}
