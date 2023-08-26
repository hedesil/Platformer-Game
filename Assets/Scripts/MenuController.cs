using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadGameLevel(int levelId)
    {
        SceneManager.LoadScene(levelId);
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
