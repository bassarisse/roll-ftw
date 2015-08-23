using UnityEngine;
using System.Collections;

public class InputHelper : MonoBehaviour {

#if UNITY_STANDALONE_OSX
	private const string INPUT_HORIZONTAL_AXIS = "Horizontal.osx";
	private const string INPUT_VERTICAL_AXIS = "Vertical.osx";
	private const string INPUT_A = "A.osx";
	private const string INPUT_B = "B.osx";
	private const string INPUT_X = "X.osx";
	private const string INPUT_Y = "Y.osx";
	private const string INPUT_L = "L.osx";
	private const string INPUT_R = "R.osx";
	private const string INPUT_LSTICK = "LStick.osx";
	private const string INPUT_RSTICK = "RStick.osx";
	private const string INPUT_START = "Start.osx";
	private const string INPUT_BACK = "Back.osx";
#elif UNITY_STANDALONE_LINUX
	private const string INPUT_HORIZONTAL_AXIS = "Horizontal.linux";
	private const string INPUT_VERTICAL_AXIS = "Vertical.linux";
	private const string INPUT_A = "A";
	private const string INPUT_B = "B";
	private const string INPUT_X = "X";
	private const string INPUT_Y = "Y";
	private const string INPUT_L = "L";
	private const string INPUT_R = "R";
	private const string INPUT_LSTICK = "LStick.linux";
	private const string INPUT_RSTICK = "RStick.linux";
	private const string INPUT_START = "Start";
	private const string INPUT_BACK = "Back";
#else
	private const string INPUT_HORIZONTAL_AXIS = "Horizontal";
	private const string INPUT_VERTICAL_AXIS = "Vertical";
	private const string INPUT_A = "A";
	private const string INPUT_B = "B";
	private const string INPUT_X = "X";
	private const string INPUT_Y = "Y";
	private const string INPUT_L = "L";
	private const string INPUT_R = "R";
	private const string INPUT_LSTICK = "LStick";
	private const string INPUT_RSTICK = "RStick";
	private const string INPUT_START = "Start";
	private const string INPUT_BACK = "Back";
#endif
	
	bool _shouldUpdate = true;
	
	InputState _isUpPressed = InputState.Idle;
	InputState _isDownPressed = InputState.Idle;
	InputState _isLeftPressed = InputState.Idle;
	InputState _isRightPressed = InputState.Idle;
	
	ButtonStateRetriever _upStateRetriever = null;
	ButtonStateRetriever _downStateRetriever = null;
	ButtonStateRetriever _holdStateRetriever = null;
	
	void Awake () {

		DontDestroyOnLoad(gameObject);
		
		_upStateRetriever = new ButtonUpRetriever (this);
		_downStateRetriever = new ButtonDownRetriever (this);
		_holdStateRetriever = new ButtonHoldRetriever (this);

	}
	
	void Start() {

		_shouldUpdate = true;

		_isUpPressed = InputState.Idle;
		_isDownPressed = InputState.Idle;
		_isLeftPressed = InputState.Idle;
		_isRightPressed = InputState.Idle;

	}
	
	void LateUpdate() {
		
		_shouldUpdate = true;
		
	}
	
	void UpdateAllAxis(bool forceUpdate = false) {
		
		if (!_shouldUpdate && !forceUpdate)
			return;
		
		_shouldUpdate = false;
		
		UpdateAxisState ((int)Input.GetAxisRaw (INPUT_VERTICAL_AXIS), ref _isUpPressed, ref _isDownPressed);
		UpdateAxisState ((int)Input.GetAxisRaw (INPUT_HORIZONTAL_AXIS), ref _isRightPressed, ref _isLeftPressed);
		
	}
	
	void UpdateAxisState(int axis, ref InputState positiveState, ref InputState negativeState) {
		
		UpdateAxisState (axis, 1, ref positiveState);
		UpdateAxisState (axis, -1, ref negativeState);
		
	}
	
