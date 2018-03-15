using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButtonHelper : MonoBehaviour {
	public GameObject helper;
	public GameObject boxItsParent;

	void Start (){
		this.helper.SetActive(false);
	}

	void OnTriggerStay(Collider collider){
		var obj = collider.gameObject;

		if(this.boxItsParent != null){
			if(!this.boxItsParent.GetComponent<EventTreasureBox>().getState()){
				this.helper.SetActive(true);
			}else{
				this.helper.SetActive(false);
			}
		}else if(obj.tag == "Player"){
			this.helper.SetActive(true);
		}
	}

	void OnTriggerExit(Collider collider){
		var obj = collider.gameObject;

		if(obj.tag == "Player"){
			this.helper.SetActive(false);
		}
	}
}
