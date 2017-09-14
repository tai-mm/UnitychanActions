using UnityEngine;
using System.Collections;

public class EventKick : MonoBehaviour {
	public bool isKick;
	public GameObject WoodParticle;
	protected float attackDamage = 4.0F;
	protected GameObject unitychan;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K)){
			this.isKick = true;
		}
	}

	void OnTriggerEnter (Collider coll){
		if(this.isKick){
			if(!(coll.gameObject.name != "WoodCrate")){
				GameObject wp = Instantiate (WoodParticle, 
					coll.gameObject.transform.position, 
					Quaternion.identity) as GameObject;
				Destroy(coll.gameObject);
				this.isKick = false;
			}else if(coll.gameObject.tag == "EnemyEntity"){
				coll.gameObject.SendMessage("receiveAttack", this.attackDamage);
				this.isKick = false;
			}
		}
	}
}
