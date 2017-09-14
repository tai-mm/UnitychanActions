using UnityEngine;
using System.IO;
using System.Collections;
using System;

public class OutputLogs : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.H)){
			this.generateNewLog("I am a winner!");
		}
	}

	public void generateNewLog(string content){
		StreamWriter writer;
		FileInfo info = new FileInfo(Application.dataPath 
			+ "/EventLogs/GameLog/OutputLogs.csv");
		//書き出しの準備
		writer = info.AppendText();

		//書き出し
		writer.WriteLine(content);

		writer.Flush();
		writer.Close();
	}
}
