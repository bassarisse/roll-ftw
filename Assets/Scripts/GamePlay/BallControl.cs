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
	bool _jumping;
	bool _firstHit;
	bool _isHittingGround;
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
		_jumping = false;
		_firstHit = true;
		_isHittingGround = false;
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

		if (_jumping) {
			var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.35f, JumpLayerMask);
			if (hit.collider != null)
				_jumping = false;
		}
		
		if (Input.GetKey (KeyCode.LeftArrow)) {
			_body.AddForce(new Vector2(-Speed, 0));
		}
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			_body.AddForce(new Vector2(Speed, 0));
		}

		if (!_jumping && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow))) {
			if (_isHittingGround) {
				AudioHandler.Play("jump");
				_jumping = true;
				_body.AddForce(new Vector2(0, JumpForce));
			}
		}

		UpdateAudio ();
		
	}
	
	public void CheckGround() {

		var hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.5f, JumpLayerMask);
		_isHittingGround = hit.collider != null;
		
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
