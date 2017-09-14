using UnityEngine;
using System.Collections;

public class PrefabAIBase : MonoBehaviour {
	public int lifeTime = 5;

	void Start () {
		Invoke("setDead", this.lifeTime);
	}
	
	public void setDead(){
		Destroy(this.gameObject);
	}
	
	void Update () {
	
	}
}
