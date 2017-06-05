using UnityEngine;
using System.Collections;

public class FighterOld : MonoBehaviour {
	//public Vector2
	InputManager im;
	Animator anm;
	Rigidbody2D rg ;
	Vector2 acc ;
	int attackTime = 0;
	int[] attackTimeLimit ;
	bool isMove = false;
	bool isLand = false;
	bool direction = true;
	float angle;
	int attackNum = 0;
	// Use this for initialization
	void Start () {
		attackTimeLimit = new int[]{25};
		acc = new Vector2 (0,0);
		rg = GetComponent<Rigidbody2D> ();
		anm = GetComponent<Animator> ();
		angle = 90;
		im = GetComponent<InputManager> ();
	}
	void SetMotion (bool IsLand,bool IsMoving,int Attack){
		bool Change = false;


		if (anm.GetBool ("isLand") != IsLand) {
			Change = true;
			anm.SetBool ("isLand", IsLand);
			//Debug.Log (anm.GetBool ("isLand"));
		}
		if (anm.GetBool ("isMoving") != IsMoving) {
			if(isLand == true)Change = true;
			anm.SetBool ("isMoving", IsMoving);
		}
		if (anm.GetInteger ("atkNum") != Attack) {
			Change = true;
			anm.SetInteger ("atkNum", Attack);
		}
		if(Change == true)anm.SetTrigger("ChangeMotion");
	}
	void AngleAim(float aim,ref float angle){
		if (aim > angle) {
			angle += Mathf.Abs (aim - angle)/8;
		}
		if (aim < angle) {
			angle -= Mathf.Abs (aim - angle)/8;
		}

	}
	// Update is called once per frame
	void FixedUpdate () {
		

		if (attackTime == 0) {
			isMove = false;
			if (im.up >= 1&&isLand == true) {
				acc.y = 15;
				if (isLand == false)
					anm.SetTrigger ("ChangeMotion");
				isLand = false;

			}
			if (im.right>=1||im.left >= 1) {
				if (im.right>=1) {
					acc.x = 6;
					direction = true;
				}
				if (im.left>=1) {
					acc.x = -6;
					direction = false;
				}
				isMove = true;

			} else {
				acc.x = 0;
			}
		}
		if (im.a >= 1) {
			attackNum = 1;
		}
		if (attackNum == 0) {
			attackTime = 0;
		} else {
			attackTime++;
			if (attackTime >= attackTimeLimit [attackNum - 1]) {
				attackNum = 0;
			}
		}
		if (isLand == false) {
			acc.y -= 0.5f;
		}
		else {
			if(acc.y<0)
			acc.y = 0;
		}
		if (direction == true)
			AngleAim (90,ref angle);
		if (direction == false)
			AngleAim (270,ref angle);

			transform.rotation = Quaternion.AngleAxis (angle,new Vector3(0,1,0));
		rg.velocity = acc;
		SetMotion (isLand, isMove, attackNum);
	}
	void OnCollisionStay2D(Collision2D c){
		if (c.transform.tag == "Land") {
			if (isLand == false&&acc.y<0) {
				attackNum = 0;
				attackTime = 0;
				isLand = true;
				SetMotion (isLand, isMove, attackNum);
			}
		}
	}
}
