using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : MonoBehaviour {
	
	void OnCollisionEnter (Collision collision) {
		var obj = collision.gameObject;
		if(obj.tag == "Player"){
			var toJump = StartCoroutine(obj.GetComponent<EntityAIUnityChanMoves>().jump(15.0f));
		}
	}
}
