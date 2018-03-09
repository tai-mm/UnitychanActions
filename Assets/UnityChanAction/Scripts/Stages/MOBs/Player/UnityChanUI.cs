using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanUI : MonoBehaviour {
	public Animator textAnim;
	public Image playerIcon;
	public GameObject playerSays;
	public GameObject prefab_ItemEff;
	public GameObject parentOfEffect;
	public bool canUseAllowCheats = false;
	private Text sayThat;

	void Start () {
		this.setEnabledToIcon(false);
		this.sayThat = this.playerSays.GetComponent<Text>();
		this.sayThat.color = new Color(255f, 255f, 255f, 0.0f);
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Item"){
			this.sayingController("+1");
			this.summonEffect();
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "EventObject"){
			this.setEnabledToIcon(true);
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.tag == "EventObject"){
			this.setEnabledToIcon(false);
		}
	}

	private void sayingController(string what){
		this.sayThat.text = what;
		this.sayThat.color = new Color(255f, 255f, 255f, 255.0f);
		this.textAnim.SetBool("Move", true);
	}

	public void summonEffect(){
		var spawness = GameObject.Instantiate(this.prefab_ItemEff, 
			transform.position, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
		spawness.transform.SetParent(this.parentOfEffect.transform, true);
		spawness.GetComponent<FollowPlayer>().player = this.gameObject;
	}

	public void cancelAnimation(){
		this.textAnim.SetBool("Move", false);
		this.sayThat.color = new Color(255f, 255f, 255f, 0.0f);
	}

	private void setEnabledToIcon(bool which){
		this.playerIcon.enabled = which;
		//this.playerIcon.FindChild("Text").gameObject.enabled = which;
	}
}
