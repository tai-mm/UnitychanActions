using UnityEngine;
using System.Collections;

public class EventCameraShake : MonoBehaviour {
	private GameObject unityChan;
	private EntityAIUnityChan uniScript;
	private int shakingTime = 0;

	void Start (){
		this.unityChan = GameObject.Find("unitychan");
		this.uniScript = this.unityChan.GetComponent<EntityAIUnityChan> ();
	}

	void Update (){
		if(Time.timeScale != 0){
			this.shaking();
		}
	}

	private void shaking(){
		if(this.uniScript.damageDown > 0){
			this.shakingTime = this.uniScript.damageDown;
			if(this.shakingTime > 7 || 
				this.shakingTime <= 4 && this.shakingTime > 1 || 
				this.shakingTime <= 0){
				transform.position += new Vector3(0.1F, 0F, 0F);
			}else if(this.shakingTime <= 7 && this.shakingTime > 4 || 
				this.shakingTime <= 1 && this.shakingTime > 0){
				transform.position += new Vector3(-0.2F, 0F, 0F);
			}
		}
		if(this.uniScript.damageDown <= 0){
			this.transform.position = new Vector3(
				this.unityChan.transform.position.x,
				this.unityChan.transform.position.y + 1.9F,
				this.unityChan.transform.position.z - 2.1F);
			this.transform.rotation = Quaternion.Slerp(
				transform.rotation, Quaternion.Euler(10.0F, 0.0F, 0.0F), 3.0F
				);
		}
	}
}
