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
	private Text playername;

	[SerializeField]
	private GameObject scrollview;

	public int playerId = 0;

	public void setPoints(ScoreModel scoreModel)
	{
		this.values = scoreModel.getBloodSugars();

		playername.text = scoreModel.getPlayerName ();

		//show player color
		Player player = (Player)InformationManager.instance.getPlayerById (scoreModel.getPlayerId ());
		int color = player.getColorId ();
		gameObject.GetComponent<Image> ().color = GameManager.instance.pawnColors [color];

		Generate ();
	}

    private void Generate()
    {
        int textCount = 0;

		if (txtScorePrefab != null)
		{
			RectTransform contentTrans = (RectTransform)gameObject.transform.GetComponent<RectTransform>();
			RectTransform scrollviewTrans = (RectTransform)gameObject.transform.GetComponent<RectTransform>();
			contentTrans.sizeDelta = new Vector2((values.Count * 85), contentTrans.sizeDelta.y);
			Debug.Log ("offset: " + Mathf.Abs((contentTrans.sizeDelta.x - scrollviewTrans.sizeDelta.x) / 2));
			Debug.Log ("offset1: " +(contentTrans.sizeDelta.x - scrollviewTrans.sizeDelta.x) / 2);
			contentTrans.transform.position = new Vector3(Mathf.Abs((contentTrans.sizeDelta.x - scrollviewTrans.sizeDelta.x) / 2), scrollviewTrans.position.y, scrollviewTrans.position.z);

			float x = 0 - contentTrans.sizeDelta.x / 2;
			x = x - 25;

			for (int index = 0; index < values.Count; index++)
			{
				x = x + 85;

				Vector3 newTextPosition = new Vector3(x,-6.5f,0);
				GameObject newGameObject = (GameObject)GameObject.Instantiate(txtScorePrefab);
				newGameObject.transform.position = newTextPosition;
				newGameObject.transform.SetParent(this.gameObject.transform, false);

				Text newText = (Text)newGameObject.GetComponent<Text>();
				newText.text = (Mathf.Round(values[index] * 100f) / 100f).ToString();
			}
		}
    }
}