	void UpdateAxisState(int axis, int desiredValue, ref InputState state) {
		
		if (axis == desiredValue) {
			if (state == InputState.Idle) {
				state = InputState.Pressed;
			} else if (state == InputState.Pressed) {
				state = InputState.Holding;
			}
		} else {
			if (state == InputState.Pressed || state == InputState.Holding) {
				state = InputState.Released;
			} else if (state == InputState.Released) {
				state = InputState.Idle;
			}
		}
		
	}
	
	public IButtonStateRetriever Up {
		get {
			return _upStateRetriever;
		}
	}
	
	public IButtonStateRetriever Down {
		get {
			return _downStateRetriever;
		}
	}
	
	public IButtonStateRetriever Hold {
		get {
			return _holdStateRetriever;
		}
	}
	
	public bool GetUpArrowState(InputPressState pressState) {
		if (pressState == InputPressState.Down)
			return this.UpArrowDown;
		if (pressState == InputPressState.Up)
			return this.UpArrowUp;
		return this.UpArrow;
	}
	
	public bool GetDownArrowState(InputPressState pressState) {
		if (pressState == InputPressState.Down)
			return this.DownArrowDown;
		if (pressState == InputPressState.Up)
			return this.DownArrowUp;
		return this.DownArrow;
	}
	
	public bool GetLeftArrowState(InputPressState pressState) {
		if (pressState == InputPressState.Down)
			return this.LeftArrowDown;
		if (pressState == InputPressState.Up)
			return this.LeftArrowUp;
		return this.LeftArrow;
	}
	
	public bool GetRightArrowState(InputPressState pressState) {
		if (pressState == InputPressState.Down)
			return this.RightArrowDown;
		if (pressState == InputPressState.Up)
			return this.RightArrowUp;
		return this.RightArrow;
	}
	
	private bool UpArrowUp {
		get {
			UpdateAllAxis();
			return _isUpPressed == InputState.Released;
		}
	}
	
	private bool UpArrow {
		get {
			UpdateAllAxis();
			return _isUpPressed == InputState.Pressed || _isUpPressed == InputState.Holding;
		}
	}
	
	private bool UpArrowDown {
		get {
			UpdateAllAxis();
			return _isUpPressed == InputState.Pressed;
		}
	}
	
	private bool DownArrowUp {
		get {
			UpdateAllAxis();
			return _isDownPressed == InputState.Released;
		}
	}
	
	private bool DownArrow {
		get {
			UpdateAllAxis();
			return _isDownPressed == InputState.Pressed || _isDownPressed == InputState.Holding;
		}
	}
	
	private bool DownArrowDown {
		get {
			UpdateAllAxis();
			return _isDownPressed == InputState.Pressed;
		}
	}
	
	private bool LeftArrowUp {
		get {
			UpdateAllAxis();
			return _isLeftPressed == InputState.Released;
		}
	}
	
	private bool LeftArrow {
		get {
			UpdateAllAxis();
			return _isLeftPressed == InputState.Pressed || _isLeftPressed == InputState.Holding;
		}
	}
	
	private bool LeftArrowDown {
		get {
			UpdateAllAxis();
			return _isLeftPressed == InputState.Pressed;
		}
	}
	
	private bool RightArrowUp {
		get {
			UpdateAllAxis();
			return _isRightPressed == InputState.Released;
		}
	}
	
	private bool RightArrow {
		get {
			UpdateAllAxis();
			return _isRightPressed == InputState.Pressed || _isRightPressed == InputState.Holding;
		}
	}
	
	private bool RightArrowDown {
		get {
			UpdateAllAxis();
			return _isRightPressed == InputState.Pressed;
		}
	}
	
	public enum InputState {
		Idle,
		Pressed,
		Holding,
		Released
	}
	
	public enum InputPressState {
		Up,
		Down,
		Hold
	}

	abstract class ButtonStateRetriever : IButtonStateRetriever {

		private InputHelper _helper;
		
		public ButtonStateRetriever() {
			throw new UnityException();
		}
		
