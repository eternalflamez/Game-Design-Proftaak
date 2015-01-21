using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShowScore : MonoBehaviour
{
	private List<float> values;
	private int maxPoints = 10;

	[SerializeField]
	private List<Text> textFields;
	
	[SerializeField]
	private Text playername;

	public int playerId = 0;

	public void setPoints(ScoreModel scoreModel)
	{
		this.values = scoreModel.getBloodSugars();
		/*values = new List<float> ();
		values.Add (5.0f);
		values.Add (6.0f);
		values.Add (7.0f);
		values.Add (8.0f);
		values.Add (7.0f);
		values.Add (3.0f);
		values.Add (2.0f);
		values.Add (1.0f);
		values.Add (2.0f);
		values.Add (2.0f);
		values.Add (2.0f);
		values.Add (2.0f);
		values.Add (3.0f);
		values.Add (8.0f);
		values.Add (9.0f);
		values.Add (2.0f);
		values.Add (2.0f);
		values.Add (3.0f);
		values.Add (8.0f);
		values.Add (9.0f);*/

		playername.text = scoreModel.getPlayerName ();

		Generate ();
	}

	private void Generate()
	{
		bool cull = (values.Count > maxPoints);
		Debug.Log (cull);
		int textCount = 0;
		for (int i = 0; i < values.Count; i++)
		{
			// For instance take count == 100
			// max = 4
			// if i % (25) != 0, then we won't show this point
			// this is because this is true whenever i == 0, 25, 50 or 75
			// precisely the points we want it to.
			if ((cull && Mathf.Floor(i % (values.Count / maxPoints)) == 0) || !cull)
			{
				textFields[textCount].text = values[i].ToString();
				textCount++;
			}
		}
	}
}
