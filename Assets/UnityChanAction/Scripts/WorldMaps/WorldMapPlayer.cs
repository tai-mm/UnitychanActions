using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WorldMapPlayer : MonoBehaviour {
	public Animator animator;
	public Rigidbody rigidBody;
	public float moveSpeed;
	public bool readyToMove = true;

	void OnTriggerStay(Collider collider){
		GameObject colObj = collider.gameObject;
		if(colObj.tag == "Stage"){
			var eastStage = colObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.east];
			var westStage = colObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.west];

			if(this.readyToMove){
				if(Input.GetKey(KeyCode.RightArrow) && eastStage){
					this.action(eastStage.transform.position, Ease.Flash);
				}

				if(Input.GetKey(KeyCode.LeftArrow) && westStage){
					this.action(westStage.transform.position, Ease.Flash);
				}
			}
		}
	}

	public void action(Vector3 coord, Ease easeType){
		transform.DOMove(coord, this.moveSpeed * Time.deltaTime)
			.SetEase(easeType).OnComplete(() => this.readyToMove = true);
		//this.animator.SetBool("Walk", true);
	}
}
