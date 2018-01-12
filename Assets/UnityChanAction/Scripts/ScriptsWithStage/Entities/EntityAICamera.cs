using UnityEngine;
using System.Collections;

public class EntityAICamera : MonoBehaviour {
	public GameObject unityChan;
	public bool isFreezing = false;
	public int direction = 1;
	private Vector3 keepPos;
	private bool zoomNow = false;
	private bool cannotTouch = false;

	void Start () {
		this.direction = 1;
	}
	
	void Update () {
		if(!isFreezing){

			//"zoomNow"がtrueなら、カメラ位置調整メソッドを停止。
			if(!this.zoomNow){
				this.chageDirection();

			}

			if(Input.GetKeyDown(KeyCode.Z)){
				StartCoroutine(zoomForward());
			}
		}
	}

	//カメラの向きを変える 
	protected void chageDirection(){
		switch(this.direction){
			default:
				transform.position = 
					unityChan.transform.position + 
					new Vector3(0F, 4.0F, -5.0F);

				transform.eulerAngles = 
					new Vector3(27.0F, 0F, 0F);
			break;

			case 2:
				transform.position = 
					unityChan.transform.position + 
					new Vector3(-5.0F, 4.0F, 0F);

				transform.eulerAngles = 
					new Vector3(27.0F, 90F, 0F);
			break;

			case 3:
				transform.position = 
					unityChan.transform.position + 
					new Vector3(5.0F, 4.0F, 0F);

				transform.eulerAngles = 
					new Vector3(27.0F, 270F, 0F);
			break;

			case 4:
				transform.position = 
					unityChan.transform.position + 
					new Vector3(0F, 4.0F, 5.0F);

				transform.eulerAngles = 
					new Vector3(27.0F, 180F, 0F);
			break;
		}

		/*ゲーム初期位置を正面に、
			Direction:1 = 初期
			Direction:2 = 右90°
			Direction:3 = 左90°
			Direction:4 = 反転*/
	}

	//カメラを正面にズームイン
	IEnumerator zoomForward(){

		//Zキーが押されるたびに、zoomNowとcannotTouchのtrueとfalseを切り替え。
		if(!this.zoomNow && !this.cannotTouch){
			//プレイヤーをフリーズ
			this.freezePlayer(true);

			this.cannotTouch = true;
			this.zoomNow = true;

			//カメラの初期位置を代入
			this.keepPos = transform.position;

			//ズーム開始
			for(int i = 0; i < 12; i++){
				float f = (float)i;
				transform.position = this.keepPos + 
					new Vector3(0F, 0F - f / 20, 0F + f / 2);

				yield return null;
			}
			this.cannotTouch = false;

		}else if(this.zoomNow && !this.cannotTouch){
			//カメラのズーム位置を代入
			this.keepPos = transform.position;

			//ズーム終了
			for(int i = 0; i < 12; i++){
				float f = (float)i;
				transform.position = this.keepPos +
					new Vector3(0F, 0F + f / 20, 0F - f / 2);

				yield return null;
			}

			this.zoomNow = false;
			this.freezePlayer(false);
		}
	}

	private void freezePlayer(bool not){
		EntityAIUnityChanMoves cs = this.unityChan.
			GetComponent<EntityAIUnityChanMoves>();

		if(cs != null){
			cs.isFreezing = not;
		}
	}
}
