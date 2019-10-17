using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
	public Transform CamPosInstructions;
	public Image BlackoutImage;
	
    private Camera cam;
	private bool toInstructions;
	private bool toMenu;
	private bool startingGame;
	
	private Vector3 originalPos;
	private Quaternion originalRot;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
	    originalPos = cam.transform.position;
	    originalRot = cam.transform.rotation;
	    BlackoutImage.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
	    if (toInstructions)
	    {
		    cam.transform.position = Vector3.Lerp(cam.transform.position, CamPosInstructions.position, 0.05f);
		    cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, CamPosInstructions.rotation, 0.05f);
	    }
	    else if (toMenu)
	    {
		    cam.transform.position = Vector3.Lerp(cam.transform.position, originalPos, 0.05f);
		    cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, originalRot, 0.05f);
	    }

	    if (startingGame)
	    {
		    BlackoutImage.color = Color.Lerp(BlackoutImage.color, Color.black, 0.05f);
	    }
    }

    public void PanToInstructions()
    {
	    toMenu = false;
	    toInstructions = true;
    }

	public void PanToMenu()
	{
		toMenu = true;
		toInstructions = false;
	}

	public void StartGame()
	{
		if (!startingGame)
		{
			StartCoroutine(Transition());
		}
	}

	IEnumerator Transition()
	{
		startingGame = true;
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene("Main");
	}

}
