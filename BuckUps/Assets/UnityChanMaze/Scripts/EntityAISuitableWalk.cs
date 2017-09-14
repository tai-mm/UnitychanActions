using UnityEngine;
using System.Collections;

public class EntityAI : MonoBehaviour {
	public float SpeedA = 4;
	protected int WalkTimer;

	// Use this for initialization
	void Start () {
		this.WalkTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * SpeedA;
	}
}
