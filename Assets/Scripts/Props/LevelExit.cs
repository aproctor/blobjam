using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LevelExit : MonoBehaviour {

	public float capcity = 1f;
	public float volume = 0f;

	[SerializeField]
	private GameObject scaler;

	public UnityEvent OnFull;

	void Awake() {
		SetFillScale ();
	}

	public bool Full {
		get {
			return volume >= capcity;
		}
	}

	private void SetFillScale() {
		scaler.transform.localScale = new Vector3 (1f, Mathf.Clamp(volume / capcity, 0.01f, 1f), 1f);
	}

	void OnTriggerEnter(Collider other) {
		Blob blob = other.GetComponent<Blob>();

		if (blob && this.Full == false && this.volume + blob.Weight <= this.capcity) {
			this.volume += blob.Weight;

			SetFillScale();

			if(this.Full) {
				OnFull.Invoke();
			}			

			blob.Die();
		}

	}
}
