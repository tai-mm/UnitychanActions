using UnityEngine;
using System.Collections;

public class EffectRoller : MonoBehaviour {
	public int rotationState = 0;
	public float rotationSpeed = 7.0F;

	void Update () {
		switch(this.rotationState){

			case 1:
				transform.eulerAngles += new Vector3
				(0F, this.rotationSpeed * Time.deltaTime, 0F);
			break;

			case 2:
				transform.eulerAngles += new Vector3
				(0F, -this.rotationSpeed * Time.deltaTime, 0F);
			break;

			default:
			break;
		}
	}
}
