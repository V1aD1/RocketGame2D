using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public Transform player;

	Text text;
	float hp;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		hp = player.GetComponent<Player>().hp;
		text.color = player.GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
		hp = player.GetComponent<Player>().hp;
		text.text = hp.ToString();

	}
}
