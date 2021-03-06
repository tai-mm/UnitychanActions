using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// -Unityちゃんの動き(移動・ジャンプ・アニメーション)
/// -Unityちゃんの装備に関すること
/// -UIに関すること
/// -バグ修正機能
/// </summary>
public class UnityChanMoves : MonoBehaviour {
	public Animator animator;//Unityちゃんのアニメーターコントローラー
	public Rigidbody rigidBody;//リジッドボディ
	public RaycastHit hit;//カメラレイ
	public GameObject[] itemIcons = new GameObject[3];
	public GameObject keyText;
	public GameObject prefab_footPR;
	protected bool isJump = false; 
	protected bool onGround = false;
	protected float speedKeeper;
	protected float moveSpeed = 12.0F;
	protected float strongLeg = 3.0F;
	//protected byte[] entityEffects = new byte[];
	private Plane plane = new Plane();
	private AnimatorStateInfo stateInfo;
	private bool canJump = false;
	private bool startFall = false;
	private int jumpLagtime = 0;
	private int deadTime = 0;
	private int attackTime = 0;
	private float fallPos = 0;
	private float fallDistance = 0;
	private string equipmentItem = "null";
	[SerializeField] Vector3 raysAt;

	//プレイヤーに付随する他のスクリプト
	private UnityChanStatus plaCsMain;
	private UnityChanRay plaCsRay;
	private UnityChanUI plaCsUI;

	//ゲーム起動時
	void Awake (){
		this.plaCsMain = GetComponent<UnityChanStatus>();
		this.rigidBody = GetComponent<Rigidbody>();
		this.stateInfo = animator.GetCurrentAnimatorStateInfo(0);
	}

	//ゲーム開始時
	void Start () {
		//アイテムアイコンを非アクティブに
		foreach(var obj in this.itemIcons){
			if(obj != null){
				obj.SetActive(false);
			}
		}
	}
	
	//毎フレームの処理
	void Update () {
		var baseCs = this.gameObject.GetComponent<UnityChanStatus>();
		if(!baseCs.isFreezing && Time.timeScale != 0){

			if(baseCs.getHealth() > 0.0F){
				this.moves();
				this.attack();
				this.raysActivity();
				if(Input.GetKeyDown(KeyCode.Space)){
					StartCoroutine(this.jump(9.0f));
				}
			}
		}
	}

	//Unityちゃんの動き
	private void moves(){
		float distance = 10.0f;
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		 //Debug.DrawRay (ray.origin, ray.direction * distance, Color.red, 3600f, false);

		plane.SetNormalAndPosition (Vector3.up, transform.position);
		if (plane.Raycast (ray, out distance)) {

			// 距離を元に交点を算出して、交点の方を向く
			var lookPoint = ray.GetPoint(distance);
			lookPoint.y = transform.position.y;

			if(this.onGround){
				transform.LookAt(lookPoint);
				this.raysAt = lookPoint;
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
	public IEnumerator jump(float leapLevel){
		if(this.isJump || this.canJump){
			yield break;
		}else if(!this.isJump){
			canJump = true;
		}

		//アニメーションとジャンプのタイミングをずらす
		yield return new WaitForSeconds(0.1f);

		//アニメーション開始
		animator.SetBool ("Jump", true);
		animator.SetBool ("Dash", false);

		yield return new WaitForSeconds(0.1f);

		//ジャンプ開始
		GetComponent<Rigidbody>().velocity = Vector3.up * leapLevel;
		this.canJump = false;
		this.isJump = true;
	}

	//ジャンプに関するレイ
	public void raysActivity(){
		float maxDistance = 0.25F;
		/*Debug.DrawRay(transform.position + new Vector3(0F, 0.08F, 0F), 
			Vector3.down * maxDistance, Color.blue, 5, false);*/
		bool rayHit = Physics.Raycast(transform.position + new Vector3(0F, 0.08F, 0F), 
			Vector3.down, out hit, maxDistance);
		if(rayHit){

			if(this.startFall){
				this.fallDistance = fallPos - transform.position.y;
				
				if(this.fallDistance > 2.0F){
					//this.receiveAttack(this.fallDistance - 8.0F);
					animator.CrossFade("Jump", 0, 0, 0.7f);
				}
				/*GameObject footParticle = GameObject.Instantiate
					(prefab_footPR, transform.position, transform.rotation) as GameObject;
				*/
				this.startFall = false;
			}

		}else{
			if(!this.startFall){
				this.fallDistance = 0;
				this.fallPos = transform.position.y;
				this.startFall = true;
			}
		}
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
			collision.gameObject.SendMessage("receiveAttack", this.plaCsMain.attackDamage);

		}else if(collision.gameObject.tag == "Item"){
			this.setEquipment(collision.gameObject.
				GetComponent<ItemMoves> ().getItemType());
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
		switch(weaponType){

			/*case "EssencialKey":
			this.keysIHave += 1;
			this.equipmentItem = "EssencialKey";
			StartCoroutine(this.itemIconScened("EssencialKey"));
			break;*/

			case "HealPotion":
			this.plaCsMain.healing(5.0F);
			break;

			default:
			break;
		}
	}

	//アイテムのアイコンを表示させる
	public IEnumerator itemIconScened(string type){
		yield return new WaitForSeconds(0.5f);

   		switch(type){
   			/*case "EssencialKey":
   			this.itemIcons[0].SetActive(true);

   			//カギの個数
   			this.keyText.GetComponent<Text>()
   				.text = this.keysIHave.ToString();

   			break;*/

   			default:
   			break;
   		}
   		yield break;
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

	//地面に足がついているか
	public bool getGround(){
		return this.onGround;
	}

	//ゲームオーバー時
	public IEnumerator setDead(){
		animator.SetBool("Down", true);
		yield return new WaitForSeconds(0.7F);

		//Application.LoadLevel("GameOver");
		SceneManager.LoadScene("GameOver");
	}
}