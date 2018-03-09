using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventVias : MonoBehaviour {
	public GameObject player;

	public bool caPlayerText(){
		if(this.player != null){
			this.player.GetComponent<UnityChanUI>().cancelAnimation();
			return true;

		}else{
			return false;
		}
	}
}
