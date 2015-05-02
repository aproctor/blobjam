using UnityEngine;
using System.Collections;

public class LaserHitpoint : MonoBehaviour {

	private Vector3 originalPosition = Vector3.zero;

	// Use this for initialization
	void Start () {
		this.originalPosition = this.transform.position;
	}

	public void Reset () {
		this.transform.position = this.originalPosition;
	}

}
