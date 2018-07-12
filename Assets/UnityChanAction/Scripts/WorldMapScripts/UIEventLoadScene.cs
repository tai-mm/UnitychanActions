using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIEventLoadScene : MonoBehaviour {
	public GameObject curtain;
	public string sceneName = "Stage";
	public bool showStarted = false;
	public float alfa = 0.0f;
	private Image curtainColor;
	private bool readyToLoad = false;

	void Start () {
		this.curtainColor = this.curtain.GetComponent<Image>();
	}
	
	void Update () {
		if(this.showStarted){
			this.curtainColor.color = new Color(0f, 0f, 0f, this.alfa);
			this.alfa += 0.015f;
		}

		if(this.alfa > 1.0f && this.readyToLoad){
			StartCoroutine(this.loading());
		}
	}

	//Playerのメソッドから呼ばれる。
	public void startToFadeOut(){
		this.showStarted = true;
		this.curtain.transform
			.DOMove(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0f), 1.0f)
			//.SetEase(easeType)
			.OnComplete(() => this.readyToLoad = true);
	}

	protected IEnumerator loading(){
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene(this.sceneName);
	}
}
