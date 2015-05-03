using UnityEngine;
using System.Collections;

public class BlobMaterial : ScriptableObject {

	public Material mat;

	public Blob.BlobAttr[] blobAttrs;

	public GameObject materialEffect = null;

	public int LayerId {
		get {
			return LayerMask.NameToLayer (this.name);
		}
	}

}
