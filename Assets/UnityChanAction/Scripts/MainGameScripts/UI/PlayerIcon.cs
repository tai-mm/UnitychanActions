using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour {
	public GameObject player;
	private RectTransform recTrans;

	void Start(){
		this.recTrans = GetComponent<RectTransform>();
	}
	
	void Update () {
		recTrans.position = RectTransformUtility.WorldToScreenPoint
			(Camera.main, this.player.transform.position + this.nowOffset());
	}

	private Vector3 nowOffset(){
		var cameraDir = CameraManager.Instance.nowDir;
		switch(cameraDir){
			default:
				return new Vector3(1.0f, 2.5f, 0f);
			break;

			case Const.EnumCameraDir.Right:
				return new Vector3(0f, 2.5f, 1.0f);
			break;

			case Const.EnumCameraDir.Left:
				return new Vector3(0f, 2.5f, -1.0f);
			break;

			case Const.EnumCameraDir.Opposition:
				return new Vector3(-1.0f, 2.5f, 0f);
			break;
		}
	}
}
