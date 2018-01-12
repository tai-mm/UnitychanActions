using UnityEngine;
using System.Collections;

public class EventIntoARoom : MonoBehaviour {
	public GameObject unityChan;
	public GameObject cameraMain;//メインカメラ
	public GameObject camerasSub;//特殊カメラ
	
	void Start () {
		camerasSub.SetActive(false);
	}

	public void changeFor(int typeOf){
		var uchanCs = unityChan.GetComponent<EntityAIUnityChanMoves>();
		
		switch(typeOf){
			//1-8　部屋に入る時
			case 1:
				//サブカメラをアクティブに、メインカメラを非アクティブに
				camerasSub.SetActive(true);
				cameraMain.SetActive(false);
				unityChan.transform.position -= 
					new Vector3(0F, 0F, 3.0F);

				//Unityちゃんのフリーズ状態を解除
				uchanCs.isFreezing = false;
			break;

			//1-8　部屋から出る時
			case 2:
				cameraMain.SetActive(true);
				camerasSub.SetActive(false);
				unityChan.transform.position += 
					new Vector3(0F, 0F, 3.0F);

				uchanCs.isFreezing = false;
			break;

			default:
			break;
		}
	}
}