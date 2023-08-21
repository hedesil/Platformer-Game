using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                if (lives > 0)
                {
                    lives--;
                    player.transform.position = spawnPoint.transform.position;
                    player.isAlive = true;
                    timer = 0;
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Load Main Menu
            }
        }
    }

    public void FinishLevel()
    {
        isLevelFinished = true;
    }
}

