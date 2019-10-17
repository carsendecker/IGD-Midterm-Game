using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMove : MonoBehaviour
{
    public float WalkMax, WalkMin; //Time that person will walk forward
    public float WalkSpeed;
    public float PauseMax, PauseMin; //Time person will wait to walk again

    private Rigidbody rb;
    private Ray wallDetect;
    private RaycastHit hit;
    private float pauseTimer;
    private bool walking;
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        wallDetect = new Ray(transform.position, transform.forward);
        Debug.DrawRay(wallDetect.origin, wallDetect.direction * 1f, Color.magenta);
        pauseTimer -= Time.deltaTime;

        if (pauseTimer <= 0)
        {
            if (!walking)
            {
                StartCoroutine(Walk());
            }
        }
        
        
        if (Physics.Raycast(wallDetect, out hit, 1f))
        {
            if (!hit.collider.gameObject.CompareTag("Player"))
            {
                transform.Rotate(0, 180f, 0);
            }
        }
    }

    IEnumerator Walk()
    {
        walking = true;
        transform.Rotate(0, Random.Range(-80, 80), 0);
        rb.velocity = transform.forward * WalkSpeed;

        float rand = Mathf.Round(Random.Range(WalkMin, WalkMax));
        
        yield return new WaitForSeconds(rand);

        rb.velocity = Vector3.zero;
        pauseTimer = Random.Range(PauseMin, PauseMax);
        walking = false;
    }
}
