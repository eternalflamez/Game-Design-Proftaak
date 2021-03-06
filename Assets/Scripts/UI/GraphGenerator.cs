﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GraphGenerator : MonoBehaviour
{
	[SerializeField]
	private Sprite pointPre;
    [SerializeField]
    private GameObject pointPrefab;
    private Vector3 offset;
    private List<float> values;
    [SerializeField]
    private float width;
    [SerializeField]
    private int _playerId;
    [SerializeField]
    private int maxPoints;
    [SerializeField]
    private float height;
    [SerializeField]
    private float margin;
    [SerializeField]
    private Text playername;
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text hyper;
    [SerializeField]
    private Text neutral;
    [SerializeField]
    private Text hypo;

    public int playerId
    {
        get
        {
            return _playerId;
        }
    }

    /// <summary>
    /// Sets the score values.
    /// Also generates the graphs, so only use this once per Generator!
    /// </summary>
    /// <param name="scoreModel">The scoremodel to display</param>
    public void setPoints(ScoreModel scoreModel)
    {
        this.values = scoreModel.getBloodSugars();
        float hyper = scoreModel.getHyperTurns();
        float neutral = values.Count;
        float hypo = scoreModel.getHypoTurns();
        float total = hyper + neutral + hypo;

        this.hyper.text = makePercent(hyper, total);
        this.neutral.text = makePercent(neutral, total);
        this.hypo.text = makePercent(hypo, total);
        this.playername.text = scoreModel.getPlayerName();
        this.score.text = scoreModel.getScore().ToString();

        this.Generate();
    }

    /// <summary>
    /// Returns how much % a is of b.
    /// </summary>
    /// <param name="a">The smaller amount.</param>
    /// <param name="b">The total.</param>
    /// <returns>c%</returns>
    private string makePercent(float a, float b)
    {
        return Mathf.Floor(100 * (a / b)) + "%";
    }

    // Use this for initialization
    private void Generate()
    {
        offset = this.transform.position;

        float increment = width / values.Count;
        bool cull = (values.Count > maxPoints);
        float min = 6;
        float high = 6;

        for (int i = 0; i < values.Count; i++)
        {
            if (values[i] < min)
            {
                min = values[i];
            }

            if (values[i] > high)
            {
                high = values[i];
            }
        }

        for (int i = 0; i < values.Count; i++)
        {
            for (int j = 0; j < maxPoints; j++)
            {
                if (i == Mathf.Round((j / maxPoints) * values.Count))
                {
                    float x = i * increment;
                    /* 
                     *       (  current - min  )
                     * low + ( --------------- ) * ( top - low )
                     *       (   high - min    )
                     * 
                        */

                    float current = values[i];
                    float top = height - margin;
                    float low = margin;
                    float y = low + ((current - min) / (high - min)) * (top - low);

                    if (y == float.NaN)
                    {
                        y = 0;
                    }

                    Vector3 position = new Vector3(x, y, 0f);
                    GameObject go = (GameObject)Instantiate(pointPre);
                    go.transform.parent = this.transform;
                    go.transform.position = position + offset;
                }
            }
        }
    }
}
