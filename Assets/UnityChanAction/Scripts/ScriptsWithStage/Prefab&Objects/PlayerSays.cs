using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSays : MonoBehaviour {
	public GameObject unityChan;
	public GameObject graphic;
	private MeshRenderer meshrenderer;

	void Awake () {
		this.meshrenderer = GetComponent<MeshRenderer>();
	}
	
	void Update () {
		Vector3 basePos = this.unityChan.transform.position;
		transform.position = new Vector3(basePos.x, basePos.y + 1.5f, basePos.z);
	}

	void OnEnable () {
		StartCoroutine(this.fadeOut());
	}

	public IEnumerator fadeOut(){
		for(int i = 255; i < 0; i--){
			yield return new WaitForSeconds(0.3f);
			meshrenderer.material.color = new Color(0, 0, 0, (float)i);
		}
	}
}
