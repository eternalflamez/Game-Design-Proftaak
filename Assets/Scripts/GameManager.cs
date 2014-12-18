using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public List<Pawn> pawns = new List<Pawn>();
	public List<Player> players = new List<Player>();
	public List<Food> foods = new List<Food>();

	public int playerTurn = 0;
	public int turnCount = 1;
	public int playerCount = 2;

	[SerializeField]
	private Button btnDice;
	[SerializeField]
	private GameObject btnEat;
	[SerializeField]
	private GameObject btnLeave;
	[SerializeField]
	private GameObject btnTake;

	[SerializeField]
	private Text lblDice;
	[SerializeField]
	private Text lblPlayerTurn;
	[SerializeField]
	private Text lblInfo;

	// Use this for initialization
	void Start ()
	{
		foods.Add (new Food ("Pizza", 100, 10));
		foods.Add (new Food ("Cola", 10, 5));

		Player player1 = new Player ();
		Player player2 = new Player();
		player1.setInfo ("Tom", 22, 180, 90.00, "man");
		player2.setInfo ("Henk", 71, 178, 74.22, "man");

		players.Add (player1);
		players.Add (player2);

		lblPlayerTurn.text = "Player 1: turn " + turnCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake()
	{
		instance = this;
	}

	public void rollDice()
	{
		if (!pawns[playerTurn].isFinished)
		{
			int diceRoll = Random.Range (1, 6);

			btnDice.interactable = false;

			lblDice.text = "Your rolled: " + diceRoll;

			players[playerTurn].walk(diceRoll * 5);
			pawns [playerTurn].setMovePawn (diceRoll);
		}
		else
		{
			playerEndTurn();
		}
	}

	public void clickRoute(string direction)
	{
		Direction dir = Direction.Right;

		switch (direction)
		{
		case "up":
			dir = Direction.Up;
			break;
		case "down":
			dir = Direction.Down;
			break;
		case "right":
			dir = Direction.Right;
			break;
		case "left":
			dir = Direction.Left;
			break;
		}

		pawns [playerTurn].setMoveDir (dir);
	}

	public void playerEndTurn()
	{
		Debug.Log ("Glucose: " + players[playerTurn].model.getGlucose());

		playerTurn++;
		
		if (playerTurn == playerCount)
		{
			playerTurn = 0;
			turnCount++;
		}
		
		btnDice.interactable = true;
		lblPlayerTurn.text = "Player " + (playerTurn + 1) + ": turn " + turnCount;
	}
	public void playerFinish(int playerID)
	{
		lblInfo.text = "Player " + playerID + " has finished.";

		bool allPlayersFinished = true;

		for (int index = 0; index < pawns.Count; index++)
		{
			if (!pawns[index].isFinished)
			{
				allPlayersFinished = false;
				break;
			}
		}

		if (allPlayersFinished)
		{
			stopGame();
		}
	}
	private void stopGame()
	{
		btnDice.interactable = false;

		lblInfo.text = "All players have finished.";
	}

	public void showObjectButtons(string type)
	{
		if (type == "food")
		{
			btnEat.SetActive (true);
		}
		else
		{
			btnTake.SetActive(true);
		}

		btnLeave.SetActive (true);
	}
	public void hideObjectButtons ()
	{
		btnEat.SetActive (false);
		btnTake.SetActive (false);
		btnLeave.SetActive (false);
	}

	public void clickEat()
	{
		clickFood (1);
	}
	public void clickTake()
	{
		Debug.Log ("Insuline before: " + players[playerTurn].insulinReserves);
		players [playerTurn].addInsulinReserves (1);
		Debug.Log ("Insuline after: " + players[playerTurn].insulinReserves);
	}
	public void clickLeave()
	{
		hideObjectButtons ();
		playerEndTurn();
	}
	public void clickFood(int id)
	{
		players [playerTurn].model.eat (foods [id]);
		float newGlucose = players [playerTurn].model.getGlucose ();

		Debug.Log ("New Glucose: " + newGlucose);

		hideObjectButtons ();
		playerEndTurn ();
	}
}