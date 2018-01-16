using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class CapturingScreen : MonoBehaviour {
	public string photoName;
	bool take;

	void Awake (){
		take = false;
	}

	void Update (){
		if(Input.GetKeyDown(KeyCode.C)){
			take = true;
			//Application.CaptureScreenshot("Assets/ScreenShots/Shot.png");
		}
	}

	void OnPostRender()
	{
		// DateTime theTime = DateTime.Now;
		// photoName += theTime.ToString("G");
		if(take)
    	{
	    	var texture = new Texture2D(Screen.width, Screen.height);
		    texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
	    	texture.Apply ();

		    byte[] pngData = texture.EncodeToPNG();
		    Destroy(texture);
		    File.WriteAllBytes(Application.dataPath + "/UnityChanAction/ScreenShots/" + photoName, pngData);

		    take = false;
		}
	}
}
