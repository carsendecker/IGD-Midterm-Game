using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float TimeLimit;
    public Camera EndingCamera;
    public TMP_Text EndText;
    public GameObject Train;

    private float timer;
    private GameObject[] timerObjects;
    private TMP_Text[] timerTexts;
    private bool gameEnded;
    private float trainSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = TimeLimit;
        EndText.enabled = false;
        
        timerObjects = GameObject.FindGameObjectsWithTag("Timer");
        timerTexts = new TMP_Text[timerObjects.Length];
        
        for (int i = 0; i < timerObjects.Length; i++)
        {
            timerTexts[i] = timerObjects[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, TimeLimit);

        foreach (var tText in timerTexts)
        {
            tText.text = "<color=orange>F</color>         " + timer.ToString("F0") + "s";
        }

        if (timer <= 0 && !gameEnded)
        {
            EndGame();
        }

        if (gameEnded)
        {
            trainSpeed = Mathf.Lerp(trainSpeed, 0.75f, 0.003f);
            Train.transform.Translate(-Train.transform.forward * trainSpeed);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Win()
    {
        if (!gameEnded)
        {
            EndText.text = "You made it! \n \n \n <size=20>(press P to play again)</size>";
            EndGame();
        }
    }

    private void EndGame()
    {
        Camera.main.enabled = false;
        EndingCamera.enabled = true;
        gameEnded = true;
        EndText.enabled = true;
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }

}
