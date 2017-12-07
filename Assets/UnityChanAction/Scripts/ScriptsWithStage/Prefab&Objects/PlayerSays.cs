using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSays : MonoBehaviour {
	public GameObject unityChan;
	
	void Update () {
		Vector3 basePos = this.unityChan.transform.position;
		transform.position = new Vector3(basePos.x, basePos.y + 1.5f, basePos.z);
	}
}
