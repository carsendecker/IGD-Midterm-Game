using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpForce;
    public float InteractDistance;
    public float PushbackForce;
    public TMP_Text SwipePrompt;
    public GameObject MetroCard;
    public AudioClip oof;

    private Rigidbody rb;
    private Vector3 tempVel;
    private Camera cam;
    private RaycastHit hit;
    private bool lookingAtSwiper;
    private bool swiping;
    private bool canMove;
    private AudioSource aso;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        canMove = true;
        aso = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        tempVel = Input.GetAxis("Horizontal") * transform.right;
        tempVel += Input.GetAxis("Vertical") * transform.forward;

//        Debug.DrawRay(transform.position, Vector3.down * 1, Color.blue);
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 1))
        {
            rb.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }

        CheckForInteraction();
        
        if (lookingAtSwiper && !swiping)
        {
            SwipePrompt.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                SwipePrompt.enabled = false;
                Swipe(true);
            }
        }
        else
        {
            SwipePrompt.enabled = false;
        }

        if (swiping && Input.GetKeyDown(KeyCode.E))
        {
            Swipe(false);
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(tempVel.x * MoveSpeed, rb.velocity.y, tempVel.z * MoveSpeed), 0.2f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            rb.AddForce(other.contacts[0].normal * PushbackForce, ForceMode.Impulse);
            aso.PlayOneShot(oof);
        }
    }

    
    
    private void CheckForInteraction()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward,  out hit, InteractDistance))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Swipe"))
            {
                lookingAtSwiper = true;
            }
        }
        else 
            lookingAtSwiper = false;
    }

    public void Swipe(bool enable)
    {
        StartCoroutine(Delay(enable));
        MetroCard.SetActive(enable);
        GetComponentInChildren<MouseLook>().enabled = !enable;
        canMove = !enable;
    }

    IEnumerator Delay(bool enable)
    {
        yield return new WaitForSeconds(0.2f);
        swiping = enable;
    }
}
