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
	/// The food the player has selected
	/// </summary>
	public int selectedFood;

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
	private Button btnUp;
	[SerializeField]
	private Button btnDown;
	[SerializeField]
	private Button btnLeft;
	[SerializeField]
	private Button btnRight;

    [SerializeField]
    private Text lblDice;
    [SerializeField]
    private Text lblPlayerTurn;
    [SerializeField]
    private Text lblInfo;
	[SerializeField]
	private Text lblGlucose;
	[SerializeField]
	private Text lblInsuline;
	[SerializeField]
	private Text lblFoodName;
	[SerializeField]
	private Text lblFoodDesciption;
	[SerializeField]
	private Text lblFoodCal;

	[SerializeField]
	private GameObject pnlFood;

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

		hideRouteButtons ();
		hideFoodPnl ();

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

        ScoreManager.instance.addPlayers(players);

        playerCount = players.Count;

		lblPlayerTurn.text = "Player " + playerTurn + ": turn " + turnCount;
		lblGlucose.text = "Bloedsuiker: " + players[0].getModel().getGlucose();
		lblInsuline.text = "Insuline: " + players[0].getInsulineReserve();
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
		lblGlucose.text = "Bloedsuiker: " + ActivePlayer().getModel().getGlucose();
		lblInsuline.text = "Insuline: " + ActivePlayer().getInsulineReserve();

		setCameraPosition ();
    }

	/// <summary>
	/// Sets the camera position to the position of the active player.
	/// </summary>
	private void setCameraPosition()
	{
		Pawn playerPawn = (Pawn)ActivePlayer ().getPawn ();
		Vector3 newPosition = playerPawn.gameObject.transform.position;
		newPosition.z = -10;

		Camera.main.transform.position = newPosition;
	}

	/// <summary>
	/// Shows the route buttons.
	/// </summary>
	public void showRouteButtons(bool up, bool down, bool right, bool left)
	{
		btnUp.interactable = up;
		btnDown.interactable = down;
		btnRight.interactable = right;
		btnLeft.interactable = left;
	}

	/// <summary>
	/// Hides the route buttons.
	/// </summary>
	public void hideRouteButtons()
	{
		btnUp.interactable = false;
		btnDown.interactable = false;
		btnRight.interactable = false;
		btnLeft.interactable = false;
	}

	/// <summary>
	/// Hides the food pnl.
	/// </summary>
	public void hideFoodPnl ()
	{
		Debug.Log ("Hide");
		pnlFood.SetActive (false);
	}

	/// <summary>
	/// Shows the food pnl.
	/// </summary>
	public void showFoodPnl()
	{
		pnlFood.SetActive (true);
	}

    /// <summary>
    /// Shows the UI required to pick up items.
    /// </summary>
    /// <param name="type"></param>
    public void showObjectButtons(string type)
    {
        if (type == "food")
        {
			pnlFood.SetActive(true);
            //btnEat.SetActive(true);
        }
        else
        {
            btnTake.SetActive(true);
			btnLeave.SetActive(true);
        }
    }

    /// <summary>
    /// Hides the UI required to pick up items.
    /// </summary>
    public void hideObjectButtons()
    {
        btnEat.SetActive(false);
        btnTake.SetActive(false);
        btnLeave.SetActive(false);

		hideFoodPnl ();
    }

    /// <summary>
    /// The click event to eat a certain piece of food.
    /// </summary>
    public void clickEat()
    {
		clickFood(selectedFood);
		hideFoodPnl ();
    }

    /// <summary>
    /// The click event to take insulin with you.
    /// </summary>
    public void clickTake()
    {
        Debug.Log("Insuline before: " + ActivePlayer().getInsulineReserve());
        ActivePlayer().addInsulinReserves(1);
        Debug.Log("Insuline after: " + ActivePlayer().getInsulineReserve());
		hideObjectButtons();
		playerEndTurn();
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
	/// Shows the food info.
	/// </summary>
	public void showFoodInfo(int foodId)
	{
		selectedFood = foodId;

		lblFoodName.text = foods [foodId].getName ();
		lblFoodDesciption.text = foods [foodId].getDesciption ();
		lblFoodCal.text = foods [foodId].getCarbs ().ToString();
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