using UnityEngine;
using System.Collections;

public class MovementSkyboxes : MonoBehaviour {
	public float rotateSpeed = 2.0F;
	protected float activeSpeed = 0.0F;
	
	void Update () {
		this.activeSpeed += 
			this.rotateSpeed * Time.deltaTime;
		this.activeSpeed %= 360.0F;

		RenderSettings.skybox.
			SetFloat("_Rotation", this.activeSpeed);
	}
}
