using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {
	
	void Update() {
		
		if (Input.touchCount == 0)
			return;

		var touch = Input.GetTouch(0);
		
		if (touch.phase == TouchPhase.Ended) {

			var angle = GetAngle(touch.position);

			if (angle == 0)
				Messenger.Broadcast("Touch.Right");
			else if (angle == 1)
				Messenger.Broadcast("Touch.Up");
			else if (angle == 2)
				Messenger.Broadcast("Touch.Left");
			else if (angle == 3)
				Messenger.Broadcast("Touch.Down");
			
		}

	}
	
	int GetAngle(Vector2 position) {
		
		var p1 = Vector2.right;
		var p2 = new Vector2 (position.x - Screen.width / 2, position.y - Screen.height / 2);
		
		var refSize = Screen.width > Screen.height ? Screen.width : Screen.height;
		
		if (p2.magnitude < refSize * 0.25f)
			return -1; // unknown
		
		var angle = Vector2.Angle (p1, p2);
		var cross = Vector3.Cross (p1, p2);
		
		if (cross.z < 0)
			angle = 360f - angle;
		
		if (angle > 60f  && angle <= 120f)
			return 1; // up
		
		if (angle > 150f && angle <= 210f)
			return 2; // left
		
		if (angle > 240f && angle <= 300f)
			return 3; // down
		
		if (angle > 330f || angle <= 30f)
			return 0; // right

		return -1; // unknown
	}
	
}