using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private InformationManager informationManager;
    public static GameManager instance;

    /// <summary>
    /// The list of pawn gameobjects.
    /// </summary>
    public List<Pawn> pawns = new List<Pawn>();

    /// <summary>
    /// The list of players. This list is generated from the information filled in at the start of the game.
    /// </summary>
    private List<Player> players = new List<Player>();

    /// <summary>
    /// A static list of food, added to at the beginning of the game.
    /// </summary>
    public List<Food> foods = new List<Food>();

	[SerializeField]
	private GameObject pawnObject;

    /// <summary>
    /// The position of the player whose turn it currently is, in the list.
    /// </summary>
    private int playerTurn;

    /// <summary>
    /// The current turn number.
    /// </summary>
    private int turnCount = 1;

    /// <summary>
    /// The amount of players we have.
    /// </summary>
    private int playerCount;

    /// <summary>
    /// The amount of turns this game is going to run.
    /// </summary>
    private int maxTurns;

    public int getMaxTurns()
    {
        return maxTurns;
    }

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
        informationManager = InformationManager.instance;
        if (informationManager == null)
        {
            GameObject InformationObject = (GameObject)Instantiate(new GameObject("InformationManagerObject"));
            InformationObject.AddComponent<InformationManager>();

            informationManager = InformationManager.instance;
            informationManager.addPlayer("Tom", 22, 180, 90.00f, Gender.Male, new PawnColor(Color.yellow, "Yellow"));
            informationManager.addPlayer("Henk", 71, 178, 74.22f, Gender.Male, new PawnColor(Color.green, "Green"));
        }

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

        players.AddRange(informationManager.getPlayers());
        
        int lowest = int.MaxValue;
        int number = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].getAge() < lowest)
            {
                lowest = players[i].getAge();
                number = i;
            }
        }

        playerTurn = number;

		Tile startTile = BoardController.instance.getStartTile ();

		Debug.Log ("StartTileCoords: " + startTile.getCoordinates());

		for (int index = 0; index < players.Count; index++)
		{
			GameObject newPawn = (GameObject)Instantiate (pawnObject, startTile.transform.position, Quaternion.identity);
			Pawn newPawnController = (Pawn)newPawn.GetComponent("Pawn");
			newPawnController.setCurrentTile(startTile);

			players[index].setPawn(newPawnController);

			startTile.addPawnToTile(newPawnController);
		}

        this.maxTurns = (int)informationManager.getMaxTurns();
        lblPlayerTurn.text = "Player " + playerTurn + ": turn " + turnCount;

        ScoreManager.instance.addPlayers(players);

        playerCount = players.Count;
    }

    void Awake()
    {
        instance = this;
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

    /// <summary>
    /// Ends the turn of the current player, saves scores when needed and gives the next player the turn.
    /// </summary>
    public void playerEndTurn()
    {
        Debug.Log("Glucose: " + ActivePlayer().getModel().getGlucose());

        if (turnCount % (maxTurns / 8) == 0)
        {
            ScoreManager.instance.createMeasurePoint(ActivePlayer().getId(), ActivePlayer().getModel().getGlucose());
            // TODO: Laat zien dat het gebeurd is.
        }

        ScoreManager.instance.setScore(ActivePlayer().getId(), ActivePlayer().getModel().getGlucose());

        if (++playerTurn == playerCount)
        {
            playerTurn = 0;
            turnCount++;

            if (turnCount == maxTurns)
            {
                informationManager.SaveScores(ScoreManager.instance);
                Application.LoadLevel("EndScreen");
            }
        }

        btnDice.interactable = true;
        lblPlayerTurn.text = "Player " + (playerTurn + 1) + ": turn " + turnCount;
    }

    /// <summary>
    /// Shows the UI required to pick up items.
    /// </summary>
    /// <param name="type"></param>
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

    /// <summary>
    /// Hides the UI required to pick up items.
    /// </summary>
    public void hideObjectButtons()
    {
        btnEat.SetActive(false);
        btnTake.SetActive(false);
        btnLeave.SetActive(false);
    }

    /// <summary>
    /// The click event to eat a certain piece of food.
    /// </summary>
    public void clickEat()
    {
        clickFood(1);
    }

    /// <summary>
    /// The click event to take insulin with you.
    /// </summary>
    public void clickTake()
    {
        Debug.Log("Insuline before: " + ActivePlayer().insulinReserves);
        ActivePlayer().addInsulinReserves(1);
        Debug.Log("Insuline after: " + ActivePlayer().insulinReserves);
    }

    /// <summary>
    /// The click event to leave a certain item.
    /// </summary>
    public void clickLeave()
    {
        hideObjectButtons();
        playerEndTurn();
    }

    /// <summary>
    /// The click event to eat a piece of food.
    /// </summary>
    /// <param name="id">The id of the food to eat.</param>
    public void clickFood(int id)
    {
        ActivePlayer().getModel().eat(foods[id]);
        float newGlucose = ActivePlayer().getModel().getGlucose();

        Debug.Log("New Glucose: " + newGlucose);

        hideObjectButtons();
        playerEndTurn();
    }

    /// <summary>
    /// Gets the active player.
    /// </summary>
    /// <returns>The player whose turn it is right now.</returns>
    private Player ActivePlayer()
    {
        return this.players[playerTurn];
    }
}