using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour {
	public Vector3 basePos;
	public float limitedPos;
	public float moveSpeed;
	public bool isUpDown = false;

	void Start () {
		this.basePos = transform.position;
	}
	
	void Update () {
		if(this.isUpDown){
			transform.position = this.basePos + new Vector3
				(0f, -3.0f + Mathf.PingPong(Time.time * this.moveSpeed, this.limitedPos), 0f);
		}else{
			transform.position = 
				this.basePos + new Vector3(Mathf.Sin(Time.time * this.moveSpeed), 0f, 0f);
		}
	}

	void OnCollisionEnter(Collision collision){
		GameObject obj = collision.gameObject;
		obj.transform.SetParent(this.transform, true);
		// obj.transform.parent = this.transform;
	}

	void OnCollisionExit(Collision collision){
		GameObject obj = collision.gameObject;
		obj.transform.parent = null;
	}
}
