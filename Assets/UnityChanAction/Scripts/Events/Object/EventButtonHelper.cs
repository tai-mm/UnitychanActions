using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventButtonHelper : MonoBehaviour {
	public GameObject helper;

	void Start (){
		this.helper.SetActive(false);
	}

	void OnTriggerStay(Collider collider){
		var obj = collider.gameObject;

		if(obj.tag == "Player"){
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
