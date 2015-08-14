using UnityEngine;
using System.Collections;

public class CameraFollowMe : MonoBehaviour {

	// Use this for initialization
	void Start () {

		var camera = Camera.main;

		if (camera == null)
			return;

		var cameraFollow = camera.GetComponent<CameraFollow> ();
		cameraFollow.target = this.gameObject;
	
	}
}
