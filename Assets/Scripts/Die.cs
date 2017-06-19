using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour {

	public float timeToLive = 0.4f;

	float timePassed = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		while (timePassed < timeToLive) {
			timePassed = timePassed + Time.deltaTime;
		}

		Destroy (this.gameObject);
	}
}
