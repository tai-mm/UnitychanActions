using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class WorldMapPlayer : MonoBehaviour {
	public Animator animator;
	public GameObject canvas;
	public float howLongToArrive;
	public bool readyToMove = true;
	public bool isFreezing = false;

	public GameObject currentObj;

	GameObject eastStage;
	GameObject westStage;

	private void Start() {
		eastStage = currentObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.east];
		westStage = currentObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.west];
	}


	void Update() {
		if (this.readyToMove) {
			this.animator.SetBool("Move", false);
		}

		//ステージ間移動
		if (this.readyToMove) {
			if (Input.GetKey(KeyCode.RightArrow) && eastStage) {
				currentObj = eastStage;
				this.action(eastStage.transform.position, Ease.InFlash);
			}

			if (Input.GetKey(KeyCode.LeftArrow) && westStage) {
				currentObj = westStage;
				this.action(westStage.transform.position, Ease.InFlash);
			}
		}

		//ステージ決定
		if (Input.GetKey(KeyCode.Space)) {
			string nameSpace = currentObj.GetComponent<EventSelectStage>().stageName;
			if (nameSpace != "") {
				StartCoroutine(this.canvasAccess(nameSpace));
			}
		}
	}

	public void action(Vector3 coord, Ease easeType) {
		eastStage = currentObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.east];
		westStage = currentObj.GetComponent<EventSelectStage>().stageObjs[EnumStagePos.direction.west];
		this.readyToMove = false;
		this.animator.SetBool("Move", true);
		transform.DOMove(coord, 1f)
			.SetEase(easeType).OnComplete(() => this.readyToMove = true);
		transform.LookAt(coord);
	}

	protected IEnumerator canvasAccess(string nameSpace) {
		this.isFreezing = true;
		transform.eulerAngles = Vector3.zero;
		animator.SetTrigger("GoAhead");
		yield return new WaitForSeconds(1.5f);

		var canvasCs = this.canvas.GetComponent<UIEventLoadScene>();
		canvasCs.sceneName = nameSpace;
		canvasCs.startToFadeOut();
	}
}
