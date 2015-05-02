using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public void OpenDoor() {
		this.GetComponent<Animator> ().SetBool("Open",true);
	}

	public void CloseDoor() {
		this.GetComponent<Animator> ().SetBool("Open", false);
	}
}
