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
	public Dictionary<EnumStagePos.direction, GameObject> stageObjs 
		= new Dictionary<EnumStagePos.direction, GameObject>();

	void Start () {
		//"stageObjs"の中に、指定されたステージオブジェクトを入れる
		this.stageObjs.Add(EnumStagePos.direction.north, northside);
		this.stageObjs.Add(EnumStagePos.direction.south, southside);
		this.stageObjs.Add(EnumStagePos.direction.east, eastside);
		this.stageObjs.Add(EnumStagePos.direction.west, westside);
	}
}
