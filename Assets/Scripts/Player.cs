using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class assumes player character is a child of the grid object
public class Player : MonoBehaviour {

	public int playerNum=1;
	public Transform shield;
	public Transform rocket;

	public float speed = 1;

	public int gridXPos = 0;
	public int gridYPos = 0;

	public float turnTime = 3;
	public int numTurns = 3;
	public float completeMoveTime = 0.5f;

	Grid grid;
	float timeUntilMoveFinishes = 0f;
	float cellLength;


	public float hp = 100;
	public bool listening = false;
	Vector3 currentDir = Vector3.up;

	void Awake(){
		EventManager.Instance.NewDir += NewDir;
	}


	// Use this for initialization
	void Start () {
		print (transform.name);
		print (transform.parent.name);
		grid = transform.GetComponentInParent<Grid> ();
		cellLength = grid.cellLength;
	}


	void OnTriggerEnter (Collider col){

		if (col.gameObject.name.Contains ("ocket")) {
			this.hp -= col.gameObject.GetComponent<Rocket>().damage;
		}
	}

	//particleSystem is the particle system whose particles cause the collision
	void OnParticleCollision(GameObject other){
		ParticleSystem part = other.GetComponent<ParticleSystem>();
		
		ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[100];
		int numParticles = ParticlePhysicsExtensions.GetCollisionEvents (part, gameObject, collisionEvents);
		
		this.hp -= numParticles;
		
	}


	void OnEnable(){
		EventManager.onMove += onMove;
		EventManager.onShield += onShield;
		EventManager.onRocket += onRocket;
	}

	void OnDisable()
	{
		EventManager.onMove -= onMove;
		EventManager.onShield -= onShield;
		EventManager.onRocket -= onRocket;
	}

	void NewDir(Vector3 dir){
		print ("Player" + playerNum + " changed direction to "+dir);
		currentDir = dir;
	}

	void onMove ()
	{
		if (this.listening == true) {
			print ("Player " + playerNum + " is moving");
			StartCoroutine (Move (currentDir * cellLength));
			timeUntilMoveFinishes = completeMoveTime;
		}
	}

	void onShield ()
	{
		if (this.listening == true) {
			print ("Player " + playerNum + " placed a shield");
			StartCoroutine (PlaceShield (currentDir));
			timeUntilMoveFinishes = completeMoveTime;
		}
	}

	void onRocket ()
	{
		if (this.listening == true) {
			print ("Player " + playerNum + " fired a rocket");
			StartCoroutine (ShootRocket (currentDir));
			timeUntilMoveFinishes = completeMoveTime;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (this.hp <= 0) {
			this.hp = 0;
			print ("Player " + playerNum + " was destroyed!!");
			this.gameObject.SetActive (false);
		}
			

		if(timeUntilMoveFinishes > 0f){
			timeUntilMoveFinishes -= Time.deltaTime;
		}
	}

	IEnumerator Move(Vector3 moveBy){
		if(currentDir == Vector3.up)
			gridYPos++;
		else if(currentDir == Vector3.down)
			gridYPos--;
		else if(currentDir == Vector3.right)
			gridXPos++;
		else if(currentDir == Vector3.left)
			gridXPos--;

		Vector3 origPos = transform.localPosition;
		float timePassed = 0f;

		while(timePassed<completeMoveTime){
			transform.localPosition = Vector3.Lerp (transform.localPosition, origPos + moveBy, timePassed / completeMoveTime);
			timePassed += Time.deltaTime;
			yield return null;
		}

		yield return null;
	}

	IEnumerator PlaceShield(Vector3 dir){

		yield return new WaitForSeconds(0.2f);

		//0 = up, 1= right, 2 = down, 3 = left
		float timePassed = 0f;

		//spawn the shield according to the specified direction
		Quaternion shieldRot = Quaternion.identity;

		if (dir == Vector3.up)
			shieldRot = Quaternion.Euler (0, 0, 90);
		else if (dir == Vector3.right)
			shieldRot = Quaternion.identity;
		else if (dir == Vector3.down)
			shieldRot = Quaternion.Euler(0,0,-90);
		else if (dir == Vector3.left)
			shieldRot = Quaternion.Euler(0,0,-180);

		Transform shieldClone = (Transform) Instantiate (shield, transform.position + dir * (cellLength / 2), shieldRot);
		shieldClone.SetParent (transform.parent);

		yield return null;
	}

	IEnumerator ShootRocket(Vector3 dir){

		yield return new WaitForSeconds(0.2f);

		//0 = up, 1= right, 2 = down, 3 = left
		float timePassed = 0f;

		Transform rocketClone = (Transform) Instantiate (rocket, transform.position + dir * (cellLength / 2), Quaternion.Euler (0, 0, 0));
		Vector3 rot = Vector3.one;
		if (dir == Vector3.right)
			rot = new Vector3 (0, 0, -90);
		else if (dir == Vector3.down)
			rot = new Vector3 (0, 0, 180);
		else if (dir == Vector3.left)
			rot = new Vector3 (0, 0, 90);

		
		rocketClone.Rotate (rot);

		rocketClone.GetComponent<Rocket> ().setDirection (dir);
		rocketClone.GetComponent<Rocket> ().setCompleteMoveTime (completeMoveTime);
		rocketClone.SetParent (transform.parent);

		while(timePassed<completeMoveTime){
			timePassed += Time.deltaTime;
			yield return null;
		}

		yield return null;
	}

	public void setGridXPos(int x)
	{
		gridXPos = x;
	}

	public void setGridYPos(int y)
	{
		gridYPos = y;
	}

	public void setListening(bool val)
	{
		listening = val;
	}

	public float getTimeUntilMoveFinishes(){
		return timeUntilMoveFinishes;
	}

}
