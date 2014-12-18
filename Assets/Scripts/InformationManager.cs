using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class InformationManager : MonoBehaviour
{
    public static InformationManager instance;

    private int maxTurns;
    private List<Player> players;

    public int getMaxTurns()
    {   
        return maxTurns;
    }

    public void setMaxTurns(int max)
    {
        this.maxTurns = max;
    }

    /// <summary>
    /// TODO: Adds a playerobject to the list, for later use.
    /// </summary>
    public void addPlayer()
    {

    }

    public List<Player> getPlayers()
    {
        return this.players;
    }

    public Player getPlayerByName(string name)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].name == name)
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
    }
}

