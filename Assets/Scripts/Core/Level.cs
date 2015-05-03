using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public Objective[] objectives;

	public void Start() {
		BlobGame.Instance.levelManager.LevelLoaded(this);
	}

	public float scrollSpeed = 30f;

	public void Update() {
		float move = Input.GetAxis ("Mouse ScrollWheel");
		if (Mathf.Abs (move) > 0f) {
			this.transform.Rotate(new Vector3(0f, move) * scrollSpeed * Time.deltaTime);
		}
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
