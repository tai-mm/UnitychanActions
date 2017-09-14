using UnityEngine;
using System.Collections;

public class EventChangeCamera : MonoBehaviour {
	public GameObject cameraMain;
	public GameObject cameraSub;

	public void changeCamera(bool flag){
		if(flag){
			cameraSub.SetActive(true);
			cameraMain.SetActive(false);
		}else{
			cameraMain.SetActive(true);
			cameraSub.SetActive(false);
		}
	}
}
