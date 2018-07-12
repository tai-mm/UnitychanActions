using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionButtonHandler : MonoBehaviour {
	[SerializeField] Button optionB;
	[SerializeField] Button backB;
	[SerializeField] Button quitB;
	List<Button> buttons = new List<Button>();
	public GameObject processor;

	protected void Awake(){
		buttons.Add(optionB);
		buttons.Add(backB);
		buttons.Add(quitB);
	}

	protected void Start(){
		foreach(var buttonCopy in buttons){
			buttonCopy.onClick.AddListener(() => {
				var buttonType = buttonCopy.GetComponent<ButtonTypes>().type;
				this.makeSomething(buttonType);
				print(buttonCopy);
			});
		}
	}

	private void makeSomething(Const.EnumPauseButton type){
		switch(type){
			default:
			break;

			case Const.EnumPauseButton.BackToGame:
				var proSc = this.processor.GetComponent<PauseHandler>();
				proSc.StartCoroutine(proSc.deleteView());
			break;

			case Const.EnumPauseButton.SaveAndQuit:
				SceneManager.LoadScene("TitleView");
			break;
		}
	}
}
