using UnityEngine;
using System.Collections;

public class EventOverground : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider){
		if(collider.gameObject.tag == "Player"){
			collider.gameObject.SendMessage("setDead");

		}else if(collider.gameObject.tag == "Attackness"){
			collider.gameObject.SendMessage("summonEffect");
			Destroy(this.gameObject);
		}
	}
}
