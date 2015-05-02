using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private int curLevel = -1;
	public string[] Levels;


	public void LoadNextLevel() {
		if (this.curLevel < this.Levels.Length - 1) {
			Application.LoadLevel(this.Levels[++this.curLevel]);
		} else {
			this.Reset();
			Application.LoadLevel("Menu");
		}
	}

	private void Reset() {
		this.curLevel = -1;
	}
}
