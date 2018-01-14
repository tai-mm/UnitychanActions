using UnityEngine;
using System.Collections;

public class EventTreasureBox : MonoBehaviour {
	public Animator animator;
	public GameObject unityChan;
	public GameObject contentItem;
	public GameObject particle;
	protected bool playerInNear;
	private Vector3 playerPos;
	private Vector3 boxPos;
	private bool isOpened;

	void Start () {
		this.playerInNear = false;
		this.boxPos = transform.position;
		this.isOpened = false;
	}
	
	void Update () {
		this.getOpeness();

		if(this.playerInNear){

			if(Input.GetKeyDown(KeyCode.O)){

				//isOpenedがfalseなら、宝箱を開く
				if(!this.isOpened){

					this.isOpened = true;
					animator.SetBool("Open", true);
					
					if(this.contentItem != null){
						StartCoroutine(spawnItem());
					}
				}/*else{
					animator.SetBool("Open", false);
					this.isOpened = false;
				}*/

			}
		}
	}

	private void getOpeness(){
		this.playerPos = this.unityChan.transform.position;
		//プレイヤーと宝箱との距離
		float getDistance = Vector3.Distance
			(transform.position, playerPos);

		if(getDistance < 1.0F){

			this.playerInNear = true;
		}else{
			this.playerInNear = false;
		}
	}

	IEnumerator spawnItem(){
		yield return new WaitForSeconds(0.8F);

		var spawness = GameObject.Instantiate(this.contentItem, 
			new Vector3(12.0f, this.boxPos.y + 1.0f, 1.0f), transform.rotation) as GameObject;
		spawness.GetComponent<EventReleaseItem>().treasureBox = this.gameObject;
		spawness.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		spawness.transform.SetParent(this.transform, true);

		//パーティクルのサイズを0に
		ParticleSystem system = this.particle.GetComponent<ParticleSystem>();
		system.startSize = 0;

		//アイテムが飛び出たら、パーティクルを消去
		yield return new WaitForSeconds(1.0F);
		Destroy(this.particle.gameObject);
	}

	public bool getState(){
		return this.isOpened;
	}
}
