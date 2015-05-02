using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public Objective[] objectives;

	public void Start() {
		BlobGame.Instance.levelManager.LevelLoaded(this);
	}

	public bool IsComplete {
		get {
			foreach (Objective objective in this.objectives) {
				if (objective.isComplete == false) {
					return false;
				}
			}

			return true;
		}
	}

	public void Finish() {
		BlobGame.Instance.levelManager.LoadNextLevel();
	}
}
