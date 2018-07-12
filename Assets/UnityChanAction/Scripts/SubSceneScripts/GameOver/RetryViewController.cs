using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RetryViewController : MonoBehaviour {

	public void SceneLoad () {
		//Application.LoadLevel("TitleView");
		SceneManager.LoadScene("TitleView");
	}
}
