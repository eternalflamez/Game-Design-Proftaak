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
	private List<GameObject> gmoTextFields;
	
	[SerializeField]
	private Text playername;

	public int playerId = 0;

	public void setPoints(ScoreModel scoreModel)
	{
		this.values = scoreModel.getBloodSugars();

		playername.text = scoreModel.getPlayerName ();

		for (int index = 0; index < gmoTextFields.Count; index++)
		{
			gmoTextFields[index].SetActive(false);
		}

		Generate ();
	}

	private void Generate()
	{
		bool cull = (values.Count > maxPoints);

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
				gmoTextFields[textCount].SetActive(true);
				textFields[textCount].text = (Mathf.Round(values[i] * 100f) / 100f).ToString();
				textCount++;
			}
		}
	}
}
