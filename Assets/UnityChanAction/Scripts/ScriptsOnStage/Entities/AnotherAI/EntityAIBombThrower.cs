using UnityEngine;
using System.Collections;

public class EntityAIBombThrower : MonoBehaviour {
	public GameObject prefab_bomb;
	private UnityChanMoves uniScript;
	private int interval = 0;

	void Awake (){
		this.uniScript = GetComponent<UnityChanMoves> ();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.M) && this.interval <= 0 &&
			this.uniScript.getEquipment() == "Bomb"){
			
			Vector3 pos = transform.position + new Vector3(0f, 1.5f, 0f) 
				+ transform.TransformDirection(Vector3.forward);

			GameObject bomb = GameObject.Instantiate(prefab_bomb
				, pos, transform.rotation) as GameObject;

			Vector3 speed = transform.TransformDirection
				(Vector3.forward) * 5;
			speed += Vector3.up * 5;

			bomb.GetComponent<Rigidbody> ().velocity = speed;

			this.interval = 15;
		}

		if(this.interval > 0){
			--this.interval;
		}
	}
}
