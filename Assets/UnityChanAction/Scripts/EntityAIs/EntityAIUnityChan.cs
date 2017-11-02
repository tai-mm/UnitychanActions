using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// -Unityちゃんの動き(移動・ジャンプ・アニメーション)
/// -Unityちゃんの装備に関すること
/// -UIに関すること
/// -バグ修正機能
/// </summary>
public class EntityAIUnityChan : MonoBehaviour {
	public Animator animator;
	public Rigidbody rigidBody;
	public Text hpLabel;
	public Image hpGauge;
	public RaycastHit hit;
	public GameObject[] itemIcons = new GameObject[3];
	public GameObject keyText;
	public GameObject prefab_footPR;
	public string isIn = "Outside";
	public int damageDown = 0;
	public bool canUseAllowCheats = false;
	public bool isFreezing = false;
	protected bool isInvisiblity = false;
	protected bool isJump = false; 
	protected bool onGround = false;
	protected float speedKeeper;
	protected float health = 10.0F;
	protected float attackDamage = 10.0F;
	protected float baseHp;
	protected float moveSpeed = 12.0F;
	protected float strongLeg = 3.0F;
	//protected byte[] entityEffects = new byte[];
	private Plane plane = new Plane();
	private AnimatorStateInfo stateInfo;
	private Vector3 startPoint;
	private bool canJump = false;
	private bool startFall = false;
	private int jumpLagtime = 0;
	private int deadTime = 0;
	private int attackTime = 0;
	private int keysIHave = 0;
	private float fallPos = 0;
	private float fallDistance = 0;
	private string equipmentItem = "null";

	//ゲーム起動時
	void Awake (){
		rigidBody = GetComponent<Rigidbody>();
		this.stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		this.startPoint = this.transform.position;
	}

	//ゲーム開始時
	void Start () {
		this.baseHp = this.health;
		this.updateLabelValue(this.getHealth(), this.baseHp);
		foreach(var obj in this.itemIcons){
			if(obj != null){
				obj.SetActive(false);
			}
		}
	}
	
	//毎フレームの処理
	void Update () {
		if(!this.isFreezing && Time.timeScale != 0){

			//HPが0ならゲームオーバー、それ以外ならMovesを実行
			if(this.getHealth() <= 0.0F){
				StartCoroutine(setDead());
				
			}else{
				this.moves();
				this.jump();
				this.attack();
				this.moveCheats();
				this.raysActivity();
			}

			if(this.damageDown > 0){
				--this.damageDown;
			}
		}

		//HPゲージの変化は、フリーズ中や時間停止中も行われる。
		this.updateLabelValue(this.getHealth(), this.baseHp);

		if(this.health > 10.0F){
			this.setHealth(10.0F);
		}
	}

	//ダメージを受けた時
	public void receiveAttack(float damage){
		if(!this.isInvisiblity){
			this.health -= damage;
			this.damageDown = 10;
		}
	}

	//HPを設定
	public void setHealth(float healthValue){
		this.health = healthValue;
	}

	//HPを回復
	public void healing(float amount){
		this.health += amount;
	}

	//Unityちゃんの動き
	private void moves(){
		float distance = 0;
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		plane.SetNormalAndPosition (Vector3.up, transform.localPosition);
		if (plane.Raycast (ray, out distance)) {

			// 距離を元に交点を算出して、交点の方を向く
			var lookPoint = ray.GetPoint(distance);

			if(this.onGround){
				transform.LookAt (lookPoint);
			}
		}

		if(rigidBody.velocity.magnitude < 4.0F){
			//前進
			if(Input.GetKey(KeyCode.W)){

				rigidBody.AddForce(transform.forward * 24.0F);

				if(!Input.GetKey(KeyCode.Space)){
					animator.SetBool("Stay", false);
					animator.SetBool("Dash", true);
				}

			}else{
				animator.SetBool("Dash", false);
				animator.SetBool("Stay", true);
				rigidBody.AddForce(transform.forward * 0.0F);
			}
		}
	}

	//ジャンプ
	private void jump(){
		if(Input.GetKeyDown(KeyCode.Space) && !this.isJump){
			canJump = true;
		}

		//アニメーションとジャンプのタイミングをずらす
		if(this.canJump){
			++this.jumpLagtime;
		}else{
			this.jumpLagtime = 0;
		}

		//アニメーション開始
		if(this.jumpLagtime > 5){
			animator.SetBool ("Jump", true);
			animator.SetBool ("Dash", false);
		}

		//ジャンプ開始
		if(this.jumpLagtime > 7){
			GetComponent<Rigidbody>().velocity = 
				Vector3.up * 9.0F;
			this.canJump = false;
			this.isJump = true;
		}
	}

