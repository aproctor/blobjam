using UnityEngine;
using System.Collections;

public class LevelExit : MonoBehaviour {

	[SerializeField]
	private Level level;


	void OnTriggerEnter(Collider other) {

		if (other.GetComponent<Blob> ()) {
			Debug.LogError ("EXIT");
		}

	}
}
