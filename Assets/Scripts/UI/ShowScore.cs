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
        int textCount = 0;
        for (int i = 0; i < values.Count; i++)
        {
            gmoTextFields[textCount].SetActive(true);
            textFields[textCount].text = (Mathf.Round(values[i] * 100f) / 100f).ToString();
            textCount++;
        }
    }
}
