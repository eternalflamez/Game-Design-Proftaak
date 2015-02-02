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
	private Image imgBackground;

	[SerializeField]
	private Text txtDotValue;

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

	public void showBigGraph(int id)
	{
		ScoreManager sm = InformationManager.instance.getScoreManager();
		ScoreModel points = sm.getScoreModel(id);

		setBackgroundColor (points.getPlayerId ());

		this.values = points.getBloodSugars();
		this.GenerateBig ();
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
		this.score.text = scoreModel.getScore().ToString() + "/100";

		setBackgroundColor(scoreModel.getPlayerId ());
		
		this.GenerateSmall();
	}

	private void setBackgroundColor(int id)
	{
		//show player color
		Player player = (Player)InformationManager.instance.getPlayerById (id);
		int color = player.getColorId ();
		imgBackground.color = GameManager.instance.pawnColors [color];
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

	public void showDotValue(Transform dotPosition)
	{
		txtDotValue.text = "Value: " + Mathf.Round((dotPosition.position.y / 50) * 100f) / 100f;
	}
	
	// Use this for initialization
	private void GenerateSmall()
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
		float count = values.Count;

		for (float i = 1; i < values.Count + 1; i++)
		{
            for (float j = 1; j < maxpoints + 1; j++)
            {
                if (i == Mathf.Round((j / maxpoints) * count))
                {
                    float x = (i * increment) - offsetWidth;
                    /* 
                        *       (  current - min  )
                        * low + ( --------------- ) * ( top - low )
                        *       (   high - min    )
                        * 
                        */

                    float current = values[(int)i - 1];
                    float top = height - margin;
                    float low = margin;
                    float y = (low - offsetHeight) + ((current - min) / (high - min)) * (top - low);

                    if (y == float.NaN)
                    {
                        y = 0;
                    }

                    Vector3 position = new Vector3(x, y, 0f);
                    GameObject go = (GameObject)Instantiate(pointPrefab);
                    go.transform.SetParent(this.transform, true); //.parent = this.transform;
                    go.transform.position = position + offset;
                    break;
                }
            }
		}
	}

	public void clearGraphBig()
	{
		GameObject[] dots = GameObject.FindGameObjectsWithTag ("DotBig");

		for (int index = 0; index < dots.Length; index++)
		{
			GameObject.Destroy(dots[index]);
		}
	}

	// Use this for initialization
	private void GenerateBig()
	{
		offset = this.transform.position;
		
		float increment = width / values.Count;
		float min = 6;
		float high = 6;

		int dotCount = 0;
		
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
		float count = values.Count;
		
		for (float i = 1; i < values.Count + 1; i++)
		{
			for (float j = 1; j < maxpoints + 1; j++)
			{
				if (i == Mathf.Round((j / maxpoints) * count))
				{
					//float x = (i * increment) - offsetWidth;
					/* 
                        *       (  current - min  )
                        * low + ( --------------- ) * ( top - low )
                        *       (   high - min    )
                        * 
                        */
					
					float current = values[(int)i - 1];
					float top = height - margin;
					float low = margin;
					float x = dotCount * 50.0f;
					float y = current * 50.0f;

					dotCount++;

					if (y == float.NaN)
					{
						y = 0;
					}

					GameObject go = (GameObject)Instantiate(pointPrefab);
					go.transform.position = new Vector3(x, y, 0f);
					go.transform.SetParent(this.transform, false); //.parent = this.transform;
					go.tag = "DotBig";
					break;
				}
			}
		}
	}
}
