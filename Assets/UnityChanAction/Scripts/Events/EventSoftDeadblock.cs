using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoftDeadblock : MonoBehaviour {
	public GameObject startPoint;
	public int damageWhenCol = 3;

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Player"){
			collision.gameObject.SendMessage
				("receiveAttack", this.damageWhenCol);

			collision.gameObject.transform.position = 
				startPoint.transform.position;
		}
	}
}
