using System;
using UnityEngine;
using System.IO;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;

namespace Netch.UtilityScripts {
	[System.Serializable]
	public class Data {
		[SerializeField] private string data;

		public string GetData() {
			return data;
		}
		public void SetData(string data) {
			this.data = data;
		}
	}

	[InitializeOnLoad]
	public class UtilityScriptPrefs : MonoBehaviour {
		private const string dataPath = "Assets/data.txt";

		static UtilityScriptPrefs() {
			LoadData();
		}

		private static void LoadData() {
			if (!File.Exists(dataPath)) {
				// Create a new data file
				FileStream fs = new FileStream(dataPath, FileMode.Create);

				Data dataObject = new Data();
				dataObject.SetData("Netch !!!");

				string data = JsonUtility.ToJson(dataObject);
				byte[] dataBytes = Encoding.UTF8.GetBytes(data);

				fs.Write(dataBytes);
				fs.Close();
			} else {
				string data = File.ReadAllText(dataPath);
				if (string.IsNullOrEmpty(data)) return;

				Data obj = JsonUtility.FromJson<Data>(data);

				Debug.Log(obj.GetData());
			}
		}

		private static void SaveData() {

		}

		public static string GetString(string key, string defaultValue) {
			return defaultValue;
		}
		public static void SetString(string key, string value) {

		}

		public static void DeleteKey(string key) {

		}
	}
}
#endif