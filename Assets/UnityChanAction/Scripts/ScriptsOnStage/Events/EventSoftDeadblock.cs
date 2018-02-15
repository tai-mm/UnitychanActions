using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoftDeadblock : MonoBehaviour {
	public int damageWhenCol = 3;

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Player"){
			collision.gameObject.SendMessage
				("attackedBy", this.damageWhenCol);
			var uniCs = collision.gameObject.
				GetComponent<UnityChanMoves>();

			if(uniCs != null && uniCs.getHealth() <= 0.0F){
				collision.gameObject.SendMessage("setDead");
			}else{
				collision.gameObject.transform.position = 
				uniCs.startPoint;
			}
		}
	}
}
