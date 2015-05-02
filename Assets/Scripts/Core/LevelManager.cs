using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private int curLevel = 0;
	public string[] Levels;

	public Level Level {
		get; private set;
	}

	void Update() {
		if (Input.GetButtonDown("Cancel")) {
			this.ReloadLevel();
		}
	}

	public void LevelLoaded(Level l) {
		this.Level = l;
		BlobGame.Instance.OnLevelLoaded();
	}

	public void LoadLevel(int index) {
		Application.LoadLevel (this.Levels [index]);
	}


	public void LoadNextLevel() {
		if (this.curLevel < this.Levels.Length - 1) {
			LoadLevel(++this.curLevel);
		} else {
			LoadMenu();
		}
	}

	public void ReloadLevel() {
		LoadLevel(this.curLevel);
	}

	public void LoadMenu() {		
		this.Reset();
		Application.LoadLevel("Menu");
	}

	private void Reset() {
		this.curLevel = 0;
	}
}
