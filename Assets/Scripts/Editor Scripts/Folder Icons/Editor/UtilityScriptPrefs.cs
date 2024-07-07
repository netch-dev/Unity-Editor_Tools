using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;

namespace Netch.UtilityScripts {
	[System.Serializable]
	public class Data {
		[SerializeField] private List<string> keys;
		[SerializeField] private List<string> values;

		public Data() {
			keys = new List<string>();
			values = new List<string>();
		}

		public string GetString(string key, string defaultValue) {
			for (int i = 0; i < keys.Count; i++) {
				if (keys[i] == key) {
					return values[i];
				}
			}

			return defaultValue;
		}

		public void SetString(string key, string value) {
			for (int i = 0; i < keys.Count; i++) {
				if (keys[i] == key) {
					values[i] = value;
					return;
				}
			}

			// The key doesn't exist, add it
			keys.Add(key);
			values.Add(value);
		}

		public void DeleteKey(string key) {
			for (int i = 0; i < keys.Count; i++) {
				if (keys[i] == key) {
					keys.RemoveAt(i);
					values.RemoveAt(i);
					return;
				}
			}
		}
	}

	[InitializeOnLoad]
	public class UtilityScriptPrefs : MonoBehaviour {
		private const string dataPath = "Assets/data.txt";
		private static Data dataObject;

		static UtilityScriptPrefs() {
			LoadData();
		}

		private static void LoadData() {
			if (!File.Exists(dataPath)) {
				// Create a new data file
				FileStream fs = new FileStream(dataPath, FileMode.Create);

				dataObject = new Data();
				string data = JsonUtility.ToJson(dataObject);
				byte[] dataBytes = Encoding.UTF8.GetBytes(data);

				fs.Write(dataBytes);
				fs.Close();
			} else {
				string data = File.ReadAllText(dataPath);
				if (string.IsNullOrEmpty(data)) return;

				dataObject = JsonUtility.FromJson<Data>(data);
			}
		}

		private static void SaveData() {
			string data = JsonUtility.ToJson(dataObject, true);
			File.WriteAllText(dataPath, data);
		}

		public static string GetString(string key, string defaultValue) {
			return dataObject.GetString(key, defaultValue);
		}

		public static void SetString(string key, string value) {
			dataObject.SetString(key, value);
			SaveData();
		}

		public static void DeleteKey(string key) {
			dataObject.DeleteKey(key);
			SaveData();
		}
	}
}
#endif