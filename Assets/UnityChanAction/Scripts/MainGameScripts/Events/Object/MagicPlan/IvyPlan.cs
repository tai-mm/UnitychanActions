using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IvyPlan : MonoBehaviour {
	protected GameObject prefabPlayer;

	void OnTriggerEnter(Collider collider){
		var colObj = collider.gameObject;
		if(colObj.tag == "Player"){
			this.prefabPlayer = colObj;
			this.generateIvy();
		}
	}

	public void generateIvy(){
		print("Generating");
	}
}
