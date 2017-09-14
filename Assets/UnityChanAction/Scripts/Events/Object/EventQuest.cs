using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQuest : MonoBehaviour {
	public GameObject questLabel;
	public GameObject goal;
	public bool openedQLabel = false;
	private bool flg = false;
	
	void Start () {
		this.questLabel.SetActive(false);
		this.goal.SetActive(false);
	}

	void OnTriggerStay(Collider collider){
		GameObject obj = collider.gameObject;
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
	}
}
