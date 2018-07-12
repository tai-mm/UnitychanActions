using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanUI : MonoBehaviour {
	public GameObject speaker;	//文章のみ
	public GameObject itemViwer;	//アイテムのアイコン表示のみ
	public GameObject almightViwer;//両方
	public GameObject prefab_ItemEff;

	void Start () {
		this.setEnabledToIcon(4, false);
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Item"){
			this.summonEffect();
		}
	}

	void OnTriggerEnter(Collider collider){
		var colObj = collider.gameObject;//まず、イベントを持つオブジェクトかどうかをタグで判定
		if(colObj.gameObject.tag == "EventObject"){

			var tresureObj = colObj.GetComponentInChildren<EventTreasureBox>();
			if(tresureObj != null && !tresureObj.getState()){//宝箱の場合
				this.setEnabledToIcon(2, true);
			}
		}
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.tag == "EventObject"){
			this.setEnabledToIcon(4, false);
		}
	}

	//吹き出しの中のテキストを編集
	public void setText(string arg){
		var textObject = this.speaker.transform.Find("Text").gameObject;
		if(arg != null){
			textObject.GetComponent<Text>().text = arg;
		}
	}

	//アイテムアイコンを変える
	public void setIcon(Sprite contents){
		var iconImage = this.itemViwer.transform.Find("Image").gameObject;
		if(contents != null){
			iconImage.GetComponent<Image>().sprite = contents;
		}
	}

	//アイテムを取ったときのエフェクトを生成する
	public void summonEffect(){
		var spawness = GameObject.Instantiate(this.prefab_ItemEff, 
			transform.position, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
		spawness.transform.SetParent(this.transform, true);
		spawness.GetComponent<FollowPlayer>().player = this.gameObject;
	}

	//吹き出しとアイコンをoffにする
	public void setEnabledToIcon(int type, bool which){
		if(type <= 1 || type >= 4){
			this.speaker.gameObject.SetActive(which);
		}
		if(type == 2 || type >= 4){
			this.itemViwer.gameObject.SetActive(which);
		}
		if(type == 3 || type >= 4){
			this.almightViwer.gameObject.SetActive(which);
		}
	}
}
