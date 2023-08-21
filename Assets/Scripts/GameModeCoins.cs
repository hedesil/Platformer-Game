using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeCoins : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] cherries;

    // Update is called once per frame
    void Update()
    {
        cherries = GameObject.FindGameObjectsWithTag("Cherry");
        if (cherries.Length == 0) {
            gameManager.FinishLevel();
        }
    }
}
