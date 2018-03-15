using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraRotater : MonoBehaviour {
	public GameObject player;
	public bool isFreezing = false;//プレイヤーがフリーズ状態であるか
	public int howLongToArrive;//移動先までの到達時間
	protected bool readyToWork = true;//カメラワークの準備ができているか

	void Awake () {
		CameraManager.Instance.nowDir = Const.EnumCameraDir.Usually;
		this.tweenActionMove(this.getCoordToMove(Const.EnumCameraDir.Usually), Ease.InFlash);
		this.tweenActionRotate(this.getAngle(Const.EnumCameraDir.Usually), Ease.InFlash);
	}
	
	void Update () {
		if(!isFreezing){

			if(this.readyToWork){
				this.followMove();
				this.inputWorking();
			}
		}
	}

	//キーボード操作によるカメラワーク
	public void inputWorking(){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			CameraManager.Instance.nowDir = Const.EnumCameraDir.Usually;
			this.tweenActionMove(this.getCoordToMove(Const.EnumCameraDir.Usually), Ease.InFlash);
			this.tweenActionRotate(this.getAngle(Const.EnumCameraDir.Usually), Ease.InFlash);
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			CameraManager.Instance.nowDir = Const.EnumCameraDir.Right;
			this.tweenActionMove(this.getCoordToMove(Const.EnumCameraDir.Right), Ease.InFlash);
			this.tweenActionRotate(this.getAngle(Const.EnumCameraDir.Right), Ease.InFlash);
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			CameraManager.Instance.nowDir = Const.EnumCameraDir.Left;
			this.tweenActionMove(this.getCoordToMove(Const.EnumCameraDir.Left), Ease.InFlash);
			this.tweenActionRotate(this.getAngle(Const.EnumCameraDir.Left), Ease.InFlash);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			CameraManager.Instance.nowDir = Const.EnumCameraDir.Opposition;
			this.tweenActionMove(this.getCoordToMove(Const.EnumCameraDir.Opposition), Ease.InFlash);
			this.tweenActionRotate(this.getAngle(Const.EnumCameraDir.Opposition), Ease.InFlash);
		}
	}

	//スクリプト操作によるカメラワーク
	public void scriptWorking(Const.EnumCameraDir selectDir){
		CameraManager.Instance.nowDir = selectDir;
		this.tweenActionMove(this.getCoordToMove(selectDir), Ease.InFlash);
		this.tweenActionRotate(this.getAngle(selectDir), Ease.InFlash);
	}

	//普段は、カメラはプレイヤーに追従する
	public void followMove(){
		transform.position = this.getCoordToMove(CameraManager.Instance.nowDir);
	}

	//DoTweenによるカメラワーク（動き）
	public void tweenActionMove(Vector3 coord, Ease easeType){
		this.freezePlayer(true);
		this.readyToWork = false;
		transform.DOMove(coord, this.howLongToArrive)
			//.SetEase(easeType)
			.OnComplete(() => {this.readyToWork = true; this.freezePlayer(false);});
			//.OnComplete(() => this.freezePlayer(false));
	}

	//DoTweenによるカメラワーク（回転）
	public void tweenActionRotate(Vector3 rotate, Ease easeType){
		transform.DORotate(rotate, this.howLongToArrive);
	}

	//カメラが動く方向を取得
	private Vector3 getCoordToMove(Const.EnumCameraDir dir){
		switch(dir){
			default:
				return this.player.transform.position 
					+ new Vector3(0.0f, 5.0f, -8.0f);

			case Const.EnumCameraDir.Right:
				return this.player.transform.position 
					+ new Vector3(8.0f, 5.0f, 0f);

			case Const.EnumCameraDir.Left:
				return this.player.transform.position 
					+ new Vector3(-8.0f, 5.0f, 0f);

			case Const.EnumCameraDir.Opposition:
				return this.player.transform.position 
					+ new Vector3(0f, 5.0f, 8.0f);
		}
	}

	//カメラアングルを取得
	private Vector3 getAngle(Const.EnumCameraDir dir){
		switch(dir){
			default:
				return new Vector3(27.0f, 0f, 0f);

			case Const.EnumCameraDir.Right:
				return new Vector3(27.0f, 270f, 0f);

			case Const.EnumCameraDir.Left:
				return new Vector3(27.0f, 90f, 0f);

			case Const.EnumCameraDir.Opposition:
				return new Vector3(27.0f, 180f, 0f);
		}
	}

	//プレイヤーをフリーズまたはフリーズ解除させる（notの値）
	private void freezePlayer(bool not){
		var plaCsMain = this.player.GetComponent<UnityChanStatus>();

		if(plaCsMain != null){
			plaCsMain.isFreezing = not;
		}
	}
}
