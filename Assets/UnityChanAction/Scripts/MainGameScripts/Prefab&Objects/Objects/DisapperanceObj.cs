﻿using UnityEngine;
using System.Collections;

public class DisapperanceObj : MonoBehaviour {
	public int lifeTime;
	
	void Update () {
		--this.lifeTime;
		
		if(this.lifeTime != null &&
			this.lifeTime <= 0){
			Destroy(this.gameObject);
		}
	}
}
