using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public GameObject player;

	void Update () {
		transform.position = this.player.transform.position + new Vector3(0f, 1.9f, 0f);
	}
}
