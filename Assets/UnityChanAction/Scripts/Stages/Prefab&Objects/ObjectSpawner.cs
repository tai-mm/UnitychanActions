using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
	[SerializeField] int interval = 0;
	public int maxSpawnRange = 0;
	public GameObject prefab;
	public Vector3 whereFor;
	public bool couldSpawn = true;
	
	void Update () {
		if(this.couldSpawn){
			++this.interval;
			if(this.interval > this.maxSpawnRange){
				this.interval = 0;
				GameObject spawness = GameObject.Instantiate
					(this.prefab, transform.position + new Vector3(0f, 1.0f, 0f), transform.rotation) as GameObject;
				spawness.transform.SetParent(this.transform, true);
			}
		}
	}

	public Vector3 getVector(){
		return this.whereFor;
	}
}
