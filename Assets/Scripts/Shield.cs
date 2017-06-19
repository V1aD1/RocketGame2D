using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col){

		if (col.gameObject.name.Contains ("ocket") || col.gameObject.name.Contains ("issile")) {
			Debug.Log("Shield was hit by rocket!");
			Destroy (this.gameObject);
		}
	}
}
