using UnityEngine;
using System.Collections;

public class EntityAIEnemyForward : MonoBehaviour {
	public float moveSpeed = 4.0F;
	protected double getDistance;
	protected Transform player;
	protected Vector3 randomPlace;

	void Start () {
		this.player = GameObject.Find("unitychan").transform;
		this.randomPlace = this.getIdleRandomPlace();
	}
	
	void Update () {
		/*this.getDistanceToPlayer = Vector3.Distance(transform.position, player.position);
		if((float)this.getDistanceToPlayer > 30.0F) {
			this.moveForPlayer;
		}else{*/
			float getDistanceSq = Vector3.SqrMagnitude(transform.position - this.randomPlace);
			if(getDistanceSq < 4.0F){
				randomPlace = this.getIdleRandomPlace();
			}else{
				Quaternion targetRotation = Quaternion.LookRotation(this.randomPlace
				 - transform.position);

        		transform.rotation = Quaternion.Slerp(transform.rotation, this.randomPlace,
        		 Time.deltaTime * 1.0F);

        		transform.Translate(Vector3.forward * this.moveSpeed * Time.deltaTime);
			//}
		}
	}

	public void moveForPlayer(){
	
	}

	private Vector3 getIdleRandomPlace(){
		return new Vector3(Random.Range(-50.0F, 50.0F), 0, Random.Range(-50.0F, 50.0F));
	}
}
