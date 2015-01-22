using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public bool canMove = false;
    private float speed = 4;

	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            Vector3 endPoint = GameManager.instance.getActivePawnPosition() + new Vector3(0, -7, -10);

            transform.position = Vector3.Lerp(this.transform.position, endPoint, Time.deltaTime * speed);
        }
	}

    public void AllowMove()
    {
        this.canMove = true;
    }
}
