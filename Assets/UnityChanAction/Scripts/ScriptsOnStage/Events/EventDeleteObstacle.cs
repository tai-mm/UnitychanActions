using UnityEngine;
using System.Collections;

public class EventDeleteObstacle : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider){
		if(collider.gameObject.tag == "Obstacle"){
			//print("test");
			Destroy(collider.gameObject);
		}
	}
}
