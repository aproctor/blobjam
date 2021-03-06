﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class BlobGameEditor : EditorWindow {

	[MenuItem("Blob Game/Scenes/Menu", false, 1)]
	public static void GotoMenu() {
		EditorApplication.OpenScene ("Assets/Scenes/Menu.unity");
	}

	[MenuItem("Blob Game/Scenes/Level 1", false, 2)]
	public static void GotoLevel1() {
		EditorApplication.OpenScene ("Assets/Scenes/Levels/Level1.unity");
	}
	[MenuItem("Blob Game/Scenes/Level 2", false, 2)]
	public static void GotoLevel2() {
		EditorApplication.OpenScene ("Assets/Scenes/Levels/Level2.unity");
	}
	[MenuItem("Blob Game/Scenes/Level 3", false, 2)]
	public static void GotoLevel3() {
		EditorApplication.OpenScene ("Assets/Scenes/Levels/Level3.unity");
	}

	
	[MenuItem("Blob Game/Editor Window", false, 300)]
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
