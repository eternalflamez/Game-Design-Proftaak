using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GraphController : MonoBehaviour {
    [SerializeField]
    private List<SpriteGraph> graphs;
    [SerializeField]
    private GameObject[] PlayerUI;

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

        for (float i = InformationManager.instance.getPlayerCount(); i < PlayerUI.Length; i++)
        {
            PlayerUI[(int)i].SetActive(false);
        }
    }
}
