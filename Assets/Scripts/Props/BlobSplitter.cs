using UnityEngine;
using System.Collections;

public class BlobSplitter : MonoBehaviour {

	public int numUses = 1;

	void OnTriggerEnter(Collider other) {
		if (numUses < 1) {
			return;
		}

		Blob blob = other.GetComponent<Blob> ();
		if (blob) {
			this.numUses -= 1;
			blob.Split();
		}
	}
}
