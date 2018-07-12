using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventScenes : MonoBehaviour {
	public Image image;
	public GameObject player;
	private UnityChanStatus plaCsMain;
	private float downInbisible = 0;

	void Start () {
		this.plaCsMain = this.player.GetComponent<UnityChanStatus>();
	}

	void Update () {
		if(Time.timeScale != 0){
			this.unityChanDamaged();
		}
	}

	//Unityちゃんがダメージを受けた時のシーン
	public void unityChanDamaged(){
		int timeDamaged = plaCsMain.damageDown;
		this.downInbisible = timeDamaged * 0.07F;

		if(timeDamaged > 5){
			this.image.color = new Color(255, 0, 0, 0.3F);

		}else if(timeDamaged > 0){
			this.image.color = new Color(255, 0, 0, this.downInbisible);

		}else{
			this.image.color = new Color(255, 0, 0, 0);
			
		}
	}
}
