using UnityEngine;
using System.Collections;

public class PrefabAIGlassShard : PrefabAIBase {
	public int lifeTime = 7;
	public float damage = 2.0F;
	public float moveSpeed = 5.0F;
	private GameObject player;
	private Quaternion rotation;
	private Vector3 playerPos;
	private int count;

	void Awake (){
		this.player = GameObject.Find("unitychan");
	}

	void Start () {
		Invoke("setDead", this.lifeTime);
		this.setObjective();
	}

	void Update () {
		transform.Translate(Vector3.forward * this.moveSpeed * Time.deltaTime);
		
		if(++this.count % 1 == 0){
			this.setObjective();
		}
	}

	void OnCollisionEnter(Collision coll){
		if(coll.gameObject.tag == "Player"){
			coll.gameObject.SendMessage("attackedBy", this.damage);
			this.setDead();
		}
	}

	private void lookAtThePlayer() {
		Quaternion targetRotation = Quaternion.LookRotation(this.playerPos
			- transform.position);

		transform.LookAt(this.playerPos);
	}

	private void setObjective() {
		this.playerPos = player.transform.position + new Vector3(1.0F, 0F, 0F);
		this.lookAtThePlayer();
	}
}
