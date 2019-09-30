using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;

    private Rigidbody rb;
    private Vector3 tempVel;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        tempVel = Input.GetAxis("Horizontal") * transform.right;
        tempVel += Input.GetAxis("Vertical") * transform.forward;
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(tempVel.x, rb.velocity.y, tempVel.z) * MoveSpeed;
//        rb.AddForce(0, -9.81f, 0, ForceMode.Force);
    }
}
