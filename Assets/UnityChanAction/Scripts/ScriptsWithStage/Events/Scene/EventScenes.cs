using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventScenes : MonoBehaviour {
	public Animator sceneAto;
	public Image image;
	public GameObject ground;
	public GameObject unityChan;
	public int changeTo = 1;
	public bool calledComing = false;//"comingFade"が既に呼ばれたか
	public bool calledNext = false;//"goToNextRoom"が既に呼ばれたか
	private EntityAIUnityChanMoves uniScript;
	private float downInbisible = 0;
	private float fadeTime = 0;

	void Start () {
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
}
