using UnityEngine;
using System.Collections;

public class EventObsOnNear : MonoBehaviour {
	public GameObject circle;
	private float distance;

	void Start () {
		this.circle.SetActive(false);
	}

	void OnTriggerStay(Collider collider){
		if(collider.gameObject.tag == "Obstacle"){

			if(!this.circle.activeSelf){
				//this.circle.SetActive(true);
			}
		}
	}
}
