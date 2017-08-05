using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public int speed = 2;
	public float damage = 25f;
	public Transform explosionEffect;


	Grid grid;
	float cellLength;
	float completeMoveTime = 0.5f;
	Vector3 direction;

	Transform explosionEffectClone;

	void OnEnable(){
		EventManager.onTurn += MoveForward;
	}

	void OnDisable()
	{
		EventManager.onTurn -= MoveForward;
	}

	//rocket gets destroyed no matter what it comes into contact with
	void OnTriggerEnter (){
		Debug.Log("Rocket destroyed!");

		explosionEffectClone = (Transform) Instantiate (explosionEffect, transform.position, Quaternion.Euler(0,0,0));
		
		//this rotation determines the direction to apply the explosion in
		Vector3 rot = new Vector3(-1*transform.eulerAngles.z + 90, 90, 0);

		explosionEffectClone.transform.Rotate (rot);

		print ("created the explosion!!");
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		grid = transform.GetComponentInParent<Grid> ();
		cellLength = grid.cellLength;
	}

	void MoveForward(){
		StartCoroutine (MoveForwardCoroutine ());
	}

	IEnumerator MoveForwardCoroutine(){
		Vector3 origPos = transform.localPosition;
		float timePassed = 0f;

		while(timePassed<completeMoveTime){
			transform.localPosition = Vector3.Lerp (transform.localPosition, origPos + direction*speed*cellLength, timePassed / completeMoveTime);
			timePassed += Time.deltaTime;
			yield return null;
		}

		yield return null;
	}

	public void setDirection(Vector3 dir){
		direction = dir;
	}

	public void setCompleteMoveTime(float time){
		completeMoveTime = time;
	}
}
