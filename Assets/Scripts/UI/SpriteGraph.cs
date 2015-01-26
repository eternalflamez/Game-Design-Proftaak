using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpriteGraph : MonoBehaviour
{
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

	[SerializeField]
	private int offsetWidth = 50;
	[SerializeField]
	private int offsetHeight = 50;
	
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

        float maxpoints = maxPoints;

		for (float i = 0; i < values.Count; i++)
		{
            for (float j = 1; j < maxpoints + 1; j++)
            {
                if (i == Mathf.Round((j / maxpoints) * values.Count))
                {
                    float x = (i * increment) - offsetWidth;
                    /* 
                        *       (  current - min  )
                        * low + ( --------------- ) * ( top - low )
                        *       (   high - min    )
                        * 
                        */

                    float current = values[(int)i];
                    float top = height - margin;
                    float low = margin;
                    float y = (low - offsetHeight) + ((current - min) / (high - min)) * (top - low);

                    if (y == float.NaN)
                    {
                        y = 0;
                    }

                    Debug.Log(current);
                    Vector3 position = new Vector3(x, y, 0f);
                    GameObject go = (GameObject)Instantiate(pointPrefab);
                    go.transform.SetParent(this.transform, true); //.parent = this.transform;
                    go.transform.position = position + offset;
                    break;
                }
            }
		}
	}
}