		public ButtonStateRetriever(InputHelper helper) {
			_helper = helper;
		}
		
		abstract protected bool GetButtonState (string buttonName);
		
		abstract protected InputPressState GetInputPressState ();
		
		public bool Up {
			get {
				return _helper.GetUpArrowState(GetInputPressState());
			}
		}
		
		public bool Down {
			get {
				return _helper.GetDownArrowState(GetInputPressState());
			}
		}
		
		public bool Left {
			get {
				return _helper.GetLeftArrowState(GetInputPressState());
			}
		}
		
		public bool Right {
			get {
				return _helper.GetRightArrowState(GetInputPressState());
			}
		}
		
		public bool A {
			get {
				return GetButtonState(INPUT_A);
			}
		}
		
		public bool B {
			get {
				return GetButtonState(INPUT_B);
			}
		}
		
		public bool X {
			get {
				return GetButtonState(INPUT_X);
			}
		}
		
		public bool Y {
			get {
				return GetButtonState(INPUT_Y);
			}
		}
		
		public bool L {
			get {
				return GetButtonState(INPUT_L);
			}
		}
		
		public bool R {
			get {
				return GetButtonState(INPUT_R);
			}
		}
		
		public bool LStick {
			get {
				return GetButtonState(INPUT_LSTICK);
			}
		}
		
		public bool RStick {
			get {
				return GetButtonState(INPUT_RSTICK);
			}
		}
		
		public bool Start {
			get {
				return GetButtonState(INPUT_START);
			}
		}
		
		public bool Back {
			get {
				return GetButtonState(INPUT_BACK);
			}
		}

	}
	
	class ButtonDownRetriever : ButtonStateRetriever {
		
		public ButtonDownRetriever(InputHelper helper) : base(helper) {}
		
		override protected bool GetButtonState(string buttonName) {
			return Input.GetButtonDown(buttonName);
		}
		
		override protected InputPressState GetInputPressState() {
			return InputPressState.Down;
		}
		
	}
	
	class ButtonUpRetriever : ButtonStateRetriever {
		
		public ButtonUpRetriever(InputHelper helper) : base(helper) {}
		
		override protected bool GetButtonState(string buttonName) {
			return Input.GetButtonUp(buttonName);
		}
		
		override protected InputPressState GetInputPressState() {
			return InputPressState.Up;
		}
		
	}
	
	class ButtonHoldRetriever : ButtonStateRetriever {
		
		public ButtonHoldRetriever(InputHelper helper) : base(helper) {}
		
		override protected bool GetButtonState(string buttonName) {
			return Input.GetButton(buttonName);
		}
		
		override protected InputPressState GetInputPressState() {
			return InputPressState.Hold;
		}
		
	}
	
}

public interface IButtonStateRetriever {
	
	bool Up { get; }
	bool Down { get; }
	bool Left { get; }
	bool Right { get; }
	bool A { get; }
	bool B { get; }
	bool X { get; }
	bool Y { get; }
	bool L { get; }
	bool R { get; }
	bool LStick { get; }
	bool RStick { get; }
	bool Start { get; }
	bool Back { get; }
	
}

static internal class InputExtensions {
	
	//Disable the unused variable warning
	#pragma warning disable 0414
	static private InputHelper _inputHelper = ( new GameObject("InputHelper") ).AddComponent<InputHelper>();
	#pragma warning restore 0414
	
	static public IButtonStateRetriever Pressed {
		get {
			return _inputHelper.Down;
		}
	}
	
	static public IButtonStateRetriever Down {
		get {
			return _inputHelper.Down;
		}
	}
	
	static public IButtonStateRetriever Released {
		get {
			return _inputHelper.Up;
		}
	}
	
	static public IButtonStateRetriever Up {
		get {
			return _inputHelper.Up;
		}
	}
	
	static public IButtonStateRetriever Holding {
		get {
			return _inputHelper.Hold;
		}
	}
	
	static public IButtonStateRetriever Is {
		get {
			return _inputHelper.Hold;
		}
	}
	
}
