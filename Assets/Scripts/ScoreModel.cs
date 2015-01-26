using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ScoreModel
{
    private int playerId;
    private string playerName;
    private float idealValue;
    private float idealValueMargin;
    private List<float> bloodSugars;
    private int hypoTurns;
    private int hyperTurns;
    private List<float> measurePoints;

    private float hypoThreshold = 3;
    private float hyperThreshold = 12;
    private int usedSugar = 0;

    /// <summary>
    /// Gets the list of bloodsugars used for the graphs.
    /// </summary>
    /// <returns>A list of floats containing the bloodsugar values of the player per turn.</returns>
    public List<float> getBloodSugars()
    {
        return bloodSugars;
    }

	public float getIdealValue()
	{
		return idealValue;
	}
	public float getIdealValueMargin()
	{
		return idealValueMargin;
	}

    public string getPlayerName()
    {
        return playerName;
    }

    public int getHypoTurns()
    {
        return hypoTurns;
    }

    public int getHyperTurns()
    {
        return hyperTurns;
    }

    public int getPlayerId()
    {
        return playerId;
    }

	public float getHyperThreshold()
	{
		return hyperThreshold;
	}

	public float getHypoThreshold()
	{
		return hypoThreshold;
	}

    public void setUsedSugar(int usedSugar)
    {
        this.usedSugar = usedSugar;
    }

    public float getScore()
    {
        float maxPointsPTurn = 10f;
        float score = 0;
        
        for (int i = 0; i < measurePoints.Count; i++)
        {
            float measurePoint = measurePoints[i];
            float points = 0;

            if (measurePoint > idealValue + idealValueMargin)
            {
                if (measurePoint > hyperThreshold)
                {
                    points = 5;
                }
                else
                {
                    points = 7.5f;
                }
                Debug.Log("Too high, added " + points + "for value" + measurePoint);
            }
            else if (measurePoint < idealValue - idealValueMargin)
            {
                if (measurePoint < hypoThreshold)
                {
                    points = 5;
                }
                else
                {
                    points = 7.5f;
                }
                Debug.Log("Too low, added " + points + "for value" + measurePoint);
            }
            else
            {
                points = maxPointsPTurn;
                Debug.Log("Correct, added " + points);
            }

            score += points;
        }

        score -= 5 * usedSugar;

        if (score < 0)
        {
            score = 0;
        }

        return Mathf.Round(score / (0.7f));
    }

    /// <summary>
    /// Gets the list of seven measure points that are created throughout the playthrough.
    /// </summary>
    /// <returns>A list of 7 bloodsugar values.</returns>
    public List<float> getMeasurePoints()
    {
        return measurePoints;
    }

    /// <summary>
    /// Creates a new ScoreModel, to keep track of the scores of a player.
    /// </summary>
    /// <param name="playerName">The name of the player.</param>
    /// <param name="idealValue">The ideal value of the player. (Default is 6)</param>
    public ScoreModel(int playerId, string playerName, float idealValue = 6, float idealValueMargin = 1)
    {
        this.playerName = playerName;
        this.playerId = playerId;
        this.bloodSugars = new List<float>();
        this.measurePoints = new List<float>();
        this.idealValue = idealValue;
        this.idealValueMargin = idealValueMargin;
        hyperTurns = 0;
        hypoTurns = 0;
    }

    /// <summary>
    /// Adds a bloodsugar value to the score, for the display of the graph at the end.
    /// Also checks if the user is in a hypo or hyper.
    /// </summary>
    /// <param name="bloodSugar">The amount of bloodsugar.</param>
    public void setScore(float bloodSugar)
    {
        bloodSugars.Add(bloodSugar);

        if (bloodSugar > hyperThreshold)
        {
            hyperTurns++;
        }
        else if (bloodSugar < hypoThreshold)
        {
            hypoTurns++;
        }
    }

    /// <summary>
    /// Creates a new measure point for this player.
    /// </summary>
    /// <param name="bloodSugar">The amount of bloodsugar at the measure point.</param>
    public void createMeasurePoint(float bloodSugar)
    {
        measurePoints.Add(bloodSugar);
    }
}