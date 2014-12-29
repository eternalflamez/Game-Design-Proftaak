using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    private float maxTurns;
    private List<Player> players;
    private float playerCount;
    private List<PawnColor> usedColors;

    public ScoreManager getScoreManager()
    {
        return scoreManager;
    }

    /// <summary>
    /// Returns the current player number that we are adding.
    /// </summary>
    /// <returns>The count of the players list.</returns>
    public float getPlayerCount()
    {
        return players.Count;
    }

    public float getMaxTurns()
    {   
        return maxTurns;
    }

    public void setMaxTurns(float max)
    {
        this.maxTurns = max;
    }

    public List<PawnColor> getUsedColors()
    {
        return usedColors;
    }

    /// <summary>
    /// Adds a playerobject to the list, for later use.
    /// </summary>
    public void addPlayer(string name, int age, int height, float weight, Gender gender, PawnColor c)
    {
        Player p = new Player();
        p.setInfo(players.Count, name, age, height, weight, gender, c.getColor());
        players.Add(p);
        usedColors.Add(c);

        if (players.Count == playerCount)
        {
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
        DontDestroyOnLoad(transform.gameObject);
        
        if (this.players == null)
        {
            this.players = new List<Player>();
        }

        if (this.usedColors == null)
        {
            this.usedColors = new List<PawnColor>();
        }

        // TODO: Stahp using dummy data.
        this.maxTurns = 15;
        this.playerCount = 2;
    }

    public void SaveScores(ScoreManager sm)
    {
        this.scoreManager = sm;
    }

    /// <summary>
    /// TODO: REMOVE, THIS IS FOR TESTING ONLY
    /// </summary>
    public void LoadPlayerInfoScene()
    {
        Application.LoadLevel("PlayerInfo");
    }
}

