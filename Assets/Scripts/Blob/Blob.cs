using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blob : MonoBehaviour {

	[System.Serializable]
	public struct BlobAttr
	{
		public string key;
		public int value;

		BlobAttr(string k, int v) {
			key = k;
			value = v;
		}
	}

	#region attrs
	[Header("Movement")]
	public bool selected = true;
	public float moveSpeed = 5f;
	public float walkSpeed = 5f;
	public float runSpeed = 10f;
	public float jumpForce = 10f;
	public float groundedJumpTolerance = 0.04f;

	[SerializeField]
	private BlobAttr[] defaultAttrs;

	private Dictionary<string, int> attrs = null;

	[Header("Split Controls")]
	public float minScale = 0.5f;
	public float maxScale = 3f;
	public float splitScale = 0.8f;
	public bool fireSplits = false;
	public float weight = 1f;
	public float killY = -10f;


	[Header("Component Links")]
	private Rigidbody rigidBody = null;
	[SerializeField]
	private Animator blobAnimator = null;
	[SerializeField]
	private Animator blobInnerAnimator = null;
	[SerializeField]
	private BlobMaterial currentMaterial = null;
	[SerializeField]
	private SkinnedMeshRenderer meshRenderer = null;

	private GameObject matEffect = null;

	public bool Grounded {
		get {
			return Mathf.Abs(this.rigidBody.velocity.y) < groundedJumpTolerance;
		}
	}

	public float Weight {
		get {
			return this.weight;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		this.rigidBody = this.GetComponent<Rigidbody> ();
		this.attrs = new Dictionary<string, int> ();

		this.ApplyAttrs (this.defaultAttrs);

		this.ApplyMat(currentMaterial, true);
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
		float verticalVelocity = this.rigidBody.velocity.y;
		if (this.AttrValue (BlobConstants.PAUSE_ANIM) > 0) {
			horizontalVelocity = 0f;
			verticalVelocity = 0f;
		}
		
		this.blobAnimator.SetFloat ("_VelY", verticalVelocity);
		this.blobAnimator.SetFloat ("_VelH", horizontalVelocity);
		this.blobAnimator.SetBool ("Grounded", this.Grounded);
		this.blobInnerAnimator.SetFloat ("_VelY", verticalVelocity);
		this.blobInnerAnimator.SetFloat ("_VelH", horizontalVelocity);
		this.blobInnerAnimator.SetBool ("Grounded", this.Grounded);

		if (this.rigidBody.velocity != Vector3.zero) {
			Vector3 lookRotation = new Vector3 (this.rigidBody.velocity.x, 0f, this.rigidBody.velocity.z);
			this.transform.rotation = Quaternion.LookRotation (-lookRotation);
		}
	}

	void UpdateInputMovement() {
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		
		Vector3 targetSpeed = new Vector3 (vertical, 0f, -horizontal) * moveSpeed;
		targetSpeed.y = this.rigidBody.velocity.y;

		if (Input.GetButton ("Fire2") || Input.GetButton ("Run")) {
			this.moveSpeed = runSpeed;
		} else {
			this.moveSpeed = walkSpeed;
		}

		if (Input.GetButtonDown ("Jump") && this.Grounded) {
			targetSpeed += new Vector3(0f, jumpForce, 0f);
		} 

		if (targetSpeed != Vector3.zero) {
			this.rigidBody.AddForce (targetSpeed - this.rigidBody.velocity, ForceMode.VelocityChange);
		}
		//this.transform.position = this.transform.position + new Vector3 (vertical, 0f, -horizontal) * moveSpeed * Time.deltaTime;
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
		this.weight *= 0.5f;
		GameObject.Instantiate (this);
		BlobGame.Instance.AddBlob ();
	}

	public void ApplyMat(BlobMaterial blobMat, bool firstMaterial = false) {
		this.meshRenderer.material = blobMat.mat;

		if (this.currentMaterial != blobMat) {
			this.RevertAttrs (this.currentMaterial.blobAttrs);
			if(this.matEffect != null) {
				GameObject.Destroy(this.matEffect);
				this.matEffect = null;
			}
		}

		if (firstMaterial || this.currentMaterial != blobMat) {
			this.currentMaterial = blobMat;
			this.ApplyAttrs (blobMat.blobAttrs);

			if(blobMat.materialEffect != null) {
				this.matEffect = (GameObject)GameObject.Instantiate(blobMat.materialEffect, this.transform.position, Quaternion.identity);
				this.matEffect.transform.parent = this.transform;
			}
		}

	}

	private void ApplyAttrs (BlobAttr[] attrArray) {
		foreach (BlobAttr a in attrArray) {
			this.attrs[a.key] = this.AttrValue(a.key) + a.value;
		}
	}
	
	private void RevertAttrs(BlobAttr[] attrArray) {
		foreach (BlobAttr a in attrArray) {
			this.attrs[a.key] = this.AttrValue(a.key) - a.value;
		}
	}

	public int AttrValue(string key) {
		if(this.attrs.ContainsKey(key)) {
			return this.attrs[key];
		}
		return 0;
	}
}
