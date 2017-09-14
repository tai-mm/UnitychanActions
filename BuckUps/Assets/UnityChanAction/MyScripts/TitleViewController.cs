using UnityEngine;
using System.Collections;

public class TitleViewController : MonoBehaviour {

	public void PushStartButton () {
		print("1 ~DecayedVillage・Alea~");
		//print("1 ~朽ち果てた村・アレア~");
		Application.LoadLevel("UnityChanAction");	
	}
}
