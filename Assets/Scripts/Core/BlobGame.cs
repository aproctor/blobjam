using UnityEngine;
using System.Collections;

public class BlobGame : MonoBehaviour {
	
	public GameHud hud;

	public int NumBlobs { get; private set; }

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

	// Use this for initialization
	void Start () {
		if (_instance == null || _instance == this) {
			_instance = this;
			DontDestroyOnLoad(this);

			this.levelManager = this.GetComponent<LevelManager>();
			this.NumBlobs = 1;
		} else {
			Debug.Log("There can only be one! BlobGame singleton freakout");
		}
	}


	//TODO rename this
	public void CountBlobs() {
		Blob[] blobs = GameObject.FindObjectsOfType<Blob> ();
		this.NumBlobs = blobs.Length;
	}

	public void AddBlob() {
		this.NumBlobs += 1;
	}

	public void RemoveBlob () {
		this.NumBlobs -= 1;

		if(this.NumBlobs <= 0) {
			this.GameOver();
		}
	}

	public void GameOver() {
		Debug.LogError ("GAME OVER!");
		this.levelManager.LoadMenu();
	}
}
