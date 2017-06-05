using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Pos{
	public int time;
	public Vector2 pos;
};
[System.Serializable]
public struct Motion{
	[Range(1,120)]public int atstart;
	[Range(1,120)]public int atend;
	[Range(1,120)]public int end;
	public bool isContinuous;
	public List<Pos> pos;
	public List<Pos> dash;

};
public class Fighter : MonoBehaviour {
	[SerializeField]Motion[] mot;
	public GameObject RotationObject;
	InputManager im;
	Animator anm;
	Rigidbody2D rg ;
	Vector2 acc ;
	int attackTime = 0;
	bool isMove = false;
	bool isLand = false;
	bool isSit = false;
	bool direction = true;
	float angle;
	int attackNum = 0;
	// Use this for initialization
	void Start () {
		anm = GetComponent<Animator> ();
		acc = new Vector2 (0,0);
		rg = GetComponent<Rigidbody2D> ();
		anm = GetComponent<Animator> ();
		angle = 90;
		im = GetComponent<InputManager> ();
	}
	void SetMotion (bool IsLand,bool IsMoving,int Attack,bool IsSit){
		bool Change = false;


		if (anm.GetBool ("isLand") != IsLand) {
			if(Attack == 0)Change = true;
			anm.SetBool ("isLand", IsLand);
			//Debug.Log (anm.GetBool ("isLand"));
		}
		if (anm.GetBool ("isMoving") != IsMoving) {
			if(isLand == true)Change = true;
			anm.SetBool ("isMoving", IsMoving);
		}
		if (anm.GetBool ("isSit") != IsSit) {
			Change = true;
			anm.SetBool ("isSit", IsSit);
		}
		if (anm.GetInteger ("AttackNum") != Attack) {
			Change = true;
			anm.SetInteger ("AttackNum", Attack);
		}
		if(Change == true)anm.SetTrigger("ChangeMotion");
	}
	// Update is called once per frame
	void AngleAim(float aim,ref float angle){
		if (aim > angle) {
			angle += Mathf.Abs (aim - angle)/3;
		}
		if (aim < angle) {
			angle -= Mathf.Abs (aim - angle)/3;
		}

	}
	void FixedUpdate(){
		if (attackTime == 0) {
			isSit = false;
			isMove = false;
			if (im.up >= 1 && isLand == true) {
				acc.y = 15;
				if (isLand == false)
					anm.SetTrigger ("ChangeMotion");
				isLand = false;

			}
			if (im.right >= 1 || im.left >= 1) {
				if (im.right >= 1) {
					acc.x = 12;
					direction = true;
				}
				if (im.left >= 1) {
					acc.x = -12;
					direction = false;
				}
				isMove = true;

			} else {
				acc.x = 0;
			}
	

			if (im.down >= 1 && isLand == true) {
				isSit = true;
				isMove = false;
				acc.x = 0;

			}
			if (im.a >= 1) {
				attackNum = 1;
				if (isMove == true)
					attackNum = 2;
				if (isSit == true)
					attackNum = 4;
				if (isLand == false)
					attackNum = 3;

				isMove = false;
				acc.x = 0;

			}
		}
	if (attackNum == 0) {
		attackTime = 0;
	} else {
		attackTime++;
		if (Mathf.Abs (acc.x) != 0) {
			acc.x /= 1.2f;
		}
		foreach (Pos a in mot[attackNum - 1].dash) {
					if (a.time == attackTime) {
						if (direction == false) {
							acc.x = -a.pos.x;
						}
						if (direction == true) {
							acc.x = a.pos.x;
						}
						acc.y+=a.pos.y;
			}
		}
		if (attackTime >= mot [attackNum - 1].end) {
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
		if (acc.y > 0 && isLand == true) {
			isLand = false;
		}
	RotationObject.transform.rotation = Quaternion.AngleAxis (angle,new Vector3(0,1,0));
	rg.velocity = acc;
	SetMotion (isLand, isMove, attackNum,isSit);
}

void OnCollisionStay2D(Collision2D c){
	if (c.transform.tag == "Land") {
		if (isLand == false&&acc.y<0) {
			attackNum = 0;
			attackTime = 0;
			isLand = true;
				SetMotion (isLand, isMove, attackNum,isSit);
		}
	}
}
}
