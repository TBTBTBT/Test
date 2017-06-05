
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UnityEventArg<Type> : UnityEvent<Type>{};
public class EventManager : MonoBehaviour {
	
	#region Events

	static public UnityEvent<int> OnCatchOSC;
	static public UnityEvent<int> OnGetVoltage;

	#endregion

	// Use this for initialization
	void Awake() {
		SetEvent (ref OnCatchOSC);
		SetEvent(ref OnGetVoltage);
	}

	#region Methods

	void SetEvent(ref UnityEvent u ){
		if (u == null) {
			u = new UnityEvent ();
		}
	}
	void SetEvent<Type>(ref UnityEvent<Type> u ){
		if (u == null) {
			u = new UnityEventArg<Type>();
		}
	}

	static public void Invoke(ref UnityEvent u ){
		if (u != null) {
			u.Invoke();
		}
	}
	static public void Invoke<Type>(ref UnityEvent<Type> u,Type arg)
	{
		if (u != null)
		{
			u.Invoke(arg);
		}
	}

	#endregion
	//がんばった
}


