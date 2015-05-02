using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Pickup : MonoBehaviour {

	private bool available = true;
	public UnityEvent OnPickup;

	void OnTriggerEnter(Collider other) {
		if (available) {
			Blob blob = other.GetComponent<Blob>();
			if(blob) {
				OnPickup.Invoke();
				available = false;
			}
		}
	}
}
