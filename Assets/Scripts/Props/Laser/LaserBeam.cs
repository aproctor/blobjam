using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

	[SerializeField]
	private GameObject emitter;

	[SerializeField]
	private LaserHitpoint endPoint;

	private bool goofy = false;
	void Start () {
		int rot = ((int)Mathf.Abs(this.transform.parent.localRotation.eulerAngles.y)) % 180;
		goofy = (rot > 45);
	}

	void Update() {
		Vector3 midPoint = (endPoint.transform.position - emitter.transform.position) * 0.5f + emitter.transform.position;
		this.transform.position = midPoint;
		this.transform.localScale = new Vector3(this.transform.lossyScale.x, Vector3.Distance (emitter.transform.position, endPoint.transform.position), this.transform.lossyScale.z);
	}
	


	void OnTriggerStay(Collider other) {
		if (goofy) {
			this.endPoint.transform.position = new Vector3 (other.transform.position.x, this.endPoint.transform.position.y, this.endPoint.transform.position.z);
		} else {
			this.endPoint.transform.position = new Vector3 (this.endPoint.transform.position.x, this.endPoint.transform.position.y, other.transform.position.z);
		}

		Blob blob = other.GetComponent<Blob>();
		if (blob && blob.AttrValue(BlobConstants.INV_LASER) < 1) {
			blob.Die();
			this.RefreshLaser();
		}
	}

	
	void OnTriggerExit(Collider other) {
		this.RefreshLaser ();
	}

	private void RefreshLaser() {
		this.endPoint.Reset();
	}


}
