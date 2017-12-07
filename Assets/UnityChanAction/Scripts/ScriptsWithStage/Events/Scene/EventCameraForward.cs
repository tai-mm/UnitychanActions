using UnityEngine;
using System.Collections;

public class EventCameraForward : MonoBehaviour {
	public GameObject camera;
	public int forward = 2;
	private EntityAICamera cameraSc;

	void Start () {
		this.camera = GameObject.Find("MainCamera");
		if(this.camera != null){
			this.cameraSc = this.camera.
				GetComponent<EntityAICamera>();
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		if(collision.gameObject.tag == "Player"){
			this.cameraSc.direction = this.forward;
		}
	}
}
