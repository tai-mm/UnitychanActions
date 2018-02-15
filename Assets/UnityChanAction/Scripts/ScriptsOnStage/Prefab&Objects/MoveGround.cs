using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour {
	[SerializeField] Vector3 basePos;//初期位置
	public GameObject spawner;//床をInstantiateするオブジェクト(主にフロート床の時に使用)
	public bool floatGround = false;//フロート床(一定方向に移動する床)かどうか
	public float limitedPos;//移動範囲の広さ
	public float moveSpeed;//移動速度
	public string status = "";//フロート床でない場合の、床の移動方向
	protected Vector3 vectorFor;//spawner側で定義されている、フロート床のベクトル

	void Start () {
		this.basePos = transform.position;
		this.spawner = transform.parent.gameObject;//spawner側で、生成時にspawnerの子供になるようになっている。
	}
	
	void Update () {
		//フロート床
		if(this.floatGround){
			this.boatfloat();
		//縦方向移動床
		}else if(this.status == "y"){
			transform.position = this.basePos + new Vector3
				(0f, -3.0f + Mathf.PingPong(Time.time * this.moveSpeed, this.limitedPos), 0f);
		//X方向移動床
		}else if(this.status == "x"){
			transform.position = 
				this.basePos + new Vector3(Mathf.Sin(Time.time * this.moveSpeed), 0f, 0f);
		//Z方向移動床
		}else{
			transform.position = 
				this.basePos + new Vector3(0f, 0f, Mathf.Sin(Time.time * this.moveSpeed));
		}

		//床のY座標が-5以下に達したら、消滅する。
		if(transform.position.y < -5.0f){
			Destroy(this.gameObject);
		}
	}

	//フロート床の動き
	public void boatfloat(){
		this.vectorFor = this.spawner.GetComponent<ObjectSpawner>().getVector();
		transform.position += this.vectorFor * Time.deltaTime * this.moveSpeed;
	}

	void OnCollisionEnter(Collision collision){
		GameObject obj = collision.gameObject;
		//プレイヤー乗った時、この床の子供として親子関係を設定する。
		if(obj.gameObject.tag == "Player"){
			obj.transform.SetParent(this.transform, true);
			// obj.transform.parent = this.transform;
		}
	}

	void OnCollisionStay(Collision collision){
		//床がY座標-5以下に達して消滅する時、ここでプレイヤーとの親子関係を解除する。
		GameObject obj = collision.gameObject;
		if(obj.tag == "Player" && transform.position.y < -5.0f){
			obj.transform.parent = null;
		}
	}

	void OnCollisionExit(Collision collision){
		//プレイヤーが床から離れると親子関係を解除。
		GameObject obj = collision.gameObject;
		obj.transform.parent = null;
	}
}
