using UnityEngine;
using System.Collections;

public class MaterialSwapper : MonoBehaviour {


	public BlobMaterial mat;

	void OnTriggerEnter(Collider other) {
		Blob blob = other.GetComponent<Blob> ();
		if (blob) {
			blob.ApplyMat(mat);
		}
	}
}
