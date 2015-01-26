using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

class InformationManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    private static InformationManager _instance;
    public static InformationManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InformationManager>();
            }

            return _instance;
        }
    }

    [SerializeField]
    private float maxTurns;
    [SerializeField]
    private List<Player> players;
    [SerializeField]
    private float playerCount;
    private List<PawnColor> usedColors;

	[SerializeField]
	private int maxInsulin;

	[SerializeField]
	private float timerEndTurn = 1f;
	[SerializeField]
	private float timerPopup = 2.5f;
	[SerializeField]
	private float timerPopupLong = 5.0f;

	public string soundSetting = "Sound";

    public ScoreManager getScoreManager()
    {
        return scoreManager;
    }

    /// <summary>
    /// Returns the current player number that we are adding.
    /// </summary>
    /// <returns>The count of the players list.</returns>
    public float getPlayerSize()
    {
        return players.Count;
    }

	public float getPlayerCount()
	{
		return playerCount;
	}

    public float getMaxTurns()
    {   
        return maxTurns;
    }

    public void setPlayerCount(float playerCount)
    {
        this.playerCount = playerCount;
    }

    public void setMaxTurns(float max)
    {
        this.maxTurns = max;
    }

    public List<PawnColor> getUsedColors()
    {
        return usedColors;
    }

	public float getTimerEndTurn()
	{
		return timerEndTurn;
	}
	public float getTimerPopup()
	{
		return timerPopup;
	}
	public float getTimerLong()
	{
		return timerPopupLong;
	}

    /// <summary>
    /// Adds a playerobject to the list, for later use.
    /// </summary>
    public void addPlayer(string name, int age, int height, float weight, Gender gender, PawnColor c, bool save)
    {
        Player p = new Player();
		p.setInfo(players.Count, name, age, height, weight, gender, c.getColor(), c.getColorId(), maxInsulin);
        players.Add(p);
        usedColors.Add(c);

        if (save)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Player));
            StreamWriter file = new StreamWriter(Path.Combine(Application.persistentDataPath, p.getFileName() + ".xml"));
            ser.Serialize(file, p);
            file.Close();
        }

        if (players.Count == playerCount)
        {
            if (TitleMusic.Instance != null)
            {
                TitleMusic.Instance.Destroy();
            }
            Application.LoadLevel("BoardGame");
        }
        else
        {
            Application.LoadLevel("PlayerInfo");
        }
    }

    public List<Player> getPlayers()
    {
        return this.players;
    }

	public Player getPlayerById(int id)
	{
		for (int index = 0; index < players.Count; index++)
		{
			if (players[index].getId() == id)
			{
				return players[index];
			}
		}

		return null;
	}

    public Player getPlayerByName(string name)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].getName() == name)
            {
                return players[i];
            }
        }

        return null;
    }

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
        
        if (this.players == null)
        {
            this.players = new List<Player>();
        }

        if (this.usedColors == null)
        {
            this.usedColors = new List<PawnColor>();
        }

        this.maxTurns = 10;
        this.playerCount = 0; //original 2
        Application.targetFrameRate = 100;
    }

    public void SaveScores(ScoreManager sm)
    {
        this.scoreManager = sm;
    }

    public void ResetGame()
    {
        this.players = new List<Player>();
        this.usedColors = new List<PawnColor>();
        this.playerCount = 0;
    }
}

