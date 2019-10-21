using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectSwipe : MonoBehaviour
{
    public float MinimumCardSpeed;
    public GameObject Gate1;
    public GameObject Gate2;
    public float GateMoveDistance;
    public Light StatusLight;
    public TMP_Text StatusText;
    public AudioClip RightSound, WrongSound;
    public GameObject Confetti;
    
    private bool swipedCard;
    private bool openGates;
    private Vector3 initGatePos1, initGatePos2;
    private AudioSource aso;

    
    void Start()
    {
        Confetti.SetActive(false);
        StatusLight.color = Color.red;
        StatusText.text = "";
        initGatePos1 = Gate1.transform.position;
        initGatePos2 = Gate2.transform.position;
        aso = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (openGates)
        {
            Vector3 newPos1 = initGatePos1;
            newPos1.x += GateMoveDistance;
            Gate1.transform.position = Vector3.Lerp(Gate1.transform.position, newPos1, 0.05f);
            
            Vector3 newPos2 = initGatePos2;
            newPos2.x -= GateMoveDistance;
            Gate2.transform.position = Vector3.Lerp(Gate2.transform.position, newPos2, 0.05f);
        }
        else
        {
            Gate1.transform.position = Vector3.Lerp(Gate1.transform.position, initGatePos1, 0.1f);
            Gate2.transform.position = Vector3.Lerp(Gate2.transform.position, initGatePos2, 0.1f);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Card") && !swipedCard)
        {
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
                        
            if (otherRB.velocity.z > MinimumCardSpeed)
            {
                StatusLight.color = Color.green;
                aso.PlayOneShot(RightSound);
                Confetti.SetActive(true);
                
                openGates = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Swipe(false);
                StartCoroutine(StatusWindow(2.5f));
            }
            else
            {
                StatusLight.color = Color.yellow;
                aso.PlayOneShot(WrongSound);
                if (otherRB.velocity.z > 0)
                {
                    StatusText.text = "Too Slow";
                }
                else
                {
                    StatusText.text = "Wrong Way";
                }

                StartCoroutine(StatusWindow(1f));
            }
        }
    }

    private IEnumerator StatusWindow(float length)
    {
        swipedCard = true;
        yield return new WaitForSeconds(length);
        swipedCard = false;
        openGates = false;
        StatusLight.color = Color.red;
        StatusText.text = "";
    }
}


