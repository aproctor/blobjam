using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Blob blob = other.GetComponent<Blob> ();
		if (blob) {
			blob.Die();
		}
	}
}
