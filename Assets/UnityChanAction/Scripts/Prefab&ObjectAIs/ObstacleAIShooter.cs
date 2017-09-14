using UnityEngine;
using System.Collections;

public class ObstacleAIShooter : MonoBehaviour {
	public GameObject player;
	public GameObject throwable;
	public float followRange = 24.0F;
	public int maxInterval = 200;
	protected Vector3 playerPos;
	protected int shootInterval;
	protected float getDisPlayer;

	void Start () {
		this.shootInterval = 0;
	}
	
	void Update () {
		this.getDetailOfPlayer();
		if(this.getDisPlayer < this.followRange && 
			this.shootInterval <= 0){
			this.shootTheBall();
		}else{
			--this.shootInterval;
		}
	}

	private void getDetailOfPlayer(){
		this.playerPos = this.player.transform.position;
		this.getDisPlayer = Vector3.Distance(transform.position, this.playerPos);
	}

	private void shootTheBall(){
		GameObject shoot = GameObject.Instantiate(throwable, transform.position,
		 transform.rotation) as GameObject;

		Quaternion targetRotation = Quaternion.LookRotation(this.playerPos
			- shoot.transform.position);

		shoot.transform.LookAt(this.playerPos);

		this.shootInterval = this.maxInterval;
	}
}
