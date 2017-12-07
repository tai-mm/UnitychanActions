using UnityEngine;
using System.Collections;

public class EventVias : MonoBehaviour {
	public GameObject parentScene;

	public void throughSpace(int roomOf){
		var sceneCs = this.parentScene.GetComponent<EventScenes>();

		switch(roomOf){
			case 1:
				sceneCs.comingFade();
			break;

			case 2:
				sceneCs.endTheFade();
			break;

			default:
			break;
		}
	}
}
