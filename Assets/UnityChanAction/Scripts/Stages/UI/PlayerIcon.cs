using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour {
	public GameObject player;
	private Vector3 offset = new Vector3(1.8f, 2.7f, 0f);
	private RectTransform recTrans;

	void Start(){
		this.recTrans = GetComponent<RectTransform>();
	}
	
	void Update () {
		recTrans.position = RectTransformUtility.WorldToScreenPoint
			(Camera.main, this.player.transform.position + offset);
	}
}
