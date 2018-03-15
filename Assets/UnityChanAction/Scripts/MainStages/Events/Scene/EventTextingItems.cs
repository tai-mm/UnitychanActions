using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventTextingItems : MonoBehaviour {
	public Animator animator;
	private Text itemTexter;
	private UnityChanMoves uniScript;
	private int fadeTime = 0;
	private bool isGot = false;

	void Start () {
		this.uniScript = GameObject.Find("unitychan")
			.GetComponent<UnityChanMoves> ();
		this.itemTexter = GetComponentInChildren<Text> ();
	}

	void Update () {
		if(this.uniScript.getEquipment() != "null"){
			if(this.fadeTime <= 0 && !this.isGot){
				animator.SetBool("FadeTime", true);
				this.fadeTime = 50;
				this.isGot = true;
				this.itemTexter.text = "You picked up the " + this.uniScript.getEquipment() 
					+ ".";
			}else if(this.fadeTime > 0){
				--this.fadeTime;
			}else if(this.fadeTime <= 0 && this.isGot){
				animator.SetBool("FadeTime", false);
			}
		}
	}

	public void retryItemHold(){
		this.isGot = false;
	}
}
