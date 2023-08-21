using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeEnemies : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] enemies;

    // Update is called once per frame
    void Update()
    {
         enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) {
            gameManager.FinishLevel();
        }
    }
}
