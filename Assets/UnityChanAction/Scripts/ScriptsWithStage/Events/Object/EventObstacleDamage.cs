using UnityEngine;
using System.Collections;

public class EventObstacleDamage : MonoBehaviour {
	public float collDamage = 1.0F;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider){
		if(collider.gameObject.tag == "Player"){
			collider.gameObject.SendMessage("attackedBy", this.collDamage);

		}else if(collider.gameObject.tag == "Attackness"){
			collider.gameObject.SendMessage("summonEffect");
			Destroy(this.gameObject);
		}
	}
}
