using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoves : MonoBehaviour {
	public GameObject player;
	private Vector3 offset = Vector3.zero;

	void Start () {
		this.offset = transform.position - player.transform.position;
	}

	void Update () {
		Vector3 playerPos = this.player.transform.position;
		Vector3 newPos = transform.position;
		newPos.x = playerPos.x + this.offset.x;
		newPos.y = playerPos.y + this.offset.y;
		newPos.z = playerPos.z + this.offset.z;
		transform.position = Vector3.Lerp(transform.position, newPos, 10.0f * Time.deltaTime);
	}
}
