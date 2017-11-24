using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAIItem : MonoBehaviour {
	public string itemType;
	public bool isRoolling;
	private EntityAIUnityChan uniScript;
	private int time;
	private float moveSpeed = 0.5F;
	private bool isReturn;

	void Awake () {
		this.time = 0;
		this.isReturn = false;
	}

	void Update () {
		if(Time.timeScale != 0 && this.isRoolling){
			this.transform.eulerAngles += new Vector3(0.0F, 5.0F, 0.0F);
		}
	}

	void OnCollisionEnter (Collision collision){
		if(collision.gameObject.tag == "Player"){
			Destroy(this.gameObject);
		}
	}

	public string getItemType(){
		switch(this.itemType){
			case "EssencialKey":
			return "EssencialKey";
			break;

			case "HealPotion":
			return "HealPotion";
			break;

			default:
			return null;
			break;
		}
	}

	/*if(!this.isReturn && this.time < 20){
		this.time++;
		transform.position += new Vector3(0, this.moveSpeed * 
			Time.deltaTime, 0);
		if(this.time >= 20){
			this.isReturn = true;
		}
	}
	if(this.isReturn && this.time > 0){
		this.time--;
		transform.position += new Vector3(0, -this.moveSpeed * 
			Time.deltaTime, 0);
		if(this.time <= 0){
 			this.isReturn = false;
		}
	}*/
}
