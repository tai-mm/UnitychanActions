using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {
	public UnityChanInventory playerInventCs;
	public GameObject panel;//インベントリ画面のパネル
	public Image selectBar;
	public Image[] images = new Image[3];
	private Dictionary<int, string> itemData = new Dictionary<int, string>();//PortableBlockスクリプトから、手に入れたアイテムのデータを受け取り、ここに記録しておく	記録内容は、「アイテム名」と「インベントリ番号」
	private int barIsOn;	//セレクトバーの位置をここに記録。0が一番上、2が一番下。

	void Start(){
		this.barIsOn = 0;
	}

	void Update(){	//各動作の心臓部
		if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.L)){
			this.selectBarMoves();	//J、Lキーで選択バーを動かす
		}
		if(Input.GetKeyDown(KeyCode.KeypadEnter)){
			print("KOJF");
			this.enterToChooseItem();	//エンターキーでアイテムを選択
		}
	}

	public void enterToChooseItem(){
		if(this.itemData.ContainsKey(this.barIsOn)){
			//this.playerInventCs.switchHandingItem(this.itemData[this.barIsOn].Key);
			print(this.itemData[this.barIsOn]);
		}
	}

	public void selectBarMoves(){
		int i = 0;
		if(Input.GetKeyDown(KeyCode.L)){
			if(this.barIsOn == 1){
				i = 2;
			}else if(this.barIsOn <= 0){
				i = 1;
			}else if(this.barIsOn >= 2){
				i = 2;
			}
		}
		if(Input.GetKeyDown(KeyCode.J)){
			if(this.barIsOn == 1){
				i = 0;
			}else if(this.barIsOn >= 2){
				i = 1;
			}
		}
		this.selectBar.GetComponent<RectTransform>().localPosition = this.images[i].GetComponent<RectTransform>().localPosition;
		this.barIsOn = i;
	}

	public void selectBarMoves(int number){
		this.selectBar.GetComponent<RectTransform>().localPosition = this.images[number].GetComponent<RectTransform>().localPosition;
	}

	public void putItemData(int number, string name){
		if(!this.itemData.ContainsKey(number)){
			this.itemData.Add(number, name);
		}
	}

	public int getBarForItsPosition(){	//選択バーの位置を取得
		return this.barIsOn;
	}
}
