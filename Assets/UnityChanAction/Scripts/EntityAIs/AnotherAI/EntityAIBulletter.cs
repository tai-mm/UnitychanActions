using UnityEngine;
using System.Collections;

public class EntityAIBulletter : MonoBehaviour {
	public GameObject bullet;
	public int minInter;
	public int maxInter;
	public float bulletSpeed = 3.0F;
	GameObject bulletClone;
	protected int shotInterval;

	void Start () {
		this.shotInterval = Random.Range(100, 200);
	}
	
	void Update () {
		if(this.shotInterval <= 0){
			this.shotToBullet();
			this.shotInterval = Random.Range(this.minInter, this.maxInter);
		}else{
			this.shotInterval -= 1;
		}
	}

	private void shotToBullet(){
		bulletClone = GameObject.Instantiate(bullet, transform.position,
		 transform.rotation) as GameObject;
	}
}
