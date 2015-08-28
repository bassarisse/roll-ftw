using UnityEngine;
using System.Collections;
using System.Linq;

public class LevelEnd : MonoBehaviour {

	public float transitionTime = 0.8f;
	public float pullForce = 1.0f;
	public float stopRange = 0.5f;
	public float stopVelocity = 0.5f;
	public float torque = 10.0f;

	bool _activated = false;
	Collider2D _collider;
	
	bool _startTransition = false;
	float _transitionTimer = 0.0f;

	// Use this for initialization
	void Start () {

		_activated = false;
		_collider = null;
		_startTransition = false;
		
		AudioHandler.Load ("level_end");
		AudioHandler.Load ("win1");

	}

	void FixedUpdate() {
		
		AttractObject ();

	}
	
	// Update is called once per frame
	void Update () {

		if (_startTransition && _transitionTimer < transitionTime) {
			_transitionTimer += Time.deltaTime;
			
			if (_transitionTimer >= transitionTime) {
				_transitionTimer = transitionTime;
				
				AudioHandler.Play("win1", 0.65f);
				Messenger.Broadcast("LevelEnd");
				Messenger.Broadcast<bool>("EnablePalleteSwap", true);
			}

			var channelValue = _transitionTimer / transitionTime;

			Messenger.Broadcast<float>("ColorCurve.SetValue", channelValue);
		}
		
	}

	void AttractObject() {
		if (_collider == null)
			return;

		Vector2 forceDir = transform.position - _collider.transform.position;

		if (forceDir.magnitude <= stopRange && _collider.attachedRigidbody.velocity.magnitude <= stopVelocity) {

			_collider.transform.position = new Vector3(transform.position.x, transform.position.y, _collider.transform.position.z);

		} else {

			_collider.attachedRigidbody.AddForce (forceDir.normalized * pullForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
			_startTransition = true;

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
		if (ballControl != null) {
			ballControl.enabled = false;
			ballControl.StopAudio();
		}

		GameTimer.running = false;
		GameState.FinishedLevel (GameTimer.time);
		
		Messenger.Broadcast("LevelEnding");

		AudioHandler.Play("level_end");

	}

}
