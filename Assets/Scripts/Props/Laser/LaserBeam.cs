using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

	[SerializeField]
	private GameObject emitter;

	[SerializeField]
	private LaserHitpoint endPoint;

	void Update() {
		Vector3 midPoint = (endPoint.transform.position - emitter.transform.position) * 0.5f + emitter.transform.position;
		this.transform.position = midPoint;
		this.transform.localScale = new Vector3(this.transform.lossyScale.x, Vector3.Distance (emitter.transform.position, endPoint.transform.position), this.transform.lossyScale.z);
	}
	


	void OnTriggerEnter(Collider other) {
		//TODO normalize position
		this.endPoint.transform.position = other.transform.position;
	}


	private void RefreshLaser() {
		this.endPoint.Reset();
	}


}
