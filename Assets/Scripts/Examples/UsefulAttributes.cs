using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class UsefulAttributes : MonoBehaviour {
	static UsefulAttributes() {
		//Debug.Log("UsefulAttributes loaded from the constructor");
	}

	[InitializeOnLoadMethod]
	private static void Setup() {
		//Debug.Log("UsefulAttributes loaded from a method");
	}
}
