using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class WorldMapPlayer : MonoBehaviour {
	public Animator animator;
	public Rigidbody rigidBody;
	public GameObject canvas;
	public float howLongToArrive;
	public bool readyToMove = true;
	public bool isFreezing = false;

	void Update () {
		if(this.readyToMove){
			this.animator.SetBool("Move", false);
		}
	}

	void OnTriggerStay(Collider collider){
		GameObject colObj = collider.gameObject;
		if(colObj.tag == "Stage" && !this.isFreezing){
			var eastStage = colObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.east];
			var westStage = colObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.west];

			//ステージ間移動
			if(this.readyToMove){
				if(Input.GetKey(KeyCode.RightArrow) && eastStage){
					this.action(eastStage.transform.position, Ease.Flash);
				}

				if(Input.GetKey(KeyCode.LeftArrow) && westStage){
					this.action(westStage.transform.position, Ease.Flash);
				}
			}

			//ステージ決定
			if(Input.GetKey(KeyCode.Space)){
				string nameSpace = colObj.GetComponent<EventSelectStage>().stageName;
				if(nameSpace != ""){
					StartCoroutine(this.canvasAccess(nameSpace));
				}
			}
		}
	}

	public void action(Vector3 coord, Ease easeType){
		this.readyToMove = false;
		transform.DOMove(coord, this.howLongToArrive * Time.deltaTime)
			.SetEase(easeType).OnComplete(() => this.readyToMove = true);
		transform.LookAt(coord);
		this.animator.SetBool("Move", true);
	}

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
