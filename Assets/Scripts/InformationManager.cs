using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class InformationManager : MonoBehaviour
{
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
    private float playerCount = 2;

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

    /// <summary>
    /// Adds a playerobject to the list, for later use.
    /// </summary>
    public void addPlayer(string name, int age, int height, float weight, Gender gender, Color c)
    {
        Player p = new Player();
        p.setInfo(name, age, height, weight, gender, c);
        players.Add(p);

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

        // TODO: Stahp using dummy data.
        this.maxTurns = 25;
    }

    /// <summary>
    /// TODO: REMOVE, THIS IS FOR TESTING ONLY
    /// </summary>
    public void LoadPlayerInfoScene()
    {
        Application.LoadLevel("PlayerInfo");
    }
}

