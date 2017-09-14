using UnityEngine;
using System.Collections;

public class BlockKick : MonoBehaviour {
	public bool WhenKick;
	public GameObject WoodParticle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.K)){
			WhenKick = true;
		}
	}

	void OnTriggerEnter (Collider coll){
		if(!(coll.gameObject.name != "WoodCrate")){
			if(WhenKick == true){
				GameObject wp = Instantiate (WoodParticle, 
					coll.gameObject.transform.position, 
					Quaternion.identity) as GameObject;
				Destroy(coll.gameObject);
				WhenKick = false;
			}else{
				
			}
		}
	}
}
