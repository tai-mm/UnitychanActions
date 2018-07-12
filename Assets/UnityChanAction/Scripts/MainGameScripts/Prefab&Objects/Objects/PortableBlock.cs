using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableBlock : MonoBehaviour {
	public UIInventory uiInventCs;//UIInventoryスクリプト
	public Animator animator;
	public Sprite itemIcon;
	public GameObject particle;
	public int itemNumberOnInventory = 0;
	public string name;
	private bool haveParent = false;

	void Awake () {
		this.particle.SetActive(false);
		this.animator.SetBool("Stillness", true);
	}
	
	void Update () {
		if(WorldManager.Instance.nowWorldOn == Const.EnumWorldSituation.InventoryView){
			this.particle.SetActive(true);
		}else if(this.particle.activeSelf){
			this.particle.SetActive(false);
		}

		//親のオブジェクトにプレイヤーがいたら、haveParentを旗揚げ
		//いなければ、旗を降ろす
		if(!this.haveParent && transform.root.gameObject.tag == "Player"){
			this.haveParent = true;
			this.animator.SetBool("Stillness", false);

		}else if(this.haveParent && transform.root.gameObject.tag != "Player"){
			this.haveParent = false;
		}
	}

	void OnTriggerStay(Collider collider){
		GameObject colObj = collider.gameObject;

		if(colObj.tag == "Player"){

			//プレイヤーの吹き出しのテキストを編集
			var uiControllCs = colObj.GetComponent<UnityChanUI>();
			if(uiControllCs){
				uiControllCs.setEnabledToIcon(2, true);
				uiControllCs.setIcon(this.itemIcon);
			}

			//このブロックを持ち運ぶ
			if(Input.GetKeyDown(KeyCode.B)){
				this.uiInventCs.putItemData(this.itemNumberOnInventory, this.name);	//UIのインベントリのアイテムデータにこれを記録
				var inventCs = colObj.GetComponent<UnityChanInventory>();	//プレイヤーのインベントリ
				inventCs.setItemToInventory(this.name, this.gameObject);	//プレイヤーのインベントリにこれを追加
				inventCs.handingObject = this.name;	//プレイヤーの手持ちアイテムをこれに変える
			}
		}
	}

	public bool getParentOfThis(){
		return this.haveParent;
	}
}