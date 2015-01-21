﻿using UnityEngine;
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
	private int playerEndTurnWait = 2;

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

	public int getPlayerWait()
	{
		return playerEndTurnWait;
	}

    /// <summary>
    /// Adds a playerobject to the list, for later use.
    /// </summary>
    public void addPlayer(string name, int age, int height, float weight, Gender gender, PawnColor c, bool save)
    {
        Player p = new Player();
		p.setInfo(players.Count, name, age, height, weight, gender, c.getColor(), maxInsulin);
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

        // TODO: Stahp using dummy data.
        this.maxTurns = 10;
        this.playerCount = 2;
    }

    public void SaveScores(ScoreManager sm)
    {
        this.scoreManager = sm;
    }
}

