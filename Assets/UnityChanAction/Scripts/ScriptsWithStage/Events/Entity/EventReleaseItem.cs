using UnityEngine;
using System.Collections;

public class EventReleaseItem : MonoBehaviour {
	public Animator animator;
	public GameObject treasureBox;
	public float throwAtX = 0F;
	public float throwAtZ = 0F;
	private EventTreasureBox boxCs;
	private bool isReleased;

	void Start () {
		this.isReleased = false;
		this.boxCs = this.treasureBox.
				GetComponent<EventTreasureBox>();
	}
	
	//宝箱が開いたら、アイテムが飛び出す
	void Update () {
		bool open = this.boxCs.getState();

		if(open && !this.isReleased){
			this.startRelease();
		}
	}

	protected void startRelease(){
		//!：この処理は、一番初めに書かないといけない
		this.isReleased = true;

		animator.SetBool("JumpOver", true);
	}
}
