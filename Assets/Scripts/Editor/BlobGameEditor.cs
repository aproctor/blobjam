using UnityEngine;
using UnityEditor;
using System.Collections;

public class BlobGameEditor : EditorWindow {
	
	[MenuItem("Blob Game/Editor Window", false, 1)]
	public static void ShowWindow() {
		EditorWindow w = EditorWindow.GetWindow(typeof(BlobGameEditor));
		w.Show();
	}
	
	void OnGUI() {
		if (GUILayout.Button ("Create BlobMaterial")) {
			BlobMaterial instance = ScriptableObject.CreateInstance<BlobMaterial>();
			AssetDatabase.CreateAsset(instance, "Assets/Resources/ScriptableObjects/Mats/new.asset");
		}
	}
}
