using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanStatus : MonoBehaviour {
	public Text hpLabel;	//メインキャンバスのHPラベル
	public Image hpGauge;	//メインキャンバスのHPゲージ
	public Vector3 startPoint;	//初期位置
	public int damageDown = 0;	//ダメージを受けたときの無敵時間
	public float attackDamage = 10.0F;	//攻撃力
	public bool isFreezing = false;		//フリーズ中は、プレイヤーは動けない
	public bool canUseAllowCheats = false;	//チート機能の使用ができるか
	protected float baseHp;
	protected float health = 10.0F;	//プレイヤーのHP
	protected bool isInvisiblity = false;//無敵状態

	//プレイヤーに付随する他のスクリプト
	private UnityChanMoves plaCsMove;
	private UnityChanRay plaCsRay;
	private UnityChanUI plaCsUI;

	void Awake(){
		WorldManager.Instance.nowWorldOn = Const.EnumWorldSituation.Usually;
		this.startPoint = this.transform.position;
		this.plaCsMove = GetComponent<UnityChanMoves>();
	}

	void Start(){
		this.baseHp = this.health;
		this.updateLabelValue(this.getHealth(), this.baseHp);
	}

	void Update(){
		if(!this.isFreezing && Time.timeScale != 0){

			//HPが0ならゲームオーバー
			if(this.getHealth() <= 0.0F){
				this.plaCsMove.StartCoroutine(this.plaCsMove.setDead());
				
			}else{
				this.moveCheats();
			}

			//ダメージダウンからの復位
			if(this.damageDown > 0){
				--this.damageDown;
			}
		}

		//HPゲージの変化は、フリーズ中や時間停止中も行われる。
		this.updateLabelValue(this.getHealth(), this.baseHp);

		if(this.health > 10.0F){
			this.setHealth(10.0F);
		}
	}

	//ダメージを受けた時
	public void attackedBy(float damage){
		if(!this.isInvisiblity){
			this.health -= damage;
			this.damageDown = 10;
		}
	}

	//HPラベルの変化
	private void updateLabelValue(float hp, float full){
		hpLabel.text = hp.ToString("f0") + "/" + full.ToString("f0");
		this.updateGaugeValue(hp);
	}

	//HPゲージの変化
	private void updateGaugeValue(float hp){
		hpGauge.fillAmount = hp / 10.0F;
	}

	//HPを設定
	public void setHealth(float healthValue){
		this.health = healthValue;
	}

	//HPを回復
	public void healing(float amount){
		this.health += amount;
	}

	//HPがいくつあるかを取得
	public float getHealth(){
		return this.health;
	}

	//Unityちゃんの状態
	public string getEntityState(){
		return this.getHealth() < 7.0F ? "Weakness" : null; //: (this.target != null ? "Attack" : "Idle");
	}

	//チートコマンドの管理
	private void moveCheats(){
		//無敵状態になる
		if(this.canUseAllowCheats){
			if(Input.GetKeyUp(KeyCode.I) && !this.isInvisiblity){
				this.isInvisiblity = true;
			}
		}

		//スタート地点に戻る
		if(Input.GetKeyDown(KeyCode.R)){
			transform.position = this.startPoint;
		}
	}
}
