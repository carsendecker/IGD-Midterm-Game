using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSwipe : MonoBehaviour
{
    public GameObject Gate1;
    public GameObject Gate2;
    public Light StatusLight;
    
    private bool swipedCard;
    private bool openGates;

    
    void Start()
    {
        StatusLight.color = Color.red;
    }

    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Card") && !swipedCard)
        {
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            Ray2D ray = new Ray2D(other.transform.position, transform.position - other.transform.position);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, ray.direction, 3f);

            Vector3 hitDirection = hit.normal;

            if (hitDirection.Equals(transform.forward))
            {
                StatusLight.color = Color.green;
                openGates = true;
                StartCoroutine(StatusWindow(5f));
            }
            else
            {
                StatusLight.color = Color.yellow;
                StartCoroutine(StatusWindow(1.5f));
            }
        }
    }

    private IEnumerator StatusWindow(float length)
    {
        swipedCard = true;
        yield return new WaitForSeconds(length);
        swipedCard = false;
        StatusLight.color = Color.red;
        openGates = false;
    }
}
