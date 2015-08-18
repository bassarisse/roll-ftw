using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class BallControl : MonoBehaviour {
	
	public float Speed = 2.0f;
	public float JumpForce = 100.0f;
	public LayerMask JumpLayerMask;
	
	Rigidbody2D _body;
	AudioSource _audio;
	float _jumpTimer;
	bool _jumping;
	bool _firstHit;
	bool _isHittingGround;
	bool _isHittingWall;
	bool _shouldHitSound;
	
	Vector2 _storedVelocity;
	float _storedAngularVelocity;
	float _storedGravityScale;
	float _storedVolume;

	// Use this for initialization
	void Start () {

		if (Application.isEditor)
			Application.LoadLevelAdditive("Game");
		
		AudioHandler.Load ("jump");
		AudioHandler.Load ("hit1");
		AudioHandler.Load ("hit2");
		AudioHandler.Load ("hit3");
		
		_body = GetComponent<Rigidbody2D> ();
		_audio = GetComponent<AudioSource> ();
		_jumpTimer = 0f;
		_jumping = false;
		_firstHit = true;
		_isHittingGround = false;
		_isHittingWall = false;
		_shouldHitSound = false;

		Messenger.AddListener ("LevelStart", ActivateControl);
		Messenger.AddListener ("LevelPause", PauseControl);
		Messenger.AddListener ("LevelResume", ResumeControl);

		this.enabled = false;
	
	}
	
	void DeactivateControl() {
		this.enabled = false;
	}
	
	void ActivateControl() {
		this.enabled = true;
	}
	
	void PauseControl() {

		_storedVolume = _audio.volume;
		
		_audio.volume = 0f;

		_storedVelocity = _body.velocity;
		_storedAngularVelocity = _body.angularVelocity;
		_storedGravityScale = _body.gravityScale;

		_body.isKinematic = true;
		_body.velocity = Vector2.zero;
		_body.angularVelocity = 0f;
		_body.gravityScale = 0f;
		_body.Sleep ();

		DeactivateControl();
	}
	
	void ResumeControl() {
		
		_audio.volume = _storedVolume;

		_body.isKinematic = false;
		_body.velocity = _storedVelocity;
		_body.angularVelocity = _storedAngularVelocity;
		_body.gravityScale = _storedGravityScale;
		_body.WakeUp ();

		ActivateControl();
	}
	
	// Update is called once per frame
	void Update () {

		CheckGround ();

		var finalSpeed = Speed + Mathf.Abs(_body.velocity.x) * 0.05f;

		if (_jumpTimer > 0f)
			_jumpTimer -= Time.fixedDeltaTime;

		if (_jumping && _jumpTimer <= 0f) {
			var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, JumpLayerMask);
			if (hit.collider != null)
				_jumping = false;
		}
		
		if (Input.GetKey (KeyCode.LeftArrow)) {
			_body.AddForce(new Vector2(-finalSpeed, 0));
		}
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			_body.AddForce(new Vector2(finalSpeed, 0));
		}

		if (!_jumping && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow))) {
			if (_isHittingGround) {
				AudioHandler.Play("jump");
				_jumping = true;
				_jumpTimer = 0.15f;
				var finalJumpForce = JumpForce;
				if (_body.velocity.y > 0 && _isHittingWall)
					finalJumpForce += Mathf.Abs(_body.velocity.y) * 0.5f;
				_body.AddForce(new Vector2(0, finalJumpForce));
			}
		}

		UpdateAudio ();
		
	}
	
	public void CheckGround() {

		_isHittingGround = false;
		
		Vector2 source = transform.position;
		var dir = Vector2.zero;

		var radius = 0.3f;

		var hits = Physics2D.CircleCastAll(source, radius, Vector2.zero, 0f, JumpLayerMask);

		for (var i = 0; i < hits.Length; i++) {
			var hit = hits[i];
			
			if (hit.collider == null)
				continue;

			_isHittingGround = true;

			dir += (hit.point - source).normalized;

		}

		dir.Normalize ();
		
		_body.AddForce(dir * 0.5f);

		//Debug.DrawRay (transform.position, dir * radius, Color.black);

		_isHittingWall = Mathf.Abs(dir.x) > 0.85f;

		if (!_isHittingGround)
			_shouldHitSound = true;

	}
	
	public void UpdateAudio() {
		
		var newVolume = Mathf.Min (Mathf.Abs(_body.velocity.magnitude) / 30.0f, 0.75f);
		if (!_isHittingGround)
			newVolume /= 10f;
		
		_audio.volume -= (_audio.volume - newVolume) * 0.1f;
		
		_audio.pitch = Mathf.Clamp (Mathf.Abs(_body.angularVelocity) / 1250.0f, 0.25f, 3f);

	}
	
	public void StopAudio() {
		_audio.Stop ();
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (_firstHit) {
			_firstHit = false;
			return;
		}
		if (!_shouldHitSound)
			return;
		_shouldHitSound = false;
		AudioHandler.Play("hit" + Random.Range(1, 3).ToString(), 0.5f);
	}

}
