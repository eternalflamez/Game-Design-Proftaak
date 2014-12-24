﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ScoreModel
{
    private string playerName;
    private float idealValue;
    private List<float> bloodSugars;
    private int hypoTurns;
    private int hyperTurns;
    private List<float> measurePoints;

    private float hypoThreshold = 3;
    private float hyperThreshold = 15;

    public List<float> getBloodSugars()
    {
        return bloodSugars;
    }

    public int getHypoTurns()
    {
        return hypoTurns;
    }

    public int getHyperTurns()
    {
        return hyperTurns;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public List<float> getMeasurePoints()
    {
        return measurePoints;
    }

    /// <summary>
    /// Creates a new ScoreModel, to keep track of the scores of a player.
    /// </summary>
    /// <param name="playerName">The name of the player.</param>
    /// <param name="idealValue">The ideal value of the player. (Default is 6)</param>
    public ScoreModel(string playerName, float idealValue = 6)
    {
        this.playerName = playerName;
        this.bloodSugars = new List<float>();
        this.measurePoints = new List<float>();
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