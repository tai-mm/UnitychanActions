using UnityEngine;
using System.Collections;

public class EventObjectSpawn : MonoBehaviour {
	public GameObject obsPrefabs;
	public bool isProStayer = false;
	private int whereToStay;

	void Start () {
		for(int limit = 0; limit < 14; limit++){

			if(limit < 7){

				GameObject instance = Instantiate (this.obsPrefabs, new Vector3(transform.position.x, 
				transform.position.y, (float)limit * 30.0F), transform.rotation) as GameObject;
				instance.transform.localScale = this.getRandomScale((float)limit / 20);
			}else{
				this.whereToStay = Random.Range(-1, 1);

				if(this.whereToStay != -1){
					this.whereToStay++;
				}
				GameObject instance = Instantiate (this.obsPrefabs, new Vector3(
					transform.position.x + (float)this.whereToStay, 
					transform.position.y, 
					(float)limit * 30.0F), 
				transform.rotation) as GameObject;
				instance.transform.localScale = new Vector3(
					transform.localScale.x - 3.0F, 
					transform.localScale.y + 5.0F, 
					transform.localScale.z - 2.0F
					);
			}
		}
	}

	private Vector3 getRandomScale(float limit){
		return new Vector3(
			transform.localScale.x, 
			Random.Range(transform.localScale.y - 1.0F, transform.localScale.y + 1.0F * limit),
			Random.Range(transform.localScale.z - 1.0F, transform.localScale.z + 0.0F * limit)
			);
	}
}
