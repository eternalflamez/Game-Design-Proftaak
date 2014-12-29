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
    public int maxTurns = 25;

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
    void Start()
    {
		foods.Add (new Food ("Cola", "", 88, 10));
		foods.Add (new Food ("Melk halfvol", "", 92, 5));
		foods.Add (new Food ("Forel", "", 132, 5));
		foods.Add (new Food ("UNOX Rookworst", "", 340, 5));
		foods.Add (new Food ("Ola Raket", "", 40, 5));
		foods.Add (new Food ("Roomijs", "", 257, 5));
		foods.Add (new Food ("Aardbeien", "", 36, 5));
		foods.Add (new Food ("Banaan", "", 188, 5));
		foods.Add (new Food ("Croissant", "", 239, 5));
		foods.Add (new Food ("Brood bruin", "", 64, 5));

		//food types
		//Cola(1, 88, "1 glas")
		//Melk halfvol(1 beker, 92, "200 ml")
		//Forel(100 gr, 132, "filet bereid")
		//UNOX Rookworst(100 gr, 340, "")
		//Ola Raket(1, 40, "")
		//Roomijs(100 gr, 257, "")
		//Aardbeien(100 gr, 36, "")
		//Banaan(1, 188, "1 banaan")
		//Croissant(50 gr, 239, "1")
		//Brood bruin(25 gr, 64, "1 snee")

        Player player1 = new Player();
        Player player2 = new Player();
        player1.setInfo("Tom", 22, 180, 90.00f, Gender.Male, Color.yellow);
        player1.setPawn(pawns[0]);
        player2.setInfo("Henk", 71, 178, 74.22f, Gender.Male, Color.green);
        player2.setPawn(pawns[1]);

        players.Add(player1);
        players.Add(player2);

        this.maxTurns = (int)InformationManager.instance.getMaxTurns();
        lblPlayerTurn.text = "Player 1: turn " + turnCount;

        ScoreManager.instance.addPlayers(players);
    }

    void Awake()
    {
        instance = this;
    }

	private void spawnNewPawn()
	{
		//todo spawn pawn
	}

	/// <summary>
	/// Rolls the dice for the current player as set by the playerTurn var
	/// </summary>
    public void rollDice()
    {
        if (!ActivePlayer().getPawn().isFinished)
        {
            int diceRoll = Random.Range(1, 6);

            btnDice.interactable = false;

            lblDice.text = "Your rolled: " + diceRoll;

            ActivePlayer().walk(diceRoll * 5);
            ActivePlayer().getPawn().setMovePawn(diceRoll);
        }
        else
        {
            playerEndTurn();
        }
    }

	/// <summary>
	/// Click event for intersect buttons
	/// </summary>
	/// <param name="direction">Direction the player is taking on the intersection.</param>
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

        ActivePlayer().getPawn().setMoveDir(dir);
    }

    public void playerEndTurn()
    {
        Debug.Log("Glucose: " + ActivePlayer().getModel().getGlucose());

        if (turnCount % (maxTurns / 8) == 0)
        {
            ScoreManager.instance.createMeasurePoint(ActivePlayer().getName(), ActivePlayer().getModel().getGlucose());
            // TODO: Laat zien dat het gebeurd is.
        }

        ScoreManager.instance.setScore(ActivePlayer().getName(), ActivePlayer().getModel().getGlucose());

        if (++playerTurn == playerCount)
        {
            playerTurn = 0;
            turnCount++;

            if (turnCount == maxTurns)
            {
				stopGame();
                // TODO: show endscreen
            }
        }

        btnDice.interactable = true;
        lblPlayerTurn.text = "Player " + (playerTurn + 1) + ": turn " + turnCount;
    }

	//remove
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
		btnEat.SetActive (false);
		btnLeave.SetActive (false);
		btnTake.SetActive (false);

        lblInfo.text = "All players have finished.";
    }

    public void showObjectButtons(string type)
    {
        if (type == "food")
        {
            btnEat.SetActive(true);
        }
        else
        {
            btnTake.SetActive(true);
        }

        btnLeave.SetActive(true);
    }

    public void hideObjectButtons()
    {
        btnEat.SetActive(false);
        btnTake.SetActive(false);
        btnLeave.SetActive(false);
    }

    public void clickEat()
    {
        clickFood(1);
    }

    public void clickTake()
    {
        Debug.Log("Insuline before: " + ActivePlayer().insulinReserves);
        players[playerTurn].addInsulinReserves(1);
        Debug.Log("Insuline after: " + ActivePlayer().insulinReserves);
    }

    public void clickLeave()
    {
        hideObjectButtons();
        playerEndTurn();
    }

    public void clickFood(int id)
    {
        players[playerTurn].getModel().eat(foods[id]);
        float newGlucose = players[playerTurn].getModel().getGlucose();

        Debug.Log("New Glucose: " + newGlucose);

        hideObjectButtons();
        playerEndTurn();
    }

    private Player ActivePlayer()
    {
        return this.players[playerTurn];
    }
}