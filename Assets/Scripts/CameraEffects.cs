using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {

    [SerializeField]
    private bool shaking;
    private float shakeSpeed;
    private float shakeThreshold;
    [SerializeField]
    private bool shakeForward;
    private float lastShook;

    private float moveThreshold;
    private float moveSpeed;
    [SerializeField]
    private bool moveForward;
    private Vector3 offset;
    private bool allowMove;

	// Use this for initialization
	void Start () {
        allowMove = true;
        shaking = false;
        shakeForward = true;
        shakeSpeed = 75;
        shakeThreshold = 4;
        moveThreshold = .14f;
        moveForward = true;
        offset = new Vector3();
        moveSpeed = 1.6f;
	}
	
	// Update is called once per frame
	void Update () {
        if (shaking)
        {
            Vector3 shakeVector = new Vector3(0, 0, shakeSpeed * Time.deltaTime);
            float z = this.transform.eulerAngles.z;

            if (shakeForward)
            {
                if (z < shakeThreshold || (z <= 360 && z >= (360 - shakeThreshold * 1.2 - lastShook)))
                {
                    this.transform.Rotate(shakeVector);
                }
                else
                {
                    shakeForward = !shakeForward;
                }
            }
            else
            {
                if (z > 360 -shakeThreshold || (z >= 0 && z <= shakeThreshold * 1.2 + lastShook))
                {
                    this.transform.Rotate(-shakeVector);
                }
                else
                {
                    shakeForward = !shakeForward;
                }
            }

            lastShook = shakeSpeed * Time.deltaTime;

            if (allowMove)
            {
                Vector3 moved;
                if (moveForward)
                {
                    moved = Vector3.right * Time.deltaTime;

                    if (this.transform.position.x > moveThreshold)
                    {
                        moveForward = !moveForward;
                    }
                }
                else
                {
                    moved = -Vector3.right * Time.deltaTime;

                    if (this.transform.position.x < -moveThreshold)
                    {
                        moveForward = !moveForward;
                    }
                }

                this.transform.position += moved * moveSpeed;
                offset += moved;
            }
        }
	}

    public void setShaking()
    {
        shaking = !shaking;
        if (!shaking)
        {
            this.transform.eulerAngles = new Vector3();
            this.transform.position -= offset;
            offset = new Vector3();
        }
    }

    public void setMovement()
    {
        allowMove = !allowMove;
    }
}
