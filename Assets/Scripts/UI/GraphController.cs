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
	private List<GameObject> trophyPanels;
	[SerializeField]
	private List<Text> trophyNames;
	[SerializeField]
	private List<Text> trophyScore;
    [SerializeField]
    private GameObject[] PlayerGraph;
	[SerializeField]
	private GameObject[] PlayerJournal;

	public List<Player> sortedList;

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

        List<ScoreModel> sortedScores = BubbleSort.Sort(sm.getScoreModels());
		sortedScores.Reverse ();

		int maxTrophys = 3;
		if (sortedScores.Count < maxTrophys)
		{
			maxTrophys = sortedScores.Count;
		}

		for (int index = 0; index < trophyPanels.Count; index++)
		{
			trophyPanels[index].SetActive(false);
		}

		for (int index = 0; index < maxTrophys; index++)
		{
			trophyPanels[index].SetActive(true);
			trophyNames[index].text = InformationManager.instance.getPlayerById(sortedScores[index].getPlayerId()).getName();
			trophyScore[index].text = sortedScores[index].getScore().ToString();
		}

		List<ScoreModel> rankings = ScoreManager.instance.getRanking ();

		for (float i = InformationManager.instance.getPlayerSize(); i < PlayerGraph.Length; i++)
        {
			PlayerGraph[(int)i].SetActive(false);
			PlayerJournal[(int)i].SetActive(false);
        }
    }
}
