using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GraphGenerator : MonoBehaviour {
    [SerializeField]
    private GameObject pointPrefab;
    private Vector3 offset;
    private List<float> values;
    [SerializeField]
    private float width;
    [SerializeField]
    private int _playerId;

    public int playerId
    {
        get
        {
            return _playerId;
        }
    }

    public void setPoints(List<float> values)
    {
        this.values = values;
    }

	// Use this for initialization
	public void Generate () {
        offset = this.transform.position;
        
        float increment = width / values.Count;
        for (int i = 0; i < values.Count; i++)
        {
            float x = i * increment;
            Vector3 position = offset + new Vector3(x, values[i], 0f);
            GameObject go = (GameObject)Instantiate(pointPrefab, position, new Quaternion());
            go.transform.parent = this.transform;
        }
	}
}
