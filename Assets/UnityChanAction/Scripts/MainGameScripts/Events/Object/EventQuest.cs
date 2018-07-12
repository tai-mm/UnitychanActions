using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQuest : MonoBehaviour {
	public GameObject questLabel;
	public GameObject goal;
	public bool openedQLabel = false;
	protected GameObject obj;
	private GameObject player;
	private UnityChanStatus uniCsMain;
	private UnityChanMoves uniCsMove;
	
	void Start () {
		this.questLabel.SetActive(false);
		this.goal.SetActive(false);
	}

	/*void Update(){
		if(Input.GetKeyDown(KeyCode.O) && uniCsMain && uniCsMove){

			if(this.uniCsMove.getKeys() >= 3){
				this.uniCsMain.isFreezing = true;
				this.goal.SetActive(true);

			}else{
				
				if(this.openedQLabel){
					this.questLabel.SetActive(false);
					this.uniCsMain.isFreezing = false;
					this.openedQLabel = false;

				}else{
					this.questLabel.SetActive(true);
					this.uniCsMain.isFreezing = true;
					this.openedQLabel = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider collider){
		this.obj = collider.gameObject;
		if(this.obj.tag != "Player")
			return;

		this.uniCsMain = null;
		this.uniCsMove = null;
		this.questLabel.SetActive(false);
		this.openedQLabel = false;

	}

	void OnTriggerEnter(Collider collider){
		this.obj = collider.gameObject;
		if(this.obj.tag == "Player"){
			this.uniCsMain = this.player.GetComponent<UnityChanStatus>();
			this.uniCsMove = this.player.GetComponent<UnityChanMoves>();
		}
		/*
		if(obj.tag == "Player"){
			if(Input.GetKeyDown(KeyCode.O) && !this.flg){
				this.flg = !this.flg;

				var uniCs = obj.GetComponent<EntityAIUnityChan>();
				if(uniCs.getKeys() >= 3){
					uniCs.isFreezing = true;
					this.goal.SetActive(true);

				}else{
					
					if(this.openedQLabel){
						this.questLabel.SetActive(false);
						this.openedQLabel = false;
				this.flg = !this.flg;

					}else{
						this.questLabel.SetActive(true);
				this.flg = !this.flg;
						this.openedQLabel = true;
					}
				}
			}else{
				this.flg = !flg;
			}
		}
		//*/
	//}
}
