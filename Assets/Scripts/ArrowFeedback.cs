using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowFeedback : MonoBehaviour {
	
	public bool ReceiveInputs = false;
	public ArrowDirection ArrowDirection = ArrowDirection.Up;
	public ArrowDirection ExtraArrowDirection = ArrowDirection.None;
	public Vector2 Shake = new Vector2(0, 0);
	public float ShakeTime = 0.5f;
	public Graphic[] LinkedGraphics;

	Graphic _target;
	Vector2 _startPosition;
	Vector2[] _linkedStartPositions;
	float _shakeTimer;
	
	// Use this for initialization
	void Start () {
		
		_target = GetComponent<Graphic> ();
		_shakeTimer = 0f;

		if (_target == null) {
			this.enabled = false;
			return;
		} else {
			_startPosition = _target.rectTransform.anchoredPosition;
		}

		_linkedStartPositions = new Vector2[LinkedGraphics.Length];

		for (var i = 0; i < LinkedGraphics.Length; i++) {
			_linkedStartPositions[i] = LinkedGraphics[i].rectTransform.anchoredPosition;
		}

		Messenger.AddListener<ArrowDirection> ("ArrowFeedback", ReceiveFeedback);

	}
	
	// Update is called once per frame
	void Update () {

		if (!_target.enabled)
			return;
		
		if (_shakeTimer > 0f) {
			_shakeTimer -= Time.deltaTime;

			var shakeOffset = _shakeTimer <= 0f ? Vector2.zero : GetShakeOffset();

			_target.rectTransform.anchoredPosition = _startPosition + shakeOffset;

			for (var i = 0; i < LinkedGraphics.Length; i++) {
				LinkedGraphics[i].rectTransform.anchoredPosition = _linkedStartPositions[i] + shakeOffset;
			}

		}

		if (!ReceiveInputs)
			return;

		if ((ArrowDirection == ArrowDirection.Up    || ExtraArrowDirection == ArrowDirection.Up   ) && InputExtensions.Pressed.Up   ||
		    (ArrowDirection == ArrowDirection.Down  || ExtraArrowDirection == ArrowDirection.Down ) && InputExtensions.Pressed.Down ||
		    (ArrowDirection == ArrowDirection.Left  || ExtraArrowDirection == ArrowDirection.Left ) && InputExtensions.Pressed.Left ||
		    (ArrowDirection == ArrowDirection.Right || ExtraArrowDirection == ArrowDirection.Right) && InputExtensions.Pressed.Right) {
			StartTimer();
		}
		
	}

	Vector2 GetShakeOffset() {
		return new Vector2(Mathf.RoundToInt(Shake.x * Random.value - Shake.x / 2),
		                   Mathf.RoundToInt(Shake.y * Random.value - Shake.y / 2));
	}

	void ReceiveFeedback(ArrowDirection arrowDirection) {
		if (arrowDirection == this.ArrowDirection || arrowDirection == this.ExtraArrowDirection)
			StartTimer();
	}

	void StartTimer() {
		_shakeTimer = ShakeTime;
	}
	
	public static void Broadcast(ArrowDirection arrowDirection) {
		Messenger.Broadcast<ArrowDirection>("ArrowFeedback", arrowDirection);
	}
	
	public static void Up() {
		Broadcast(ArrowDirection.Up);
	}
	
	public static void Down() {
		Broadcast(ArrowDirection.Down);
	}
	
	public static void Left() {
		Broadcast(ArrowDirection.Left);
	}
	
	public static void Right() {
		Broadcast(ArrowDirection.Right);
	}

}

public enum ArrowDirection {
	Up,
	Down,
	Left,
	Right,
	None
}