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
    public TMP_Text SwipePrompt;
    public GameObject MetroCard;

    private Rigidbody rb;
    private Vector3 tempVel;
    private Camera cam;
    private RaycastHit hit;
    private bool lookingAtSwiper;
    private bool swiping;
    private bool canMove;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        tempVel = Input.GetAxis("Horizontal") * transform.right;
        tempVel += Input.GetAxis("Vertical") * transform.forward;

        if (Input.GetKeyDown(KeyCode.Space))
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
            rb.velocity = new Vector3(tempVel.x * MoveSpeed, rb.velocity.y, tempVel.z * MoveSpeed);
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

    private void Swipe(bool enable)
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
