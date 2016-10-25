using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class BallControl : MonoBehaviour {
	
	public float Speed = 2.0f;
	public float JumpForce = 100.0f;
	public LayerMask JumpLayerMask;

	float frameFactor = 1f / 60f;
	
	Rigidbody2D _body;
	AudioSource _audio;
	float _jumpTimer;
	float _leapTimer;
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
			SceneManager.LoadScene ("Game", LoadSceneMode.Additive);
		
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

	void FixedUpdate () {
		
		CheckGround ();
		
		var leftIsTouched = false;
		var rightIsTouched = false;
		
		if (Input.touchCount > 0 && Input.touchCount < 3) {
			
			var halfWidth = Screen.width / 2;
			var halfHeight = Screen.height / 2;
			
			for (var i = 0; i < Input.touchCount; i++) {
				var touch = Input.GetTouch (i);
				
				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
					
					if (!leftIsTouched && touch.position.y < halfHeight && touch.position.x < halfWidth)
						leftIsTouched = true;
					
					if (!rightIsTouched && touch.position.y < halfHeight && touch.position.x >= halfWidth)
						rightIsTouched = true;
					
				}
				
			}
		}
		
		var finalSpeed = (Speed / frameFactor * Time.fixedDeltaTime) + Mathf.Abs(_body.velocity.x) * 0.05f;
		
		if (leftIsTouched || InputExtensions.Holding.Left) {
			_body.AddForce(new Vector2(-finalSpeed, 0));
		}
		
		if (rightIsTouched || InputExtensions.Holding.Right) {
			_body.AddForce(new Vector2(finalSpeed, 0));
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (_isHittingGround)
			_leapTimer = 0f;
		else
			_leapTimer += Time.deltaTime;
		
		if (_jumpTimer > 0f)
			_jumpTimer -= Time.deltaTime;
		
		if (_jumping && _jumpTimer <= 0f) {
			var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, JumpLayerMask);
			if (hit.collider != null)
				_jumping = false;
		}

		var touchedUp = false;
		
		if (Input.touchCount > 0 && Input.touchCount < 3) {

			var halfHeight = Screen.height / 2;
			
			for (var i = 0; i < Input.touchCount; i++) {
				var touch = Input.GetTouch (i);
				
				if (touch.phase == TouchPhase.Began && touch.position.y >= halfHeight) {
					touchedUp = true;
					break;
				}
				
			}
		}

		if (touchedUp || InputExtensions.Pressed.A || InputExtensions.Pressed.Up) {
			Jump();
		}

		UpdateAudio ();
		
	}

	void Jump() {

		if (_jumping)
			return;

		if (!_isHittingGround && _leapTimer > 0.075f)
			return;

		AudioHandler.Play("jump");
		_jumping = true;
		_jumpTimer = 0.15f;
		var finalJumpForce = JumpForce;
		if (_body.velocity.y > 0 && _isHittingWall)
			finalJumpForce += Mathf.Abs(_body.velocity.y) * 0.5f;
		_body.AddForce(new Vector2(0, finalJumpForce));
		
	}
	
	void CheckGround() {

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
		
		_body.AddForce(dir * 0.5f / frameFactor * Time.fixedDeltaTime);

		//Debug.DrawRay (transform.position, dir * radius, Color.black);

		_isHittingWall = Mathf.Abs(dir.x) > 0.85f;

		if (!_isHittingGround)
			_shouldHitSound = true;

	}
	
	void UpdateAudio() {
		
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
