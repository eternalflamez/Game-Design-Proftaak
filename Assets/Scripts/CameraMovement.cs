using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    float speed = 4;

	// Use this for initialization
	void Start () {
        Camera c = this.GetComponent<Camera>();
        c.orthographicSize = 5;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 endPoint = GameManager.instance.getActivePawnPosition() + new Vector3(0, -7, -10);

        transform.position = Vector3.Lerp(this.transform.position, endPoint, Time.deltaTime * speed);
	}
}
