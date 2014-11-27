using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private float speed = 5;
    private float deceleration = 13;
    private float curSpeed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.transform.rotation = new Quaternion();

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            curSpeed = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            curSpeed = speed;
        }
        else
        {
            if (curSpeed > 0)
            {
                curSpeed -= deceleration * Time.deltaTime;
            }
            else if(curSpeed < 0)
            {
                curSpeed += deceleration * Time.deltaTime;
            }
        }

        if (Mathf.Abs(curSpeed) - .1f < 0)
        {
            curSpeed = 0;
        }

        this.rigidbody2D.velocity = new Vector2(curSpeed, this.rigidbody2D.velocity.y);
	}
}
