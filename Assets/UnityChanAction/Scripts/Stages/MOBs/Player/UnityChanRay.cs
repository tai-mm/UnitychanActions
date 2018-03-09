using UnityEngine;
using System.Collections;

public class UnityChanRay : MonoBehaviour {
	public RaycastHit hit;
	public GameObject camera;
	private bool cameraStopper = false;

	//プレイヤーに付随する他のスクリプト
	private UnityChanStatus plaCsMain;
	private UnityChanMoves plaCsMove;
	private UnityChanUI plaCsUI;

	void Start () {
		this.plaCsMain = this.GetComponent<UnityChanStatus>();
		this.plaCsMove = this.GetComponent<UnityChanMoves>();
	}
	
	void Update () {
		if(!this.plaCsMain.isFreezing && Time.timeScale != 0){
			this.raysActivity();
			this.cancelRay();
		}
	}

	protected void raysActivity(){
		float maxDistance = 100.0F;
		bool rayHit = Physics.Raycast(transform.position + new Vector3(0F, 0.08F, 0F), 
			Vector3.down, out hit, maxDistance);
		if(rayHit){
			if(hit.collider.tag == "OutOfTheWorld"){

				//Unityちゃんのy座標が-7未満なら、カメラ固定
				if(this.transform.position.y < -7.0F){
					this.cameraStopper = true;
				}
			}
		}
	}

	private void cancelRay(){
		var cameCs = this.camera.
				GetComponent<CameraRotater>();

			//Unityちゃんのy座標が-7以上なら、カメラ固定を解除
			if(this.cameraStopper){
				cameCs.isFreezing = true;

				if(this.transform.position.y >= -7.0F || 
					this.plaCsMove.getGround()){
					this.cameraStopper = false;
				}
			}else{
				cameCs.isFreezing = false;
			}
	}
}
/*
"UnityChanRay"
-落下時のカメラ制御
*/
