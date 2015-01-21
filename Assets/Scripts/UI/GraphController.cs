using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class GraphController : MonoBehaviour {
    [SerializeField]
    private List<SpriteGraph> graphs;
	[SerializeField]
	private List<ShowScore> journals;
    [SerializeField]
    private GameObject[] PlayerGraph;
	[SerializeField]
	private GameObject[] PlayerJournal;

	[SerializeField]
	private Image player1Trophy;
	[SerializeField]
	private Image player2Trophy;
	[SerializeField]
	private Image player3Trophy;
	[SerializeField]
	private Image player4Trophy;

	// Use this for initialization
	void Start () {
        ScoreManager sm = InformationManager.instance.getScoreManager();

        for (int i = 0; i < graphs.Count; i++)
        {
			SpriteGraph gg = graphs[i];
            int id = gg.playerId;
            ScoreModel points = sm.getScoreModel(id);
            if (points != null)
            {
                gg.setPoints(points);
            }
        }

		for (int i = 0; i < journals.Count; i++)
		{
			ShowScore ss = journals[i];
			int id = ss.playerId;
			ScoreModel points = sm.getScoreModel(id);
			if (points != null)
			{
				ss.setPoints(points);
			}
		}

		//calc ranking


		for (float i = InformationManager.instance.getPlayerCount(); i < PlayerGraph.Length; i++)
        {
			PlayerGraph[(int)i].SetActive(false);
			PlayerJournal[(int)i].SetActive(false);
        }
    }
}
