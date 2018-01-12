using UnityEngine;
using System.Collections;

public class EventDoor : MonoBehaviour {
	public GameObject unityChan;
	public GameObject scene;
	protected bool playerInNear;
	private Vector3 playerPos;
	private EntityAIUnityChanMoves uniCs;

	void Start () {
		this.playerInNear = false;
		this.uniCs = this.unityChan.GetComponent<EntityAIUnityChanMoves>();
	}

	void Update () {
		this.getOpeness();

		if(this.playerInNear && this.uniCs.isIn == "Outside"){
			if(Input.GetKeyDown(KeyCode.O)){
				this.uniCs.isIn = "Room";
				this.intoARoom(1);
			}
		}
	}

	private void getOpeness(){
		this.playerPos = this.unityChan.transform.position;
		//プレイヤーとドアとの距離
		float getDistance = Vector3.Distance
			(transform.position, playerPos);

		if(getDistance < 3.0F){

			this.playerInNear = true;
		}else{
			this.playerInNear = false;
		}
	}

	public void intoARoom(int change){
		//Unityちゃんをフリーズ
		this.uniCs.isFreezing = true;

		//フェードアウト開始
		var sceneCs = this.scene.GetComponent<EventScenes>();
		sceneCs.calledComing = false;
		sceneCs.calledNext = false;
		sceneCs.changeTo = change;
		sceneCs.goingFade();
	}
}