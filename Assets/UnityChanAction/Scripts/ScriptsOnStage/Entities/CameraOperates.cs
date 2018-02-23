using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraOperates : MonoBehaviour {
	[SerializeField] Const.EnumCameraDir direction;
	public GameObject unityChan;
	public bool isFreezing = false;
	public int howLongToArrive;
	protected bool readyToWork = true;
	private Vector3 keepPos;
	private bool zoomNow = false;
	private bool cannotTouch = false;

	void Awake () {
		this.direction = Const.EnumCameraDir.Usually;
		this.tweenAction(this.getCoordToMove(), this.getAngle(), Ease.InFlash);
	}
	
	void Update () {
		if(!isFreezing){

			if(this.readyToWork){
				this.inputWorking();
				this.followMove();
			}

			if(Input.GetKeyDown(KeyCode.Z)){
				StartCoroutine(zoomForward());
			}
		}
	}

	//キーボード操作によるカメラワーク
	public void inputWorking(){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			this.direction = Const.EnumCameraDir.Usually;
			this.tweenAction(this.getCoordToMove(), this.getAngle(), Ease.InFlash);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			this.direction = Const.EnumCameraDir.Right;
			this.tweenAction(this.getCoordToMove(), this.getAngle(), Ease.InFlash);
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			this.direction = Const.EnumCameraDir.Left;
			this.tweenAction(this.getCoordToMove(), this.getAngle(), Ease.InFlash);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			this.direction = Const.EnumCameraDir.Opposition;
			this.tweenAction(this.getCoordToMove(), this.getAngle(), Ease.InFlash);
		}
	}

	//スクリプト操作によるカメラワーク
	public void caughtWorking(Const.EnumCameraDir selectDir){
		this.direction = selectDir;
		this.tweenAction(this.getCoordToMove(), this.getAngle(), Ease.InFlash);
	}

	public void followMove(){
		transform.position = this.getCoordToMove();
	}

	public void tweenAction(Vector3 coord, Vector3 rotate, Ease easeType){
		this.readyToWork = false;
		transform.DOMove(coord, this.howLongToArrive)
			/*.SetEase(easeType)*/.OnComplete(() => this.readyToWork = true);
		transform.DORotate(rotate, this.howLongToArrive);
	}

	//カメラが動く方向を取得
	public Vector3 getCoordToMove(){
		switch(this.direction){
			default:
				return this.unityChan.transform.position 
					+ new Vector3(0.0f, 5.0f, -8.0f);

			case Const.EnumCameraDir.Right:
				return this.unityChan.transform.position 
					+ new Vector3(5.0f, 4.0f, 0f);

			case Const.EnumCameraDir.Left:
				return this.unityChan.transform.position 
					+ new Vector3(-5.0f, 4.0f, 0f);

			case Const.EnumCameraDir.Opposition:
				return this.unityChan.transform.position 
					+ new Vector3(0f, 5.0f, 8.0f);
		}
	}

	//カメラアングルを取得
	public Vector3 getAngle(){
		switch(this.direction){
			default:
				return new Vector3(25.0f, 0f, 0f);

			case Const.EnumCameraDir.Right:
				return new Vector3(27.0f, 270f, 0f);

			case Const.EnumCameraDir.Left:
				return new Vector3(27.0f, 90f, 0f);

			case Const.EnumCameraDir.Opposition:
				return new Vector3(25.0f, 180f, 0f);
		}
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
				transform.position = this.keepPos + new Vector3(0F, 0F - f / 20, 0F + f / 2);

				yield return null;
			}
			this.cannotTouch = false;

		//Zキーが押されるたびに、zoomNowとcannotTouchのtrueとfalseを切り替え。
		}else if(this.zoomNow && !this.cannotTouch){
			//カメラのズーム位置を代入
			this.keepPos = transform.position;

			//ズーム終了
			for(int i = 0; i < 12; i++){
				float f = (float)i;
				transform.position = this.keepPos + new Vector3(0F, 0F + f / 20, 0F - f / 2);

				yield return null;
			}

			//ズームモードとプレイヤーのフリーズを解除。
			this.zoomNow = false;
			this.freezePlayer(false);
		}
	}

	private void freezePlayer(bool not){
		UnityChanMoves cs = this.unityChan.GetComponent<UnityChanMoves>();

		if(cs != null){
			cs.isFreezing = not;
		}
	}
}
