using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	public int mode = 0;
	public int up=0,down=0,left=0,right=0,a=0,b=0,c=0;
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
		if (mode == 0) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				up++;
			} else {
				up = 0;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				down++;
			} else {
				down = 0;
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				left++;
			} else {
				left = 0;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				right++;
			} else {
				right = 0;
			}
			if (Input.GetKey (KeyCode.Z)) {
			//	Debug.Log ("P");
				a++;
			} else {
			//	Debug.Log ("R");
				a = 0;
			}
			if (Input.GetKey (KeyCode.X)) {
				b++;
			} else {
				b = 0;
			}
			if (Input.GetKey (KeyCode.C)) {
				c++;
			} else {
				c = 0;
			}
		}
	}
}