	/// <summary>
	/// レイを飛ばす
	///	落下ダメージ関連はここで制御
	/// </summary>
	public void raysActivity(){
		float maxDistance = 0.25F;
		/*Debug.DrawRay(transform.position + new Vector3(0F, 0.08F, 0F), 
			Vector3.down * maxDistance, Color.blue, 5, false);*/
		bool rayHit = Physics.Raycast(transform.position + new Vector3(0F, 0.08F, 0F), 
			Vector3.down, out hit, maxDistance);
		if(rayHit){

			if(this.startFall){
				this.fallDistance = fallPos - transform.position.y;
				//print(this.fallDistance);
				
				if(this.fallDistance > 2.0F){
					//this.receiveAttack(this.fallDistance - 8.0F);
					animator.CrossFade("Jump", 0, 0, 0.7f);
				}
				GameObject footParticle = GameObject.Instantiate
					(prefab_footPR, transform.position, transform.rotation) as GameObject;

				this.startFall = false;
			}

		}else{
			if(!this.startFall){
				this.fallDistance = 0;
				this.fallPos = transform.position.y;
				this.startFall = true;
			}
		}
		//Debug.Log(rayHit);
	}

	//攻撃
	private void attack(){
		//"attackTime"が0未満なら、値を10に
		if(Input.GetKeyDown(KeyCode.K) && 
			this.attackTime <= 0){

			this.attackTime = 10;

		//"attackTime"が0以上なら
		}else if(this.attackTime >= 0){
			animator.SetInteger("Kick", this.attackTime);
			--this.attackTime;
		}
	}

	//チートコマンドの管理
	private void moveCheats(){
		if(this.canUseAllowCheats){
			if(Input.GetKeyUp(KeyCode.I) && !this.isInvisiblity){
				this.isInvisiblity = true;
				Debug.Log("UnityChan have been minder");
			}
		}

		//スタート地点に戻る
		if(Input.GetKeyDown(KeyCode.R)){
			transform.position = this.startPoint;
		}
	}

	/*オブジェクトにぶつかったとき（コライダー）
		ジャンプシステムの制御
	*/
	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Ground"){
			if(this.isJump){
				animator.SetBool ("Jump", false);
				this.isJump = false;
			}
		}
	}

	/*オブジェクトにぶつかったとき
		EnemyEntityタグを持つならダメージを付与
		Itemタグを持つなら、そのアイテムを拾う
	*/
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "EnemyEntity"){
			collision.gameObject.SendMessage("receiveAttack", this.attackDamage);

		}else if(collision.gameObject.tag == "Item"){
			this.setEquipment(collision.gameObject.
				GetComponent<EntityAIItem> ().getItemType());
			Destroy(collision.gameObject);
		}
	}

	void OnCollisionStay(Collision collision){
		if(collision.gameObject.tag == "Ground"){
			this.onGround = true;
		}
	}

	void OnCollisionExit(Collision collision){
		this.onGround = false;
	}

	//アイテムを定義する
	public void setEquipment(string weaponType){
		print(weaponType);
		switch(weaponType){

			case "EssencialKey":
			this.keysIHave += 1;
			this.equipmentItem = "EssencialKey";
			this.itemIconScened("EssencialKey");
			break;

			case "HealPotion":
			this.healing(5.0F);
			break;

			default:
			break;
		}
	}

	//アイテムのアイコンを表示させる
	public void itemIconScened(string type){
   		switch(type){
   			case "EssencialKey":   			
   			/*this.itemIcons[0].GetComponent<CanvasRenderer>()
   				.SetAlpha(1);*/
   			this.itemIcons[0].SetActive(true);

   			//カギの個数
   			this.keyText.GetComponent<Text>()
   				.text = this.keysIHave.ToString();

   			break;

   			default:
   			break;
   		}
	}

	//持っているアイテムを取得する
	public string getEquipment(){
		return this.equipmentItem;
	}

	//パーティクル消去
	IEnumerator deleteParticles(int parType){
		yield return new WaitForSeconds(0.3F);

		switch(parType){
			case 1://着地時のパーティクル
			
			//yield return new WaitWhile (() => parCs.IsAlive(true));
			break;

			default:
			break;
		}
	}

	//HPラベルの変化
	private void updateLabelValue(float hp, float full){
		hpLabel.text = hp.ToString("f0") + "/" + full.ToString("f0");
		this.updateGaugeValue(hp);
	}

	//HPゲージの変化
	private void updateGaugeValue(float hp){
		hpGauge.fillAmount = hp / 10.0F;
	}

	//HPがいくつあるかを取得
	public float getHealth(){
		return this.health;
	}

	//Unityちゃんの状態
	public string getEntityState(){
		return this.getHealth() < 7.0F ? "Weakness" : null; //: (this.target != null ? "Attack" : "Idle");
	}

	//地面に足がついているか
	public bool getGround(){
		return this.onGround;
	}

	public int getKeys(){
		return this.keysIHave;
	}

	//ゲームオーバー時
	public IEnumerator setDead(){
		animator.SetBool("Down", true);
		yield return new WaitForSeconds(0.7F);

		Application.LoadLevel("GameOver");
	}

	//バグの管理
	private void bugStaff(string situation){
		switch(situation){

			default:
				Debug.Log("What is some errors you've saw?");
			break;
		}
	}
}