using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQuest : MonoBehaviour {
	public GameObject questLabel;
	public GameObject goal;
	public bool openedQLabel = false;
	protected GameObject obj;
	private UnityChanMoves uniCs;
	
	void Start () {
		this.questLabel.SetActive(false);
		this.goal.SetActive(false);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.O) && this.uniCs){

			if(this.uniCs.getKeys() >= 3){
				this.uniCs.isFreezing = true;
				this.goal.SetActive(true);

			}else{
				
				if(this.openedQLabel){
					this.questLabel.SetActive(false);
					this.uniCs.isFreezing = false;
					this.openedQLabel = false;

				}else{
					this.questLabel.SetActive(true);
					this.uniCs.isFreezing = true;
					this.openedQLabel = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider collider){
		this.obj = collider.gameObject;
		if(this.obj.tag != "Player")
			return;
			
		this.uniCs = null;
		this.questLabel.SetActive(false);
		this.openedQLabel = false;

	}

	void OnTriggerEnter(Collider collider){
		this.obj = collider.gameObject;
		if(this.obj.tag == "Player"){
			this.uniCs = this.obj.GetComponent<UnityChanMoves>();
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
	}
}
