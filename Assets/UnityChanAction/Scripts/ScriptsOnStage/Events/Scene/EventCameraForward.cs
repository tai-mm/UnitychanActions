using UnityEngine;
using System.Collections;

public class EventCameraForward : MonoBehaviour {
	public GameObject camera;
	public int forward = 2;
	private CameraOperates cameraSc;

	void Start () {
		this.camera = GameObject.Find("MainCamera");
		if(this.camera != null){
			this.cameraSc = this.camera.
				GetComponent<CameraOperates>();
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		if(collision.gameObject.tag == "Player"){
			this.cameraSc.direction = this.forward;
		}
	}
}
