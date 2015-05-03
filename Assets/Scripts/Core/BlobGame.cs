using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlobGame : MonoBehaviour {
	
	public GameHud hud;

	private List<Blob> blobs = new List<Blob> ();
	public int NumBlobs {
		get {
			return this.blobs.Count;
		}
	}

	private static BlobGame _instance = null;
	public static BlobGame Instance {
		get {
			if(_instance == null) {
				//For development, load prefab, this is typically instantiated in the Menu Scene
				Debug.Log ("Instantiating BlobGame for Debug");
			 	_instance = GameObject.Instantiate((GameObject)Resources.Load("BlobGame")).GetComponent<BlobGame>();
			}
			return _instance;
		}
	}

	public LevelManager levelManager;

	public enum GameState {
		Loading,
		Menu,
		Playing,
		Over
	}
	public GameState state = GameState.Loading;
	
	public void ToggleSelection() {
		List<Blob> selectedBlobs = new List<Blob>();
		int curSelected = 0;
		int i = 0;
		foreach (Blob b in this.blobs) {
			if(b.Selected) {
				selectedBlobs.Add(b);
				curSelected = i;
			}
			i++;
		}

		//Deselect all blobs
		foreach (Blob b in selectedBlobs) {
			b.Selected = false;
		}

		if (selectedBlobs.Count > 1) {
			//Select the first blob in the list (others were just deselected)
			selectedBlobs [0].Selected = true;
		} else if(selectedBlobs.Count == 1){
			//Select the next blob based on curSelected
			int targetIndex = ++curSelected % this.blobs.Count;
			bool SELECT_ALL_ON_LAST = true;

			if(targetIndex == 0 && SELECT_ALL_ON_LAST) {
				foreach (Blob b in this.blobs) {
					b.Selected = true;
				}
			} else {
				this.blobs[targetIndex].Selected = true;
			}

		} else if(this.blobs.Count > 0) {
			//Select the first blob if none were selected for some reason
			this.blobs[0].Selected = true;
		}
	}

	// Use this for initialization
	void Start () {
		if (_instance == null || _instance == this) {
			_instance = this;
			DontDestroyOnLoad(this);

			this.levelManager = this.GetComponent<LevelManager>();

			string[] layers = new string[] {
				"slime","rock", "ice", "fire", "water"
			};
			foreach(string layer in layers) {
				Physics.IgnoreLayerCollision(LayerMask.NameToLayer(layer),LayerMask.NameToLayer("ignore_" + layer));
			}

		} else {
			GameObject.Destroy(this.gameObject);
			Debug.Log("There can only be one! BlobGame singleton freakout");
		}
	}


	void Update() {
		if (this.state == GameState.Over) {			
			if (Input.GetButtonDown ("Cancel")) {
				this.levelManager.ReloadLevel ();
			}
			if (Input.GetButtonDown ("Jump")) {
				this.levelManager.LoadMenu ();
			}
		} else {
			//TODO only do this if playing, but state machine isn't fully rigged upf or development scenes
			this.UpdatePlaying();
		}

	}

	void UpdatePlaying() {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			this.ToggleSelection();
		}
	}

	public void OnLevelLoaded () {
		this.state = GameState.Playing;
	
		Blob[] blobs = GameObject.FindObjectsOfType<Blob> ();
		this.blobs.AddRange (blobs);
	}

	public void AddBlob(Blob blob) {
		this.blobs.Add (blob);
	}

	public void RemoveBlob (Blob blob) {
		this.blobs.Remove (blob);

		if(this.NumBlobs <= 0) {
			//Not ready to do this, level loads cause loops
			if(this.levelManager.Level.IsComplete) {
				this.levelManager.LoadNextLevel();
			} else {
				this.GameOver();
			}
		}
	}

	public void GameOver() {
		Debug.LogError ("GAME OVER!");
		this.state = GameState.Over;
	}
}
