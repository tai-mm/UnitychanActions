using UnityEngine;
using System.Collections;

public class EventTreeSpawn : MonoBehaviour {
	public GameObject treePrefabs;
	private Quaternion randomize;
	private int count = 0;

	void Awake () {
		for(int limit = 0; limit < 22; limit++){
			Instantiate (this.treePrefabs, new Vector3(transform.position.x, transform.position.y, 
				(float)limit * 20.0F), transform.rotation = this.getRandomRotate());
		}
	}

	private Quaternion getRandomRotate(){
		this.randomize = Quaternion.Euler(transform.rotation.x, 
		Random.Range(0.0F, 359.0F), transform.rotation.z);
		return Quaternion.Slerp(transform.rotation, randomize, 3.0F);
	}
}
