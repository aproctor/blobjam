using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public bool complete = false;

	public void Finish() {
		BlobGame.Instance.levelManager.LoadNextLevel();
	}
}
