using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoves : MonoBehaviour {
	public GameObject player;

	void Update () {
		Vector3 defPos = this.player.transform.position;
		transform.position = new Vector3(defPos.x, defPos.y + 7, defPos.z + 13);
	}
}
