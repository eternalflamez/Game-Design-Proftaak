using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public List<Pawn> players = new List<Pawn>();

	public int playerTurn = 0;
	public int turnCount = 1;
	public int playerCount = 2;

	[SerializeField]
	private Button btnDice;

	[SerializeField]
	private Text lblDice;
	[SerializeField]
	private Text lblPlayerTurn;
	[SerializeField]
	private Text lblInfo;

	// Use this for initialization
	void Start ()
	{
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
		if (!players[playerTurn].isFinished)
		{
			int diceRoll = Random.Range (1, 6);

			btnDice.interactable = false;

			lblDice.text = "Your rolled: " + diceRoll;

			players [playerTurn].setMovePawn (diceRoll);
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

		players [playerTurn].setMoveDir (dir);
	}

	public void playerEndTurn()
	{
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

		for (int index = 0; index < players.Count; index++)
		{
			if (!players[index].isFinished)
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
}