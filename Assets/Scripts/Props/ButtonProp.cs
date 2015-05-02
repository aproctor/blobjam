using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ButtonProp : MonoBehaviour {
	
	private int numCurrentPressers = 0;

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
		if (other.GetComponent<ButtonPresser> ()) {
			//TODO abstract this so others can flick switches
			state = ButtonState.Pushed;
			buttonPushed.Invoke();
			if(buttonType == ButtonType.Switch) {
				state = ButtonState.Locked;
			}
		}
	}


	void OnTriggerStay(Collider other) {
		if (other.GetComponent<ButtonPresser> ()) {
			numCurrentPressers++;
		}
	}


	void OnTriggerExit(Collider other) {
		if (other.GetComponent<ButtonPresser> () && numCurrentPressers == 0) {
			//TODO check for multiple things pressing the button
			buttonReleased.Invoke();
		}
	}

	void Update() {
		//This is a brutal, but it's a jam, whatever
		numCurrentPressers = 0;
	}


}
