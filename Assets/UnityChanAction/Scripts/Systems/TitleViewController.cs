using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleViewController : MonoBehaviour {

	public void PushStartButton () {
		Application.LoadLevel("StageSelecter");
		//SceneManager.LoadScene("StageSelecter");
	}
}
