using UnityEngine;
using System.Collections;

public class Blob : MonoBehaviour {

	public bool selected = true;
	public float moveSpeed = 5f;
	public float walkSpeed = 5f;
	public float runSpeed = 10f;
	public float jumpForce = 10f;
	public float groundedJumpTolerance = 0.04f;

	private Rigidbody rigidBody = null;

	public bool Grounded {
		get {
			return Mathf.Abs(this.rigidBody.velocity.y) < groundedJumpTolerance;
		}
	}

	// Use this for initialization
	void Start () {
		this.rigidBody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (selected) {
			UpdateInputMovement();
		}
	}

	void UpdateInputMovement() {

		if (Input.GetButton ("Fire2") || Input.GetButton ("Run")) {
			this.moveSpeed = runSpeed;
		} else {
			this.moveSpeed = walkSpeed;
		}

		if (Input.GetButtonDown ("Jump") && this.Grounded) {
			this.rigidBody.AddForce(new Vector3(0f, jumpForce, 0f));
		} 

		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");

		this.transform.position = this.transform.position + new Vector3 (vertical, 0f, -horizontal) * moveSpeed * Time.deltaTime;
	}
}
