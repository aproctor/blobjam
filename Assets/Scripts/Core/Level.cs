using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public bool complete = false;

	public Objective[] objectives;

	public bool IsComplete() {
		foreach(Objective objective in this.objectives) {
			if(objective.isComplete == false) {
				return false;
			}
		}

		return true;
	}

	public void Finish() {
		BlobGame.Instance.levelManager.LoadNextLevel();
	}
}
