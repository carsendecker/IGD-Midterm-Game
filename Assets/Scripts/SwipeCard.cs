using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCard : MonoBehaviour
{
	public float MoveSpeed;
	public float AngleClamp = 40f;
	
    private Rigidbody rb;
    private float mouseX, mouseY;
	private Camera cam;
	private Vector3 perfectAngle;
	private Vector3 startPos;


	private void Awake()
	{
		startPos = transform.position;
	}

	private void OnEnable()
	{
		transform.localPosition = startPos;
	}
	
    void Start()
    {
	    rb = GetComponent<Rigidbody>();
	    cam = Camera.main;
	    perfectAngle = transform.forward;
	    gameObject.SetActive(false);
    }

	void Update()
    {
        mouseX = Input.GetAxis("Mouse X"); //Horiz. mouse velocity
        mouseY = Input.GetAxis("Mouse Y"); // Vertical mouse velocity
    }

    private void FixedUpdate()
    {
	    Vector3 vel = rb.velocity;
	    vel = cam.transform.right * mouseX + cam.transform.forward * mouseY;
	    rb.velocity = vel * MoveSpeed;

//		float clampedY = Mathf.Clamp(transform.rotation.eulerAngles.y, -30, 30);
//	    Vector3 clampedAngles = new Vector3(transform.rotation.eulerAngles.x, clampedY, transform.rotation.eulerAngles.z);
//	    transform.rotation = Quaternion.Euler(clampedAngles);

//	    Debug.Log(Vector3.Angle(transform.forward, perfectAngle));
	    if (Vector3.Angle(transform.forward, perfectAngle) > AngleClamp)
	    {
		    Quaternion newRot = Quaternion.FromToRotation(transform.forward, perfectAngle);
		    float clampedAngleY = Mathf.Clamp(newRot.eulerAngles.y, 0, 30);
		    Debug.Log(clampedAngleY);
		    rb.AddTorque(new Vector3(0, -clampedAngleY, 0));
		    
	    }
//	    else if (Vector3.SignedAngle(perfectAngle, transform.forward, Vector3.up) < -AngleClamp)
//	    {
//		    Quaternion newRot = Quaternion.FromToRotation(transform.forward, perfectAngle);
//		    float clampedAngleY = Mathf.Clamp(newRot.eulerAngles.y, -30, 0);
//		    Debug.Log(clampedAngleY);
//
//		    rb.AddTorque(new Vector3(0, clampedAngleY, 0));
//
//	    }
    }

 }
