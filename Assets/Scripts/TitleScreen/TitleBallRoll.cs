using UnityEngine;
using System.Collections;

public class TitleBallRoll : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.forward, -360f * Time.fixedDeltaTime);

	}
}
