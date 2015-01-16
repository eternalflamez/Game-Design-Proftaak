using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] dices;
    private InformationManager informationManager;
    public static GameManager instance;

	[SerializeField]
	private Image HUDBackground;

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
	private AudioSource audioSource;

    [SerializeField]
    private Button btnDice;

	[SerializeField]
	private Button btnUp;
	[SerializeField]
	private Button btnDown;
	[SerializeField]
	private Button btnLeft;
	[SerializeField]
	private Button btnRight;

    [SerializeField]
    private Text lblPlayerTurn;
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
	private Text lblSound;

	[SerializeField]
	private GameObject pnlFood;
	[SerializeField]
	private GameObject pnlMenu;

    // Use this for initialization
    void Start()
    {
        // TODO: Maybe remove? Hotfix for testing without filling in info.
        informationManager = InformationManager.instance;
        if (informationManager == null)
        {
            GameObject InformationObject = (GameObject)Instantiate(new GameObject("InformationManagerObject"));
            InformationObject.AddComponent<InformationManager>();

            informationManager = InformationManager.instance;
            informationManager.addPlayer("Tom", 22, 180, 90.00f, Gender.Male, new PawnColor(Color.yellow, "Yellow"), false);
            informationManager.addPlayer("Henk", 71, 178, 74.22f, Gender.Male, new PawnColor(Color.green, "Green"), false);
        }

		hideRouteButtons ();
		hideFoodPnl ();

		foods.Add (new Food ("Cola", "Een glas Cola", 88, 10));
		foods.Add (new Food ("Melk halfvol", "Een glas halfvolle melk", 92, 5));
		foods.Add (new Food ("Forel", "Een gebraden Forel Filet", 132, 5));
		foods.Add (new Food ("UNOX Rookworst", "Een portie UNOX Rookworst", 340, 5));
		foods.Add (new Food ("Ola Raket", "Een Ola Raket waterijs", 40, 5));
		foods.Add (new Food ("Roomijs", "Een portie ijs", 257, 5));
		foods.Add (new Food ("Aardbeien", "100 gram aardbeien", 36, 5));
		foods.Add (new Food ("Banaan", "1 banaan", 188, 5));
		foods.Add (new Food ("Croissant", "1 Croissant", 239, 5));
		foods.Add (new Food ("Brood bruin", "1 snee belegd brood", 64, 5));
		foods.Add (new Food ("Druivensuiker", "Nodig voor Hypo", 400, 5));

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

		lblPlayerTurn.text = "Speler " + ActivePlayer().getName() + System.Environment.NewLine + "Beurt " + turnCount;
		lblGlucose.text = "Bloedsuiker: " + players[0].getModel().getGlucose();
		lblInsuline.text = "Insuline: " + players[0].getInsulineReserve();

		HUDBackground.color = ActivePlayer ().getPawn ().getColor ();
    }

    void Awake()
    {
        instance = this;
        Reset();
    }

    void Reset()
    {
        foreach (GameObject item in dices)
        {
            item.SetActive(false);
        }
    }

	/// <summary>
	/// Rolls the dice for the current player as set by the playerTurn var
	/// </summary>
    public void rollDice()
    {
        if (!ActivePlayer().getPawn().isFinished)
        {
            Reset();
            int diceRoll1;
            int diceRoll2;

            diceRoll1 = Random.Range(1, 7);
            dices[diceRoll1 - 1].SetActive(true);

            diceRoll2 = Random.Range(1, 7);
            dices[diceRoll2 + 5].SetActive(true);

            int diceRoll = diceRoll1 + diceRoll2;

            btnDice.interactable = false;

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
                for (int i = 0; i < players.Count; i++)
                {
                    ScoreManager.instance.setUsedSugar(players[i].getId(), players[i].getUsedSugar());
                }

                informationManager.SaveScores(ScoreManager.instance);
                Application.LoadLevel("EndScreen");
            }
        }

        btnDice.interactable = true;
		lblPlayerTurn.text = "Speler " + ActivePlayer().getName() + System.Environment.NewLine + "Beurt " + turnCount;
		lblGlucose.text = "Bloedsuiker: " + ActivePlayer().getModel().getGlucose();
		lblInsuline.text = "Insuline: " + ActivePlayer().getInsulineReserve();

		HUDBackground.color = ActivePlayer ().getPawn ().getColor ();

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
        newPosition.y -= 7;

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
    /// The click event to eat a certain piece of food.
    /// </summary>
    public void clickEat()
    {
		clickFood(selectedFood);
		hideFoodPnl ();
    }

    /// <summary>
    /// The click event to leave a certain item.
    /// </summary>
    public void clickLeave()
    {
        hideFoodPnl();
        playerEndTurn();
    }

    /// <summary>
    /// The click event to eat a piece of food.
    /// </summary>
    /// <param name="id">The id of the food to eat.</param>
    public void clickFood(int id)
    {
		ActivePlayer().getModel().eat(foods[id]);

		if (id == (foods.Count - 1))
		{
			ActivePlayer().useSugar();
			ActivePlayer().skipsTurn = true;
		}

        hideFoodPnl();
        playerEndTurn();
    }

	/// <summary>
	/// Shows the food info.
	/// </summary>
	public void showFoodInfo(int foodId)
	{
		selectedFood = foodId;

		lblFoodName.text = foods [foodId].getName ();
		lblFoodDesciption.text = foods [foodId].getDescription ();
		lblFoodCal.text = "Cal: " + foods [foodId].getCarbs ().ToString();
	}

	/// <summary>
	/// Opens the menu.
	/// </summary>
	public void openMenu()
	{
		pnlMenu.SetActive (true);
	}

	/// <summary>
	/// Closes the menu.
	/// </summary>
	public void closeMenu()
	{
		pnlMenu.SetActive (false);
	}

	/// <summary>
	/// Loads the aantalbeurtenScreen scene(start new game).
	/// </summary>
	public void clickNewGame()
	{
		Application.LoadLevel ("aantalBeurtenScreen");
	}

	/// <summary>
	/// Turns the sound on or off depending on current state.
	/// </summary>
	public void clickSound()
	{
		if (lblSound.text == "Geluid aan")
		{
			audioSource.mute = false;

			lblSound.text = "Geluid uit";
		}
		else
		{
			audioSource.mute = true;

			lblSound.text = "Geluid aan";
		}
	}

    /// <summary>
    /// Gets the active player.
    /// </summary>
    /// <returns>The player whose turn it is right now.</returns>
    public Player ActivePlayer()
    {
        return this.players[playerTurn];
    }

    public Vector3 getActivePawnPosition()
    {
        return ActivePlayer().getPawn().getPosition();
    }
}