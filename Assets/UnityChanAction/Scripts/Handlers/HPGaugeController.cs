using UnityEngine;
using System.Collections;

public class HPGaugeController : MonoBehaviour {
	private GameObject unityChan;
	private EntityAIUnityChan uniScript;
	private int flashing;

	void Start () {
		this.unityChan = GameObject.Find("unitychan");
		this.uniScript = this.unityChan.GetComponent<EntityAIUnityChan> ();
		this.flashing = 0;
	}
	
	void Update () {
		if(this.flashing > 0){
			--this.flashing;
		}else if(this.flashing <= 0){
			this.flashing = 20;
		}

		if(this.uniScript.getHealth() >= 3.0F){
			/*if(this.flashing > 10){
				GetComponent<Renderer>().material.color = new Color(
					0, 0, 0, 1.0F);
			}else{
				GetComponent<Renderer>().material.color = new Color(
					0, 0, 0, 100.0F);
			}*/
		}
	}
}
