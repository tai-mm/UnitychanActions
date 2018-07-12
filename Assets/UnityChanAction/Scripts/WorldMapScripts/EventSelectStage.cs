using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSelectStage : MonoBehaviour {
	public string stageName;
	public GameObject northside;
	public GameObject southside;
	public GameObject eastside;
	public GameObject westside;
	//方角とステージを関連付け
	public Dictionary<Const.EnumDirection, GameObject> stageObjs 
		= new Dictionary<Const.EnumDirection, GameObject>();

	void Awake () {
		//"stageObjs"の中に、指定されたステージオブジェクトを入れる
		this.stageObjs.Add(Const.EnumDirection.North, northside);
		this.stageObjs.Add(Const.EnumDirection.South, southside);
		this.stageObjs.Add(Const.EnumDirection.East, eastside);
		this.stageObjs.Add(Const.EnumDirection.West, westside);
	}
}
