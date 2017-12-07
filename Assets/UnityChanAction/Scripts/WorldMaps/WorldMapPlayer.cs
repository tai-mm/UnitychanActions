using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class WorldMapPlayer : MonoBehaviour {
	public Animator animator;
	public Rigidbody rigidBody;
	public GameObject canvas;
	public GameObject currentObj;
	public float howLongToArrive;
	public bool readyToMove = true;
	public bool isFreezing = false;

	GameObject eastStage;
	GameObject westStage;
	//GameObject northStage;
	//GameObject southStage;

	void Start () {
		eastStage = this.currentObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.east];
		westStage = this.currentObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.west];
	}

	void Update () {
		if(this.readyToMove){
			this.animator.SetBool("Move", false);

			//ステージ間移動
			if(Input.GetKey(KeyCode.RightArrow) && eastStage){
				this.currentObj = eastStage;
				this.action(eastStage.transform.position, Ease.InFlash);
			}
			if(Input.GetKey(KeyCode.LeftArrow) && westStage){
				this.currentObj = westStage;
				this.action(westStage.transform.position, Ease.InFlash);
			}

			//ステージ決定
			if(Input.GetKey(KeyCode.Space)){
				string nameSpace = currentObj.GetComponent<EventSelectStage>().stageName;
				if(nameSpace != ""){
 					StartCoroutine(this.canvasAccess(nameSpace));
  				}
	  		}
		}
	}

	public void action(Vector3 coord, Ease easeType){
		this.readyToMove = false;
		transform.DOMove(coord, this.howLongToArrive)
			/*.SetEase(easeType)*/.OnComplete(() => this.readyToMove = true);
		transform.LookAt(coord);
		this.animator.SetBool("Move", true);
		eastStage = this.currentObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.east];
		westStage = this.currentObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.west];
	}

	//選んだステージのシーンへ移動する時は、キャンバスのイベントを経由して処理が実行される。
	protected IEnumerator canvasAccess(string nameSpace){
		this.isFreezing = true;
		transform.eulerAngles = Vector3.zero;
		animator.SetTrigger("GoAhead");
		yield return new WaitForSeconds(1.5f);

		var canvasCs = this.canvas.GetComponent<UIEventLoadScene>();
		canvasCs.sceneName = nameSpace;
		canvasCs.startToFadeOut();
	}
}
