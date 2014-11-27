using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private float speed = 5;
    private float deceleration = 13;
    private float curSpeed = 0;

	private bool isEnded = false;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () 
    {
		this.transform.rotation = new Quaternion();

		if (!isEnded)
		{
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

	void OnGUI()
	{
		if (isEnded)
		{
			if (GUI.Button(new Rect(310,180,200,30), "Restart Level"))
			{
				Application.LoadLevel (0);
			}
		}
	}

	private void stopGame()
	{
		isEnded = true;

		this.rigidbody2D.isKinematic = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		stopGame ();
	}
}
