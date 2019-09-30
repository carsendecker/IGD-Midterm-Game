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
	private float verticalAngle = 0;
    
    void Start()
    {
	    rb = GetComponent<Rigidbody>();
	    cam = Camera.main;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X"); //Horiz. mouse velocity
        mouseY = Input.GetAxis("Mouse Y"); // Vertical mouse velocity

//	    verticalAngle = transform.localEulerAngles.y;
        
//	    transform.localEulerAngles = new Vector3(0, 
//		    Mathf.Clamp(transform.localEulerAngles.y, -AngleClamp, AngleClamp),
//		    0);
    }

    private void FixedUpdate()
    {
	    Vector3 vel = rb.velocity;
//	    vel.x = mouseX * MoveSpeed;
//	    vel.z = mouseY * MoveSpeed;

	    vel = cam.transform.right * mouseX + cam.transform.forward * mouseY;

	    rb.velocity = vel * MoveSpeed;
    }
}
