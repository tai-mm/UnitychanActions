using UnityEngine;
using System.Collections;

public class PauseHandler : MonoBehaviour {
	public Animator animator;
	public bool deleteContents;
	private GameObject pauseMenu;

	void Awake (){
		this.pauseMenu = GameObject.Find("TextUICanvas").
			transform.Find("Scenes").
			transform.Find("PauseView").gameObject;
		this.pauseMenu.SetActive(false);
		this.animator = 
			this.pauseMenu.GetComponent<Animator>();
		this.deleteContents = true;
	}
	
	void Update () {
		if(this.gameObject.activeSelf){

			if(Input.GetKeyDown(KeyCode.P)){

				if(Time.timeScale == 1){
					StartCoroutine(setView());

				}else{
					this.deleteView();
				}
			}
		}
	}

	private IEnumerator setView(){
		this.pauseMenu.SetActive(true);
		this.animator.SetBool("PauseView", true);

		yield return new WaitForSeconds(0.5F);
		Time.timeScale = 0;
	}

	private void deleteView(){
		this.pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
}
