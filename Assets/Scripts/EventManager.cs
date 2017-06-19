using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour 
{
	//Singleton
	private static EventManager instance;

	//Construct
	private EventManager() {}

	public static EventManager Instance{
		get{
			if (instance == null)
				instance = GameObject.FindObjectOfType (typeof(EventManager)) as EventManager;

			return instance;
		}

	}

	public GameObject grid;
	public GameObject player1;
	public GameObject player2;

	public delegate void Turn();
	public static event Turn onTurn;
	public static event Turn onMove;
	public static event Turn onShield;
	public static event Turn onRocket;

	public delegate void Dir(Vector3 dir);
	public event Dir NewDir;

	int P1TurnCount = 0;
	int turnIncrease = 1;


	//player 1 always starts
	bool player1Turn = true;

	int gridHeight = 0;
	int gridWidth = 0;


	void Start(){
		player1.GetComponent<Player> ().setListening (player1Turn);
		player2.GetComponent<Player> ().setListening (!player1Turn);

		gridHeight = grid.GetComponent<Grid> ().height;
		gridWidth  = grid.GetComponent<Grid> ().length; 
	}

	void FixedUpdate()
	{

		//every player gets 3 turns ehhh
		if (P1TurnCount == 3) {
			turnIncrease = -1;
			player1Turn = false;
			player1.GetComponent<Player> ().setListening (player1Turn);
			player2.GetComponent<Player> ().setListening (!player1Turn);
		} else if (P1TurnCount == 0) {
			turnIncrease = 1;
			player1Turn = true;
			player1.GetComponent<Player> ().setListening (player1Turn);
			player2.GetComponent<Player> ().setListening (!player1Turn);
		}

		if(player1Turn == true && player1.GetComponent<Player>().getTimeUntilMoveFinishes() <= 0f){

			if (Input.GetAxis ("1_Vertical") > 0 && player1.GetComponent<Player> ().gridYPos < gridHeight) {
				if (NewDir != null)
					NewDir (Vector3.up);
			}

			else if (Input.GetAxis ("1_Vertical") < 0 && player1.GetComponent<Player> ().gridYPos > 0) {
				if (NewDir != null)
					NewDir (Vector3.down);
			}

			else if (Input.GetAxis ("1_Horizontal") > 0 && player1.GetComponent<Player> ().gridXPos < gridWidth) {
				if (NewDir != null)
					NewDir (Vector3.right);
			}

			else if (Input.GetAxis ("1_Horizontal") < 0 && player1.GetComponent<Player> ().gridXPos > 0) {
				if (NewDir != null)
					NewDir (Vector3.left);
			}


			if (Input.GetButtonDown ("1_Move")) {
				if (onTurn != null)
					onTurn ();

				if (onMove != null)
					onMove ();

				P1TurnCount += turnIncrease;

			} else if (Input.GetButtonDown ("1_Shield")) {
				if (onTurn != null)
					onTurn ();

				if (onShield != null)
					onShield ();

				P1TurnCount += turnIncrease;
				
			} else if(Input.GetButtonDown ("1_Rocket")) {
			
				if (onTurn != null)
					onTurn ();

				if (onRocket != null)
					onRocket ();

				P1TurnCount += turnIncrease;
	
			}
		}

		else if(player1Turn == false && player2.GetComponent<Player>().getTimeUntilMoveFinishes() <= 0f){

			if (Input.GetAxis ("2_Vertical") > 0 && player2.GetComponent<Player> ().gridYPos < gridHeight) {
				if (NewDir != null)
					NewDir (Vector3.up);
			}

			else if (Input.GetAxis ("2_Vertical") < 0 && player2.GetComponent<Player> ().gridYPos > 0) {
				if (NewDir != null)
					NewDir (Vector3.down);
			}

			else if (Input.GetAxis ("2_Horizontal") > 0 && player2.GetComponent<Player> ().gridXPos < gridWidth) {
				if (NewDir != null)
					NewDir (Vector3.right);
			}

			else if (Input.GetAxis ("2_Horizontal") < 0 && player2.GetComponent<Player> ().gridXPos > 0) {
				if (NewDir != null)
					NewDir (Vector3.left);
			}

			if (Input.GetButtonDown ("2_Move")) {

				if (onTurn != null)
					onTurn ();

				if (onMove != null)
					onMove ();

				P1TurnCount += turnIncrease;

			} else if (Input.GetButtonDown ("2_Shield")) {

				if (onTurn != null)
					onTurn ();

				if (onShield != null)
					onShield ();

				P1TurnCount += turnIncrease;

			} else if(Input.GetButtonDown ("2_Rocket")) {

				if (onTurn != null)
					onTurn ();

				if (onRocket != null)
					onRocket ();

				P1TurnCount += turnIncrease;
			}
		}
	}
}