using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ScoreManager : MonoBehaviour 
{
    public static ScoreManager instance;
    private List<ScoreModel> scores;

    void Awake()
    {
        instance = this;
        scores = new List<ScoreModel>();
    }

    public void addPlayers(List<Player> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            addPlayer(players[i].getName());
        }
    }

    private void addPlayer(string playerName)
    {
        this.scores.Add(new ScoreModel(playerName));
    }

    public void setScore(string playerName, float bloodSugar)
    {
        getScoreModel(playerName).setScore(bloodSugar);
    }

    public void createMeasurePoint(string playerName, float bloodSugar)
    {
        getScoreModel(playerName).createMeasurePoint(bloodSugar);
    }

    public ScoreModel getScoreModel(string playerName)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            if (playerName == scores[i].getPlayerName())
            {
                return scores[i];
            }
        }

        // twas not found
        throw new KeyNotFoundException("No ScoreModel was found attached to player " + playerName);
    }
}
