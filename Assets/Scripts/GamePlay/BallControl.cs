using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {
	
	public float Speed = 2.0f;
	public float JumpForce = 100.0f;
	public LayerMask JumpLayerMask;
	
	Rigidbody2D _body;
	bool _jumping;

	// Use this for initialization
	void Start () {

		_body = GetComponent<Rigidbody2D> ();
		_jumping = false;

		Messenger.AddListener ("LevelStart", ActivateControl);

		this.enabled = false;
	
	}
	
	void ActivateControl() {
		this.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

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
			var hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.down, 0.5f, JumpLayerMask);
			if (hit.collider != null) {
				_jumping = true;
				_body.AddForce(new Vector2(0, JumpForce));
			}
		}
	
	}

}
