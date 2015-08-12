using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour {
	
	public float pullForce = 1.0f;
	public float stopRange = 0.5f;
	public float stopVelocity = 0.5f;
	public float torque = 10.0f;

	bool _activated = false;
	Collider2D _collider;

	// Use this for initialization
	void Start () {
		_activated = false;
		_collider = null;
	}
	
	// Update is called once per frame
	void Update () {

		AttractObject ();
	
	}

	void AttractObject() {
		if (_collider == null)
			return;

		Vector2 forceDir = transform.position - _collider.transform.position;

		if (forceDir.magnitude <= stopRange && _collider.attachedRigidbody.velocity.magnitude <= stopVelocity) {

			_collider.transform.position = new Vector3(transform.position.x, transform.position.y, _collider.transform.position.z);

		} else {

			_collider.attachedRigidbody.AddForce (forceDir.normalized * pullForce * Time.fixedDeltaTime, ForceMode2D.Impulse);

		}

	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (_activated || collider.gameObject.name != "Ball")
			return;

		_activated = true;
		_collider = collider;
		_collider.attachedRigidbody.gravityScale = 0.0f;
		_collider.attachedRigidbody.AddTorque (torque, ForceMode2D.Impulse);

		var ballControl = collider.GetComponent<BallControl> ();
		if (ballControl != null)
			ballControl.enabled = false;

	}

}
