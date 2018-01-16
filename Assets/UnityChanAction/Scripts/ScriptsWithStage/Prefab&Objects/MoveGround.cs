using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour {
	public Vector3 basePos;
	public float limitedPos;
	public float moveSpeed;

	void Start () {
		this.basePos = transform.position;
	}
	
	void Update () {
		transform.position = 
			this.basePos + new Vector3(Mathf.Sin(Time.time * this.moveSpeed), 0f, 0f);
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
