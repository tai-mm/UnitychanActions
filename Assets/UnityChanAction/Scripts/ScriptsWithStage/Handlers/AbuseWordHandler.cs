using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AbuseWordHandler : MonoBehaviour {
	private int randomize;

	void Awake () {
		this.randomize = Random.Range(1, 4);
	}

	void Start () {
		switch(this.randomize){

			case 1:
			this.GetComponent<Text>().text = "m9(^Д^)ﾌﾟｷﾞｬｰ";
			break;

			case 2:
			this.GetComponent<Text>().text = "え？もう終わり ＾＾？";
			break;

			case 3:
			this.GetComponent<Text>().text = "れてぃごーwww　れてぃごーwww";
			break;

			default :
			break;
		}
	}
}
