using UnityEngine;
using System.Collections;

public class CameraFollowMe : MonoBehaviour {
	
	public bool ResetStartPosition = false;
	public Vector2 StartPosition = Vector2.zero;

	void Start () {
		Follow ();
	}

	void Update () {
		Follow ();
	}

	void Follow() {
		
		var camera = Camera.main;
		
		if (camera == null)
			return;
		
		var cameraFollow = camera.GetComponent<CameraFollow> ();
		
		if (cameraFollow == null)
			return;
		
		if (cameraFollow.target != null)
			return;
		
		cameraFollow.target = this.gameObject;

		if (ResetStartPosition)
			cameraFollow.SetPosition(StartPosition);
		
		this.enabled = false;

	}
}
