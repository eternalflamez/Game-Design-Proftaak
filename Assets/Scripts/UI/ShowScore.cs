using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShowScore : MonoBehaviour
{
	private List<float> values;
	private int maxPoints = 10;

	[SerializeField]
	private GameObject txtScorePrefab;

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

		if (txtScorePrefab != null)
		{
			int x = -1170;

			for (int index = 0; index < values.Count; index++)
			{
				x = x + 85;
				Vector3 newTextPosition = new Vector3(x,-6.5f,0);
				Debug.Log ("NewPosition" + index + ": " + newTextPosition);
				GameObject newGameObject = (GameObject)GameObject.Instantiate(txtScorePrefab);
				newGameObject.transform.position = newTextPosition;
				newGameObject.transform.SetParent(this.gameObject.transform, false);

				Text newText = (Text)newGameObject.GetComponent<Text>();
				newText.text = (Mathf.Round(values[index] * 100f) / 100f).ToString();
			}
		}


        //for (int i = 0; i < values.Count; i++)
        //{
        //    gmoTextFields[textCount].SetActive(true);
        //    textFields[textCount].text = (Mathf.Round(values[i] * 100f) / 100f).ToString();
        //    textCount++;
        //}
    }
}
