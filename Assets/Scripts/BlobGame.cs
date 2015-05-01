using UnityEngine;
using System.Collections;

public class BlobGame : MonoBehaviour {


	private static BlobGame _instance = null;
	public static BlobGame Instance {
		get {
			return _instance;
		}
	}

	public LevelManager levelManager;

	// Use this for initialization
	void Start () {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad(this);
		} else {
			Debug.Log("There can only be one! BlobGame singleton freakout");
		}
	}
}
