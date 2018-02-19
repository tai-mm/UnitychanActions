﻿using UnityEngine;
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
	
	void Update () {
		if(!isFreezing){
			this.inputWorking();

			if(Input.GetKeyDown(KeyCode.Z)){
				StartCoroutine(zoomForward());
			}
		}
	}

	//キーボード操作によるカメラワーク
	public void inputWorking(){
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			this.direction = Const.EnumCameraDir.Usually;
		}
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			this.direction = Const.EnumCameraDir.Right;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			this.direction = Const.EnumCameraDir.Left;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			this.direction = Const.EnumCameraDir.Opposition;
		}
		this.action(this.getCoordToMove(), Ease.InFlash);
	}

	//スクリプト操作によるカメラワーク
	public void caughtWorking(Const.EnumCameraDir selectDir){
		this.direction = selectDir;
		this.action(this.getCoordToMove(), Ease.InFlash);
	}

	public void action(Vector3 coord, Ease easeType){
		this.readyToWork = false;
		transform.DOMove(coord, this.howLongToArrive)
			/*.SetEase(easeType)*/.OnComplete(() => this.readyToWork = true);
	}

	//カメラが動く方向を取得
	public Vector3 getCoordToMove(){
		switch(this.direction){
			default:
				return new Vector3(0.0f, 4.0f, -7.0f);

			case Right:
				return new Vector3(-5.0f, 4.0f, 0f);

			case Left:
				return new Vector3(5.0f, 4.0f, 0f);

			case Opposition:
				return new Vector3(0f, 4.0f, 5.0f);
		}
	}

	//カメラアングルを取得
	public Vector3 getAngle(){
		switch(this.direction){
			default:
				return new Vector3(24.0f, 0f, 0f);

			case Right:
				return new Vector3(27.0f, 90f, 0f);

			case Left:
				return new Vector3(27.0f, 270f, 0f);

			case Opposition:
				return new Vector3(27.0f, 180f, 0f);
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
