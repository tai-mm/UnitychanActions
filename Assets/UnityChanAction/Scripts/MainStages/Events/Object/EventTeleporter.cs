using UnityEngine;
using System.Collections;

public class EventTeleporter : MonoBehaviour {
	public GameObject teleporter;
	public string intoAHere;
	public bool isChangeable;
	private Vector3 sendTo;

	void Start () {
		if(this.teleporter != null){
			this.sendTo = 
				this.teleporter.transform.position;
		}
	}

	void OnTriggerEnter (Collider collider){

		string tag = collider.gameObject.tag;
		if(tag == "Player"){
			GameObject player = collider.gameObject;

			player.transform.position = this.sendTo;
		}
	}
}
