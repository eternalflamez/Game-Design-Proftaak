using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ScoreManager : MonoBehaviour 
{
    /// <summary>
    /// An instance of the ScoreManager, use this to add scores to a player.
    /// </summary>
    public static ScoreManager instance;

    /// <summary>
    /// The list of scores of players.
    /// </summary>
    private List<ScoreModel> scores;

    void Awake()
    {
        instance = this;
        scores = new List<ScoreModel>();
    }

    /// <summary>
    /// Adds points for a list of players.
    /// </summary>
    /// <param name="players">The players to save the current points for.</param>
    public void addPlayers(List<Player> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            addPlayer(players[i].getId(), players[i].getName());
        }
    }

    /// <summary>
    /// Adds a score for one player.
    /// </summary>
    /// <param name="playerName"></param>
    private void addPlayer(int playerId, string name)
    {
        this.scores.Add(new ScoreModel(playerId, name));
    }

    /// <summary>
    /// Sets a certain score for a certain player.
    /// </summary>
    /// <param name="playerName">The name of the player to add the score to.</param>
    /// <param name="bloodSugar">The current bloodsugar level of the player.</param>
    public void setScore(int playerId, float bloodSugar)
    {
        getScoreModel(playerId).setScore(bloodSugar);
    }

    public void setUsedSugar(int playerId, int usedSugar)
    {
        getScoreModel(playerId).setUsedSugar(usedSugar);
    }

    /// <summary>
    /// Creates a new measure point (one of the seven).
    /// </summary>
    /// <param name="playerName">The name of the player to add the score to.</param>
    /// <param name="bloodSugar">The current bloodsugar level of the player.</param>
    public void createMeasurePoint(int playerId, float bloodSugar)
    {
        getScoreModel(playerId).createMeasurePoint(bloodSugar);
    }

    /// <summary>
    /// Gets the scores of a certain player.
    /// </summary>
    /// <param name="playerName">The name of the player to find.</param>
    /// <returns>The ScoreModel that corresponds to the queried player, or a KeyNotFoundException.</returns>
    public ScoreModel getScoreModel(int playerId)
    {
        for (int i = 0; i < scores.Count; i++)
        {
            if (playerId == scores[i].getPlayerId())
            {
                return scores[i];
            }
        }

        return null;
    }
}
