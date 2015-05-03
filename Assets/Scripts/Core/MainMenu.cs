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
		BlobGame.Instance.state = BlobGame.GameState.Menu;

//		BlobGame.Instance.hud.Hide ();
	}

	// Update is called once per frame
	void Update () {
		if(state == MenuState.Idle) {
			if(Input.anyKeyDown) {				
//				BlobGame.Instance.hud.Show();
				BlobGame.Instance.levelManager.LoadLevel(0);
				state = MenuState.Exiting;
			}
		}
	}
}
