using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public int totalCoins = 0;
    public int lives = 3;

    public Transform spawnPoint;
    public PlayerController player;
    public float timeToRespawn = 2f;
    public float timer = 0;
    public bool isGameOver = false;
    public bool isLevelFinished = false;

    public TextMeshProUGUI lifesText;

    public TextMeshProUGUI beersText;

    public GameObject levelEndPanel;

    public TextMeshProUGUI helpText;

    private string originalHelpText = "Consigue llegar a casa bebiendo el mayor número de Steinburg sin que te atrape Fermín";

    public TextMeshProUGUI levelEndText;


    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isAlive == false)
        {
            if (timer < timeToRespawn)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                if (lives >= 1)
                {
                    lives--;
                    player.transform.position = spawnPoint.transform.position;
                    player.isAlive = true;
                    player.playerCanMove = true;
                    timer = 0;
                    UpdateHelpText(originalHelpText);
                    Debug.Log("Te quedan " + lives + " vidas.");
                }
                else
                {
                    isGameOver = true;
                }
            }
        }

        if (isGameOver == true || isLevelFinished == true)
        {
            levelEndPanel.SetActive(true); // Activamos el panel del game over

            if (isGameOver)
            {
                levelEndText.text = "GAME OVER";
            }
            else if (isLevelFinished)
            {
                levelEndText.text = "FINISHED";
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.touchCount > 0)
            {
                SceneManager.LoadScene(0);
            }
        }

        lifesText.text = "x" + lives;
        beersText.text = "x" + totalCoins;
    }

    public void FinishLevel()
    {
        isLevelFinished = true;
    }

    public void UpdateHelpText(string text) {
        helpText.text = text; 
    }


    
    
}

