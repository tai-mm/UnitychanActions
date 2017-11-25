using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIEventLoadScene : MonoBehaviour {
	public GameObject curtain;
	public Animator animator;
	public string sceneName = "Stage";
	public bool showStarted = false;
	public float alfa = 0.0f;
	private Image curtainColor;

	void Start () {
		this.curtainColor = this.curtain.GetComponent<Image>();
		this.animator = this.curtain.GetComponent<Animator>();
	}
	
	void Update () {
		if(this.showStarted){
			this.curtainColor.color = new Color(0f, 0f, 0f, this.alfa);
			this.alfa += 0.015f;
		}

		if(this.alfa > 1.0f){
			StartCoroutine(this.loading());
		}
	}

	public void startToFadeOut(){
		this.showStarted = true;
		this.animator.SetBool("Going", true);
	}

	protected IEnumerator loading(){
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(this.sceneName);
	}
}
