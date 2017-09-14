using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappGround : MonoBehaviour {
	public GameObject[] grounds = new GameObject[4];

	void Start () {
		if(this.grounds[0] != null){
			this.grounds[0].SetActive(false);
		}
		StartCoroutine(activation());
	}
	
	protected IEnumerator activation(){
		for(int turn = 0; turn < this.grounds.Length; ++turn){
			yield return new WaitForSeconds(1.2F);

			this.grounds[turn].SetActive(false);
			int slot = turn - 1;
			if(slot < 0){
				slot = 3;
			}
			if(!this.grounds[slot].activeSelf){
				this.grounds[slot].SetActive(true);
			}
		}
		this.turning();
		yield break;
	}

	protected void turning(){
		StartCoroutine(activation());
	}
}
