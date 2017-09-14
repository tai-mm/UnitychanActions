using UnityEngine;
using System.Collections;

public class PrefabAIBullet : MonoBehaviour {
	public int lifeTime = 10;
	public float damage = 3.0F;
	public GameObject bulletParticle;
	protected int noneDamageTime = 60;
	protected float moveSpeed = 5.0F;

	void Start () {
		Invoke("setDead", this.lifeTime);

		/*GameObject bpClone = GameObject.Instantiate(bulletParticle, transform.position,
		 transform.rotation) as GameObject;

		bpClone.transform.SetParent(this.transform);*/
	}
	
	void Update () {
		--this.noneDamageTime;

		transform.position += transform.forward * Time.deltaTime * moveSpeed;
		
		this.entityUpdate();
	}

	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag == "Player"){
			coll.gameObject.SendMessage("receiveAttack", this.damage);
			this.setDead();
		}

		if(this.noneDamageTime < 0){
			this.setDead();
		}
	}

	private void entityUpdate(){

	}

	public void setDead(){
		Destroy(this.gameObject);
	}
}
