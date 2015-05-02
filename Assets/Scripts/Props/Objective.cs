using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	public bool isComplete = false;

	public void CompleteObjective() {
		isComplete = true;
	}
}
