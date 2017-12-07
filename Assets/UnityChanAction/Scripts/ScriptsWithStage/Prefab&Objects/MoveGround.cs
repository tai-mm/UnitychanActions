using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour {
	public Vector3 basePos;
	public float limitedPos;
	public float moveSpeed;
	private bool isTurning = false;

	void Start () {
		this.basePos = transform.position;
	}
	
	void Update () {
		//常にx座標を記録しておく
		float inX = transform.position.x;

		//起点座標より、x軸が"limitedPos"分だけ離れているか
		if(inX - this.basePos.x > this.limitedPos ||
			inX - this.basePos.x < -this.limitedPos){

			//リターン
			if(this.isTurning){
				this.isTurning = false;
			}else{
				this.isTurning = true;
			}
		}
		if(this.isTurning){
			transform.position += new Vector3
				(-this.moveSpeed * Time.deltaTime, 0F, 0F);
		}else{
			transform.position += new Vector3
				(this.moveSpeed * Time.deltaTime, 0F, 0F);
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
