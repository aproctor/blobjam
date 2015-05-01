using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ButtonProp : MonoBehaviour {

	public enum ButtonType {
		Hold,
		Switch,
		Toggle
	}

	public enum ButtonState {
		Idle,
		Pushed,
		Locked
	}

	public ButtonType buttonType = ButtonType.Hold;

	public ButtonState state = ButtonState.Idle;

	public GameObject target = null;
	public UnityEvent buttonPushed;
	public UnityEvent buttonReleased;

	void OnTriggerEnter(Collider other) {
		if (state == ButtonState.Locked) {
			return;
		}
		if (other.GetComponent<Blob> ()) {
			//TODO abstract this so others can flick switches
			state = ButtonState.Pushed;
			buttonPushed.Invoke();
			if(buttonType == ButtonType.Switch) {
				state = ButtonState.Locked;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.GetComponent<Blob> ()) {
			//TODO check for multiple things pressing the button
			buttonReleased.Invoke();
		}
	}


}
