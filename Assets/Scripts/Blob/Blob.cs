﻿using UnityEngine;
using System.Collections;

public class Blob : MonoBehaviour {

	#region attrs
	[Header("Movement")]
	public bool selected = true;
	public float moveSpeed = 5f;
	public float walkSpeed = 5f;
	public float runSpeed = 10f;
	public float jumpForce = 10f;
	public float groundedJumpTolerance = 0.04f;

	[Header("Split Controls")]
	public float minScale = 0.5f;
	public float maxScale = 3f;
	public float splitScale = 0.8f;
	public bool fireSplits = false;
	public float killY = -10f;

	private Rigidbody rigidBody = null;
	private Animator blobAnimator = null;

	[Header("Component Links")]
	[SerializeField]
	private BlobMaterial currentMaterial = null;
	[SerializeField]
	private SkinnedMeshRenderer meshRenderer = null;

	public bool Grounded {
		get {
			return Mathf.Abs(this.rigidBody.velocity.y) < groundedJumpTolerance;
		}
	}

	public float Weight {
		get {
			return this.transform.lossyScale.x;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		this.rigidBody = this.GetComponent<Rigidbody> ();
		this.blobAnimator = this.GetComponent<Animator> ();

		this.ApplyMat(currentMaterial);
	}

	#region update_methods
	// Update is called once per frame
	void Update () {

		if (selected) {
			UpdateInputMovement();

			if(this.fireSplits && Input.GetButtonDown("Fire1")) {
				this.Split();
			}
		}


		this.UpdateAnimations ();

		if (this.transform.position.y < killY) {
			this.Die();
		}
	}


	void UpdateAnimations() {
		float xSqr = Mathf.Pow(this.rigidBody.velocity.x, 2);
		float zSqr = Mathf.Pow(this.rigidBody.velocity.z, 2);

		float horizontalVelocity = Mathf.Sqrt(xSqr + zSqr);
		this.blobAnimator.SetFloat ("_VelY", this.rigidBody.velocity.y);
		this.blobAnimator.SetFloat ("_VelH", horizontalVelocity);
		this.blobAnimator.SetBool ("Grounded", this.Grounded);

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
	#endregion

	public void Die() {
		BlobGame.Instance.RemoveBlob ();
		//TODO tween time
		GameObject.Destroy (this.gameObject);
	}


	public void Split() {
		float targetScale = this.transform.localScale.x * this.splitScale;
		if (targetScale < this.minScale) {
			return;
		}
		this.transform.localScale = Vector3.one * targetScale;
		GameObject.Instantiate (this);
		BlobGame.Instance.AddBlob ();
	}

	public void ApplyMat(BlobMaterial blobMat) {
		this.meshRenderer.material = blobMat.mat;
		this.currentMaterial = blobMat;
	}
}
