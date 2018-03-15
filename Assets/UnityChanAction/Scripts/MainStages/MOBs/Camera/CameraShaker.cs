using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {
	public GameObject player;
	private UnityChanStatus plaCsMain;
	private int shakingTime = 0;

	void Start (){
		this.plaCsMain = this.player.GetComponent<UnityChanStatus> ();
	}

	void Update (){
		if(Time.timeScale != 0){
			this.shaking();
		}
	}

	private void shaking(){
		if(this.plaCsMain.damageDown > 0){
			this.shakingTime = this.plaCsMain.damageDown;
			if(this.shakingTime > 7 || 
				this.shakingTime <= 4 && this.shakingTime > 1 || 
				this.shakingTime <= 0){
				transform.position += new Vector3(0.1F, 0F, 0F);
			}else if(this.shakingTime <= 7 && this.shakingTime > 4 || 
				this.shakingTime <= 1 && this.shakingTime > 0){
				transform.position += new Vector3(-0.2F, 0F, 0F);
			}
		}
		if(this.plaCsMain.damageDown <= 0){
			this.transform.position = new Vector3(
				this.player.transform.position.x,
				this.player.transform.position.y + 1.9F,
				this.player.transform.position.z - 2.1F);
			this.transform.rotation = Quaternion.Slerp(
				transform.rotation, Quaternion.Euler(10.0F, 0.0F, 0.0F), 3.0F
				);
		}
	}
}
