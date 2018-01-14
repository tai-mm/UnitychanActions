using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityAIUnityChanUIs : MonoBehaviour {
	public Animator textAnim;
	public GameObject playerSays;
	public bool canUseAllowCheats = false;
	private Text sayThat;

	void Start () {
		this.sayThat = this.playerSays.GetComponent<Text>();
		this.sayThat.color = new Color(255f, 255f, 255f, 0.0f);
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Item"){
			this.sayingController("+1");
		}
	}

	private void sayingController(string what){
		this.sayThat.text = what;
		this.sayThat.color = new Color(255f, 255f, 255f, 255.0f);
		this.textAnim.SetBool("Move", true);
	}

	public void cancelAnimation(){
		this.textAnim.SetBool("Move", false);
		this.sayThat.color = new Color(255f, 255f, 255f, 0.0f);
	}
}
