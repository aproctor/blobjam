using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public enum MenuState {
		Loading,
		Idle,
		Exiting
	}

	public MenuState state = MenuState.Loading;

	void Start () {
		//TODO wait for a delay to animate the title screen
		state = MenuState.Idle;
	}

	// Update is called once per frame
	void Update () {
		if(state == MenuState.Idle) {
			if(Input.GetButtonDown("Jump")) {
				BlobGame.Instance.levelManager.LoadNextLevel();
				state = MenuState.Exiting;
			}
		}
	}
}
