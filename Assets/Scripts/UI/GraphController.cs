using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GraphController : MonoBehaviour {
    [SerializeField]
    private List<GraphGenerator> graphs;

	// Use this for initialization
	void Start () {
        ScoreManager sm = InformationManager.instance.getScoreManager();
        
        for (int i = 0; i < graphs.Count; i++)
        {
            GraphGenerator gg = graphs[i];
            int id = gg.playerId;
            ScoreModel points = sm.getScoreModel(id);
            gg.setPoints(points.getBloodSugars());
            gg.Generate();
        }
    }
}
