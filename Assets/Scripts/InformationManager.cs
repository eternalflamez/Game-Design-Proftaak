using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class InformationManager : MonoBehaviour
{
    public static InformationManager instance;

    private float maxTurns;
    private List<Player> players;

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
    public void addPlayer(string name, int age, int height, float weight, Gender gender)
    {
        Player p = new Player();
        p.setInfo(name, age, height, weight, gender);
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
        instance = this;
        this.maxTurns = 25;
        this.players = new List<Player>();
    }
}

