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

    [SerializeField]
    private Scrollbar insulinMeter;

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
    /// Indicates the player actually wants to eat the selected food.
    /// </summary>
    public bool eatSelectedFood;

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
    private Text lblFoodName;
    [SerializeField]
    private Text lblFoodDesciption;
    [SerializeField]
    private Text lblFoodCal;

	[SerializeField]
	private Text lblInfo;

    [SerializeField]
    private GameObject btnEatFood;
	[SerializeField]
	private Text txtButtonUseFood;
    [SerializeField]
    private GameObject pnlGrey;
	[SerializeField]
	private GameObject pnlTileMenu;
    [SerializeField]
    private GameObject pnlFood;
	[SerializeField]
	private GameObject pnlDextro;
    [SerializeField]
    private GameObject btnDextro;
    [SerializeField]
    private GameObject btnUseInsulin;

	[SerializeField]
	private GameObject pnlPopupPlayer;
	[SerializeField]
	private Text txtPlayerPopup;
	[SerializeField]
	private GameObject pnlPopupGame;
	[SerializeField]
	private Text txtGamePopup;

	private bool popupPlayerActive = false;

	public List<string> playerPopups;

	[SerializeField]
	private List<Sprite> hudBackgrounds;

	[SerializeField]
	private AudioSource foodAudio;
	[SerializeField]
	private AudioClip audioFood;
	[SerializeField]
	private AudioClip audioDrink;

	private bool loadLevel = false;

	public List<Color> pawnColors;

	/// <summary>
	/// Hides the playerPopup automagicly
	/// </summary>
	/// <returns></returns>
	IEnumerator hidePlayerPopupTimer(float time)
	{
		popupPlayerActive = true;
		float waitTime = 0.0f;

		if (time != -1)
		{
			waitTime = time;
		}
		else
		{
			waitTime = InformationManager.instance.getTimerPopup();
		}
		
		setPlayerPopupText ();

		yield return new WaitForSeconds(waitTime);

		if (playerPopups.Count > 0)
		{
			setPlayerPopupText ();

			yield return new WaitForSeconds(waitTime);
		}

		if (!loadLevel)
		{
			Debug.Log ("Hide");
			hidePlayerPopUp ();
		}
		else
		{
			Application.LoadLevel("EndScreen");
		}

		popupPlayerActive = false;

		yield break;
	}
	/// <summary>
	/// Hides the playerPopup automagicly
	/// </summary>
	/// <returns></returns>
	IEnumerator hideGamePopupTimer(float time)
	{
		float waitTime = 0.0f;
		
		if (time != -1)
		{
			waitTime = time;
		}
		else
		{
			waitTime = InformationManager.instance.getTimerPopup();
		}
		
		yield return new WaitForSeconds(waitTime);

		hideGamePopUp ();
		
		yield break;
	}

	private void setPlayerPopupText()
	{
		Debug.Log ("setPlayerPopupText: " + playerPopups.Count);
		txtPlayerPopup.text = playerPopups [0];
		playerPopups.RemoveAt (0);
	}
	
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
            informationManager.addPlayer("Tom", 22, 180, 90.00f, Gender.Male, new PawnColor(Color.yellow, "Yellow", 1), false);
            informationManager.addPlayer("Henk", 71, 178, 74.22f, Gender.Male, new PawnColor(Color.green, "Green", 2), false);
        }

        hideRouteButtons();
        selectedFood = -1;

        foods.Add(new Food("Cola", "Een glas Cola", 88, 60, ItemType.Drink));
        foods.Add(new Food("Melk halfvol", "Een glas halfvolle melk", 92, 120, ItemType.Drink));
        foods.Add(new Food("Forel", "Een gebraden Forel Filet", 132, 110, ItemType.Food));
        foods.Add(new Food("UNOX Rookworst", "Een portie UNOX Rookworst", 340, 120, ItemType.Food));
        foods.Add(new Food("Ola Raket", "Een Ola Raket waterijs", 40, 95, ItemType.Food));
        foods.Add(new Food("Roomijs", "Een portie ijs", 257, 95, ItemType.Food));
        foods.Add(new Food("Aardbeien", "100 gram aardbeien", 36, 80, ItemType.Food));
        foods.Add(new Food("Banaan", "1 banaan", 188, 80, ItemType.Food));
        foods.Add(new Food("Croissant", "1 Croissant", 239, 120, ItemType.Food));
        foods.Add(new Food("Brood bruin", "1 snee belegd brood", 64, 120, ItemType.Food));
        foods.Add(new Food("Druivensuiker", "Nodig voor Hypo, beurt overslaan", 400, 45, ItemType.Food));

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

        Tile startTile = BoardController.instance.getStartTile();

        for (int index = 0; index < players.Count; index++)
        {
			Vector3 startPosition = new Vector3(startTile.transform.position.x, startTile.transform.position.y, -0.183f);
            GameObject newPawn = (GameObject)Instantiate(pawnObject, startPosition, Quaternion.identity);
            Pawn newPawnController = (Pawn)newPawn.GetComponent("Pawn");
            newPawnController.setCurrentTile(startTile);

            players[index].setPawn(newPawnController);

            startTile.addPawnToTile(newPawnController);
        }

        this.maxTurns = (int)informationManager.getMaxTurns();

        ScoreManager.instance.addPlayers(players);

        playerCount = players.Count;

		lblPlayerTurn.text = "Speler " + ActivePlayer().getName() + System.Environment.NewLine + "Beurt " + turnCount + "/" + InformationManager.instance.getMaxTurns();
        
		setHUDBackground ();
        setInsulinMeter();
        setBloodSugarMeter();

		showPlayerPopUp ("Speler " + ActivePlayer().getName() + " is aan de beurt.", InformationManager.instance.getTimerLong());
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

			hidePlayerPopUp();
			hideGamePopUp();

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

		setInfoText ("");

        ActivePlayer().getPawn().setMoveDir(dir);
    }

    /// <summary>
    /// Ends the turn of the current player, saves scores when needed and gives the next player the turn.
    /// </summary>
    public void playerEndTurn()
    {
        float maxTurns = this.maxTurns;

        //sorry for the magic number, reader.
        float measurePoints = 7;
        
        for (float j = 1; j < measurePoints + 1; j++)
        {
            if (turnCount == Mathf.Round((j / measurePoints) * maxTurns))
            {
                ScoreManager.instance.createMeasurePoint(ActivePlayer().getId(), ActivePlayer().getModel().getGlucose());
                showPlayerPopUp("Meetpunt tekst moet aangepast worden", -1);
                // TODO: Text aanpassen
                break;
            }
        }

        ScoreManager.instance.setScore(ActivePlayer().getId(), ActivePlayer().getModel().getGlucose());

		loadLevel = false;

        if (++playerTurn == playerCount)
        {
            playerTurn = 0;
            turnCount++;

            if (turnCount > maxTurns)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    ScoreManager.instance.setUsedSugar(players[i].getId(), players[i].getUsedSugar());
                }

                informationManager.SaveScores(ScoreManager.instance);
				loadLevel = true;
                
				showPlayerPopUp("Het spel is afgelopen", -1);

				//Application.LoadLevel("EndScreen");
            }
			else
			{
				if (turnCount == (maxTurns - 1))
				{
					showPlayerPopUp("De laatste ronde", -1);
				}
			}
        }

		if (!loadLevel)
		{
			btnDice.interactable = true;
			lblPlayerTurn.text = "Speler " + ActivePlayer().getName() + System.Environment.NewLine + "Beurt " + turnCount + "/" + InformationManager.instance.getMaxTurns();

			setHUDBackground ();
			setInsulinMeter();
			setBloodSugarMeter();

			setInfoText ("");
			showPlayerPopUp ("Speler " + ActivePlayer().getName() + " is aan de beurt.", -1);
		}
    }

	/// <summary>
	/// Shows the popup
	/// </summary>
	/// <param name="text">Text that will be placed in the popup</param>
	/// <param name="time">time can be defined as a custom length, -1 used default value(from Information manager)</param>
	public void showPlayerPopUp(string text, float time)
	{
		pnlPopupPlayer.SetActive (true);
		//txtPlayerPopup.text = text;
		//if (playerPopups.Count == 0)
		//{
		//	txtPlayerPopup.text = text;
		//}
		Debug.Log ("showPlayerPopup: " + text);

		playerPopups.Add (text);

		Debug.Log ("Count: " + playerPopups.Count);

		if (!popupPlayerActive)
		{
			StartCoroutine (hidePlayerPopupTimer(time));
		}
	}

	public void hidePlayerPopUp()
	{
		pnlPopupPlayer.SetActive (false);
		txtGamePopup.text = "";
	}

	public void showGamePopUp(string text, float time)
	{
		pnlPopupGame.SetActive (true);
		txtGamePopup.text = text;
		
		StartCoroutine (hideGamePopupTimer(time));
	}
	
	public void hideGamePopUp()
	{
		pnlPopupGame.SetActive (false);
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
    public void hideFoodPnl()
    {
		pnlTileMenu.SetActive (false);
        //pnlFood.SetActive(false);
    }

    /// <summary>
    /// Shows the food pnl.
    /// </summary>
    public void showFoodPnl(bool onFoodTile)
    {
		pnlTileMenu.SetActive (true);

		if (!onFoodTile)
		{
			pnlFood.SetActive(false);
			pnlDextro.SetActive(true);
		}
		else
		{
			pnlFood.SetActive(true);
			pnlDextro.SetActive(false);
		}
        //pnlFood.SetActive(true);
        //pnlGrey.SetActive(!onFoodTile);
		btnEatFood.GetComponent<Button> ().interactable = false;
        //btnEatFood.SetActive(!eatSelectedFood);

		checkInsulin ();
    }

	/// <summary>
	/// shows or diables insuline buttons
	/// </summary>
	public void checkInsulin()
	{
		if (ActivePlayer().getInsulineReserve() == 0)
		{
			btnUseInsulin.GetComponent<Button>().interactable = false;// .SetActive(false);
		}
		else
		{
			btnUseInsulin.GetComponent<Button>().interactable = true;//btnUseInsulin.SetActive(true);
		}
	}

    /// <summary>
    /// The click event to eat a certain piece of food.
    /// </summary>
    public void clickEat()
    {
        if (selectedFood == foods.Count - 1)
        {
			btnUseInsulin.GetComponent<Button>().interactable = false;//btnUseInsulin.SetActive(false);
        }
        
        if (selectedFood >= 0 )
        {
			if (foods[selectedFood].foodType == ItemType.Food)
			{
				audioSource.PlayOneShot(audioFood);
			}
			else
			{
				audioSource.PlayOneShot(audioDrink);
			}

            btnDextro.SetActive(false);
            eatSelectedFood = true;
			showFoodInfo();
            showFoodPnl(false);
        }
    }

    /// <summary>
    /// The click event to leave a certain item.
    /// </summary>
    public void clickLeave()
    {
        if (eatSelectedFood && selectedFood != -1)
        {
            ActivePlayer().getModel().eat(foods[selectedFood]);

            if (selectedFood == (foods.Count - 1))
            {
                ActivePlayer().useSugar();
                ActivePlayer().skipsTurn = true;
            }
        }

        eatSelectedFood = false;
        selectedFood = -1;
        showFoodInfo();
        hideFoodPnl();
        btnDextro.SetActive(true);
		btnUseInsulin.GetComponent<Button>().interactable = true;
        //btnUseInsulin.SetActive(true);
        playerEndTurn();
    }

    /// <summary>
    /// Shows the food info.
    /// </summary>
    public void showFoodInfo(int foodId)
    {
        selectedFood = foodId;

		if (foods[foodId].foodType == ItemType.Food)
		{
			txtButtonUseFood.text = "Eet op";
		}
		else
		{
			txtButtonUseFood.text = "Drink op";
		}

        lblFoodName.text = foods[foodId].getName();
        lblFoodDesciption.text = foods[foodId].getDescription();
        lblFoodCal.text = "Kcal: " + foods[foodId].getCarbs().ToString();

		btnEatFood.GetComponent<Button> ().interactable = true;
    }

    public void showFoodInfo()
    {
        lblFoodName.text = "";
        lblFoodDesciption.text = "";
        lblFoodCal.text = "";


    }

    /// <summary>
    /// Sets the insulin meter.
    /// </summary>
    public void setInsulinMeter()
    {
        float newSize = 1 - (0.075f * ActivePlayer().getInsulineReserve());

        insulinMeter.size = newSize;
    }

    public void useInsulin()
    {
        selectedFood = -1;
        showFoodInfo();
        btnDextro.SetActive(false);
        ActivePlayer().useInsulinReserves(1f);

		checkInsulin ();
        
        setInsulinMeter();
		setBloodSugarMeter ();
    }

    public void setBloodSugarMeter()
    {
        lblGlucose.text = "" + Mathf.Round(ActivePlayer().getModel().getGlucose() * 100f) / 100f;

        ScoreModel model = ScoreManager.instance.getScoreModel(ActivePlayer().getId());
        float idealValueMin = model.getIdealValue() - model.getIdealValueMargin();
        float idealValueMax = model.getIdealValue() + model.getIdealValueMargin();

        Color textColor = Color.black;

		float r;
		float g;
		float b;

        //check if hyper/hypo
        if (ActivePlayer().getModel().getGlucose() > InformationManager.instance.getHyperThreshold())
        {
			//orange
			r = 242f / 255f;
			g = 104f / 255f;
			b = 25f / 255f;
			textColor = new Color(r, g, b);
        }
        else if (ActivePlayer().getModel().getGlucose() < InformationManager.instance.getHypoThreshold())
        {
			//blue
			r = 25f / 255f;
			g = 35f / 255f;
			b = 242f / 255f;
			textColor = new Color(r, g, b);
        }

        //check ideal value
        if (ActivePlayer().getModel().getGlucose() <= idealValueMax && ActivePlayer().getModel().getGlucose() >= idealValueMin)
        {
			//dark green
			r = 36f / 255f;
			g = 114f / 255f;
			b = 36f / 255f;
			textColor = new Color(r, g, b);
        }

        lblGlucose.color = textColor;
    }

	/// <summary>
	/// Changes the HUD background to the background from the current player.
	/// </summary>
	public void setHUDBackground()
	{
		HUDBackground.sprite = hudBackgrounds[ActivePlayer ().getColorId()];
	}

	/// <summary>
	/// Enables the button dice.
	/// </summary>
	public void enableBtnDice()
	{
		btnDice.interactable = true;
	}
	/// <summary>
	/// Disables the button dice.
	/// </summary>
	public void disableBtnDice()
	{
		btnDice.interactable = false;
	}

	/// <summary>
	/// Sets the info text, used to display squares to walk
	/// </summary>
	/// <param name="text">Text.</param>
	public void setInfoText(string text)
	{
		lblInfo.text = text;
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