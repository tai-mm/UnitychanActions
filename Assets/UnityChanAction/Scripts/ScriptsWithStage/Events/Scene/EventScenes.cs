using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventScenes : MonoBehaviour {
	public Animator sceneAto;
	public GameObject ground;
	public GameObject unityChan;
	public int changeTo = 1;
	public bool calledComing = false;//"comingFade"が既に呼ばれたか
	public bool calledNext = false;//"goToNextRoom"が既に呼ばれたか
	private EntityAIUnityChanMoves uniScript;
	private Image image;
	private float downInbisible = 0;
	private float fadeTime = 0;

	void Start () {
		this.image = this.GetComponent<Image>();
		this.uniScript = this.unityChan.GetComponent<EntityAIUnityChanMoves> ();
	}

	void Update () {
		if(Time.timeScale != 0){
			this.unityChanDamaged();
		}
	}

	//Unityちゃんがダメージを受けた時のシーン
	public void unityChanDamaged(){
		this.downInbisible = this.uniScript.damageDown * 0.07F;
		if(this.uniScript.damageDown > 5){
			this.image.color = new Color(255, 0, 0, 0.3F);

		}else if(this.uniScript.damageDown > 0){
			this.image.color = new Color(255, 0, 0, this.downInbisible);

		}else{
			this.image.color = new Color(255, 0, 0, 0);
			
		}
	}

	//画面の暗転
	public void goingFade(){
		this.sceneAto.SetBool("Black", true);
		this.sceneAto.SetFloat("AnimationSpeed", 1.0F);
	}

	//画面の暗転終了(アニメーションイベントから呼ばれる)
	public void comingFade(){
		if(!this.calledComing){
			this.calledComing = true;
			this.sceneAto.SetFloat("AnimationSpeed", 0.0F);//フェードアニメーションを一時停止
			StartCoroutine(goToNextRoom());
		}
	}

	//扉の向こうの部屋に移る
	IEnumerator goToNextRoom(){
		var changeCs = this.ground.GetComponent<EventIntoARoom>();
		changeCs.changeFor(this.changeTo);//"EventIntoARoom"の"changeFor"を呼ぶ
		yield return new WaitForSeconds(0.2F);

		//フェードアニメーション再開
		this.sceneAto.SetFloat("AnimationSpeed", -1.0F);
	}

	public void endTheFade(){
		if(this.calledNext){
			this.sceneAto.SetBool("Black", false);
		}else{
			this.calledNext = true;
		}
	}
}
