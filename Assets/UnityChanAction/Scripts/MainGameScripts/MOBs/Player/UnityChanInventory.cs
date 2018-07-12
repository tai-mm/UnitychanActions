using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanInventory : MonoBehaviour {
	public Dictionary<string, GameObject> inventory = new Dictionary<string, GameObject>();//インベントリは、オブジェクト名とゲームオブジェクト情報を記録するディクショナリーである
	[SerializeField]public string handingObject;

	void Update(){
		this.putItemBlock();
	}

	public void putItemBlock(){	//アイテムを置く
		if(handingObject != "" && handingObject != null){
			if(this.inventory.ContainsKey(this.handingObject)){
				if(Input.GetKeyDown(KeyCode.K)){
					this.inventory[this.handingObject].SetActive(true);
					this.inventory[this.handingObject].transform.parent = null;
					this.removeItemOnInventory(this.handingObject);
				}
			}
		}
	}

	//インベントリに持つブロックを記録
	public void setItemToInventory(string name, GameObject obj){
		if(!this.inventory.ContainsKey(name)){
			this.inventory.Add(name, obj);
			Debug.Log(name + " : " + obj);

			obj.transform.SetParent(this.transform, true);
			obj.SetActive(false);

			var playersItem = GameObject.Find(name);
			playersItem.SetActive(false);
		}
	}

	public void switchHandingItem(string itemName){

	}

	//インベントリからアイテムを削除	
	public void removeItemOnInventory(string name){
		this.inventory.Remove(name);
	}

	/*public void resizingInventory(int size){
		System.Array.Resize(ref this.inventory, size);
	}*/
}