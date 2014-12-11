using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public List<Pawn> players = new List<Pawn>();

	public int playerTurn = 0;

	public Button btnUp;
	public Button btnDown;
	public Button btnRight;
	public Button btnLeft;

	// Use this for initialization
	void Start ()
	{
		btnUp.interactable = false;
		btnDown.interactable = false;
		btnRight.interactable = false;
		btnLeft.interactable = false;
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
		int diceRoll = Random.Range (1, 6);

		players [playerTurn].setMovePawn (diceRoll);
	}

	public void clickRoute(string direction)
	{
		Debug.Log ("clickRoute: " + direction);
		players [playerTurn].setMoveDir (direction);
	}

	public void playerFinish(int playerID)
	{

	}

	public void enableButtons(bool up, bool down, bool right, bool left)
	{
        btnUp.interactable = up;
        btnDown.interactable = down;
        btnRight.interactable = right;
        btnLeft.interactable = left;
	}
}
