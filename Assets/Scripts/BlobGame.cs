using UnityEngine;
using System.Collections;

public class BlobGame : MonoBehaviour {


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
		} else {
			Debug.Log("There can only be one! BlobGame singleton freakout");
		}
	}
}
