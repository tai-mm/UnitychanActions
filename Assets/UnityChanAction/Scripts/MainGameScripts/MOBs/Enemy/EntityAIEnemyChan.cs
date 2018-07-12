using UnityEngine;
using System.Collections;

public class EntityAIEnemyChan : MonoBehaviour{
	public float moveSpeed = 1.0F;
	public float moveSpeedAttack = 2.0F;
	public float followRange = 10.0F;
	public float attackDamage = 1.0F;
	public float getDisSq;
	public int stareTime;
	public GameObject target;
	protected float health = 10.0F;
	protected int knockbackTime = 0;
	protected int attackInterval;
	protected int receiveTime;
	protected byte entityEffects;
	protected GameObject player;
	protected Vector3 playerPos;
	protected Vector3 randomPlace;

	void Start () {
		gameObject.tag = "EnemyEntity";
		this.player = GameObject.Find("unitychan");
		this.randomPlace = this.getIdleRandomPlace();
		this.stareTime = (int)Random.Range(30.0F, 200.0F);
		this.attackInterval = 30;
		this.receiveTime = 0;
		this.entityEffects = 0;
	}
	
	void Update () {
		if(this.health <= 0.0F){
			this.dead();
		}else{
			this.entityUpd();
		}

		if(this.knockbackTime > 0){
			--this.knockbackTime;
			transform.position -= transform.forward * 1.0F;
		}

		if(this.receiveTime > 0){
			GetComponent<Renderer>().material.color = Color.red;
			this.receiveTime -= 1;
		}else{
			GetComponent<Renderer>().material.color = Color.yellow;
		}

		this.playerPos = this.player.transform.position;
		float getDisPlayer = Vector3.Distance(transform.position, playerPos);
		if(getDisPlayer < this.followRange && getDisPlayer > 2.0F) {
			this.targetRange();
			this.target = player;
		}else if(getDisPlayer <= 2.0F){
			this.attackToTarget(player);
		}else{
			this.idleMove();
			this.target = null;
		}
	}

	public virtual void entityUpd(){

	}

	public void idleMove(){
		this.getDisSq = Vector3.Distance(this.randomPlace, transform.position);
		if(this.getDisSq < 4.0F){
			this.idleStare();
		}else{
			Quaternion targetRotation = Quaternion.LookRotation(this.randomPlace
			- transform.position);

        	transform.LookAt(this.randomPlace);

        	transform.Translate(Vector3.forward * this.moveSpeed * Time.deltaTime);
        }
	}

	public void idleStare(){
		if(this.stareTime > 0){
			--this.stareTime;
		}else if(this.stareTime <= 0){
			this.stareTime = (int)Random.Range(30.0F, 200.0F);
			this.randomPlace = this.getIdleRandomPlace();
		}
	}

	public void targetRange(){
		if(this.target != null){
			Quaternion targetRotation = Quaternion.LookRotation(this.playerPos
			- transform.position);

        	transform.LookAt(this.playerPos);

        	transform.Translate(Vector3.forward * this.moveSpeedAttack * Time.deltaTime);
    	}
	}

	void OnCollisionEnter(Collision coll){
		if(coll.gameObject == this.target){
			this.attackToTarget(coll.gameObject);
		}
	}

	public void attackToTarget(GameObject attackObj){
		if(this.attackInterval >= 30){
			attackObj.SendMessage("receiveAttack", this.attackDamage);
			this.attackInterval = 0;
		}else{
			++this.attackInterval;
		}
	}

	private Vector3 getIdleRandomPlace(){
		return new Vector3(Random.Range(this.transform.position.x - 15.0F,
		this.transform.position.x + 15.0F),
		transform.position.y,
		Random.Range(this.transform.position.z - 15.0F,
		this.transform.position.z + 15.0F));
	}

	public void receiveAttack(float damage){
		this.health -= damage;
		this.knockbackTime = 3;
		this.receiveTime = 5;
	}

	public void setHealth(float healthValue){
		this.health = healthValue;
	}

	public void heal(int amount){
		this.health += amount;
	}

	public void dead(){
		Destroy(this.gameObject);
	}

	public float getHealth(){
		return this.health;
	}

	public string getEntityState(){
		return this.health < 2.0F ? "Weakness" : (this.target != null ? "Attack" : "Idle");
	}

	public void setKnockback(int amount){
		this.knockbackTime = amount;
	}

	static string entityType(){
		return "NormalEnemy";
	}
}
