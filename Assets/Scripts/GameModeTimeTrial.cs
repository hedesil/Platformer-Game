using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeTimeTrial : MonoBehaviour
{
    public GameManager gameManager;
    public float levelTimer = 300f;

    // Update is called once per frame
    void Update()
    {
        if(levelTimer > 0) {
            levelTimer = levelTimer - Time.deltaTime;
        } else {
            if(gameManager.isGameOver == false) {
                gameManager.isGameOver = true;
                gameManager.player.Die();
            }
        }
    }
}
