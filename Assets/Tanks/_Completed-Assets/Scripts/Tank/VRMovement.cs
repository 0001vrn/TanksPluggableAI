using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMovement : MonoBehaviour {

    public float m_Speed = 12f;

    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
	private float m_MovementInputValue;
    private float toggleValue = 1f;
    private readonly float EPSILON = 0.2f;

	bool one_click = false;
    bool forwardOrBackward = false;//false neans forward
	//bool timer_running;
	float timer_for_double_click = 0f;

	//this is how long in seconds to allow for a double click
	float delay = 0.2f;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
		// When the tank is turned on, make sure it's not kinematic.
		m_Rigidbody.isKinematic = false;

		// Also reset the input values.
		m_MovementInputValue = 0f;
    }

    void OnDisable()
    {
		// When the tank is turned off, set it to kinematic so it stops moving.
		m_Rigidbody.isKinematic = true;
    }

    // Use this for initialization
    void Start () 
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
		
        if (Input.GetMouseButtonDown(0))
		{
			if (!one_click) // first click no previous clicks
			{
				one_click = true;

                timer_for_double_click = Time.time; // save the current time
                                                    // do one click things;

                m_MovementInputValue = toggleValue;
			}
			else
			{
				one_click = false; // found a double click, now reset
                //Debug.Log("Double click");
                //do double click things
                forwardOrBackward = !forwardOrBackward;
                toggleValue = forwardOrBackward ? (float)1 : (float)-1; 
            }
		}
        if (one_click)
        {
            // if the time now is delay seconds more than when the first click started. 
            if ((Time.time - timer_for_double_click) > delay)
            {

                //basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.

                one_click = false;

            }
        }

        if (Input.GetMouseButtonUp(0))
            m_MovementInputValue = 0;
	}

    void FixedUpdate()
    {
        Move();
    }

	private void Move()
	{
		// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}
}
