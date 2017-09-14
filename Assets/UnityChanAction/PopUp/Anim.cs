using UnityEngine;
using System.Collections;

public class Anim : MonoBehaviour {
	public Animator animator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.P)){
			animator.SetBool ("Entry", true);
		}
	
	}
}
