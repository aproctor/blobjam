using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameHud : MonoBehaviour {

	[SerializeField]
	private BlobGame blobGame = null;
	private Canvas canvas = null;

	[SerializeField]
	private GameObject GameOverText = null;

	[SerializeField]
	private Text livesField;

	void Start() {
		this.canvas = this.GetComponent<Canvas> ();
	}

	void Update() {
		//TODO don't really need to draw this often
		this.livesField.text = "Blobs: " + blobGame.NumBlobs;
	}

	public void Show() {
		this.canvas.gameObject.SetActive (true);
	}

	public void Hide() {
		this.canvas.gameObject.SetActive (false);
	}

	public void ShowGameOver(bool win = false) {
		//TODO happy message for win
		this.GameOverText.SetActive (true);
	}

	public void HideGameOver() {
		this.GameOverText.SetActive (false);
	}
}
