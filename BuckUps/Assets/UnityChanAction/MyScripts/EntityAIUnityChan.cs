using UnityEngine;
using System.Collections;

public class UnityChanAI : MonoBehaviour {
	public Animator animator;
	public float SpeedA = 4;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.A)){
			//transform.eulerAngles = new Vector3(0f, 270, 0f);
			transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y - 5, 0f);
		}

		if (Input.GetKey(KeyCode.D)){
			//transform.eulerAngles = new Vector3(0f, 90, 0f);
			transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 5, 0f);
		}

		if (Input.GetKeyDown(KeyCode.S)){
			//transform.eulerAngles = new Vector3(0f, 90, 0f);
			transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + 180, 0f);
		}

		if(Input.GetKey(KeyCode.W)){
			//transform.eulerAngles = new Vector3(0f, 0f, 0f);
			transform.position += transform.forward * Time.deltaTime * SpeedA;
			animator.SetTrigger ("Walk");
			animator.SetBool ("Idle", false);
		}

		/*if(Input.GetKey(KeyCode.S)){
			transform.eulerAngles = new Vector3(0f, 180f, 0f);
			transform.position += transform.forward * Time.deltaTime * SpeedB;
			animator.SetTrigger ("Walk");
			animator.SetBool ("Idle", false);
		}*/
		
		if(Input.GetKeyDown(KeyCode.Space)){
			GetComponent<Rigidbody>().velocity = Vector3.up * 4.2f;
			
		}
		if(Input.GetKeyUp(KeyCode.W)){
			animator.SetBool ("Idle", true);
		}
		if(Input.GetKeyUp(KeyCode.S)){
			animator.SetBool ("Idle", true);
		}
		if(Input.GetKeyUp(KeyCode.A)){
			animator.SetBool ("Idle", true);
		}
		if(Input.GetKeyUp(KeyCode.D)){
			animator.SetBool ("Idle", true);
		}
		if(Input.GetKeyDown(KeyCode.K)){
			animator.SetBool ("Kick", true);
			animator.SetBool ("Idle", false);
		}
		if(Input.GetKeyUp(KeyCode.K)){
			animator.SetBool ("Kick", false);
			animator.SetBool ("Idle", true);
			//GameObject.Find("WoodCrate")
		}
	}
}
