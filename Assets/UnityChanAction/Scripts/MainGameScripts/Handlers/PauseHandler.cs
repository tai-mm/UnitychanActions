using UnityEngine;
using System.Collections;

public class PauseHandler : MonoBehaviour {
	public Animator animator;
	public GameObject pauseMenu;
	public bool deleteContents;

	void Awake (){
		this.pauseMenu.SetActive(false);
		this.animator = this.pauseMenu.GetComponent<Animator>();
		this.deleteContents = true;
	}
	
	void Update () {
		if(this.gameObject.activeSelf){

			if(Input.GetKeyDown(KeyCode.P)){

				if(Time.timeScale == 1){
					StartCoroutine(setView());
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

	public IEnumerator deleteView(){
		this.animator.speed = -1;
		yield return new WaitForSeconds(0.5F);

		this.pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
}
