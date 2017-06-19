using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public int length = 10;
	public int height = 10;

	public Vector2 Player1Pos = Vector2.zero;
	public Vector2 Player2Pos = Vector2.zero;

	public GameObject player1;
	public GameObject player2;

	//it's assumed the cell will always be square
	public float cellLength = 2;

	// Use this for initialization
	void Start () {

		//player 1 
		player1.transform.parent = transform;

		player1.GetComponent<Player> ().setGridXPos (Mathf.FloorToInt(Player1Pos.x));
		player1.GetComponent<Player> ().setGridYPos (Mathf.FloorToInt(Player1Pos.y));

		player1.transform.localPosition = new Vector3 (Mathf.FloorToInt(Player1Pos.x) * cellLength, Mathf.FloorToInt(Player1Pos.y) * cellLength, 0);

		//player 2
		player2.transform.parent = transform;

		player2.GetComponent<Player> ().setGridXPos (Mathf.FloorToInt(Player2Pos.x));
		player2.GetComponent<Player> ().setGridYPos (Mathf.FloorToInt(Player2Pos.y));

		player2.transform.localPosition = new Vector3 (Mathf.FloorToInt(Player2Pos.x) * cellLength, Mathf.FloorToInt(Player2Pos.y) * cellLength, 0);
	}

	void OnDrawGizmosSelected(){
		for (int i = 0; i < length+1; i++) {

			Gizmos.color = Color.green;

			//up down lines
			Gizmos.DrawLine (transform.localPosition + Vector3.right * i * cellLength, 
				(transform.localPosition + Vector3.right * i * cellLength) + Vector3.up * (cellLength * height));

			//left right lines
			Gizmos.DrawLine (transform.localPosition + Vector3.up * i * cellLength,
				(transform.localPosition + Vector3.up * i * cellLength) + Vector3.right * (cellLength * length));
		}
	}
}
