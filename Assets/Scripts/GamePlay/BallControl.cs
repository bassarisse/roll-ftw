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

	// Use this for initialization
	void Start () {
		
		AudioHandler.Load ("jump");
		AudioHandler.Load ("hit1");
		AudioHandler.Load ("hit2");
		AudioHandler.Load ("hit3");
		
		_body = GetComponent<Rigidbody2D> ();
		_audio = GetComponent<AudioSource> ();
		_jumping = false;
		_firstHit = true;

		Messenger.AddListener ("LevelStart", ActivateControl);

		this.enabled = false;
	
	}
	
	void ActivateControl() {
		this.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (_jumping) {
			var jumpHit = Physics2D.Raycast(transform.position, Vector2.down, 0.35f, JumpLayerMask);
			if (jumpHit.collider != null) {
				_jumping = false;
			}
		}
		
		if (Input.GetKey (KeyCode.LeftArrow)) {
			_body.AddForce(new Vector2(-Speed, 0));
		}
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			_body.AddForce(new Vector2(Speed, 0));
		}
		
		var hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.5f, JumpLayerMask);
		var isHittingGround = hit.collider != null;

		if (!_jumping && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow))) {
			if (isHittingGround) {
				AudioHandler.Play("jump");
				_jumping = true;
				_body.AddForce(new Vector2(0, JumpForce));
			}
		}
		
		var newVolume = Mathf.Min (Mathf.Abs(_body.velocity.magnitude) / 30.0f, 0.75f);
		if (!isHittingGround)
			newVolume /= 10f;
		
		_audio.volume -= (_audio.volume - newVolume) * 0.1f;

		_audio.pitch = Mathf.Max (Mathf.Min (Mathf.Abs(_body.angularVelocity) / 1250.0f, 3f), 0.25f);
	
	}

	public void StopAudio() {
		_audio.Stop ();
	}
	
	void OnCollisionEnter2D(Collision2D collision) {
		if (_firstHit) {
			_firstHit = false;
			return;
		}
		AudioHandler.Play("hit" + Random.Range(1, 3).ToString(), 0.3f);
	}

}
