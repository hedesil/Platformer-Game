using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour
{
    private RuntimePlatform platform;

    //public AudioSource audioSource;

    public void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //audioSource.Play();
        platform = Application.platform;
        Debug.Log("Plataforma: " + platform);
    }

    public void LoadGameLevel(int levelId)
    {
        SceneManager.LoadScene(levelId);
    }

    public void ExitGame()
    {
        if (platform == RuntimePlatform.WebGLPlayer)
        {

            Debug.Log("Platform: " + platform);
            Debug.Log("Comparando con: " + RuntimePlatform.WebGLPlayer);
            Debug.Log("Estás en Web y no hace nada");
        }
        else if (platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("Platform: " + platform);
            Debug.Log("Comparando con: " + RuntimePlatform.WindowsEditor);
            Debug.Log("Saliendo del juego");
            Application.Quit();
        }
    }

    public void PauseBackgroundAudio () {
         //audioSource.Pause();
          StartCoroutine(ResumeAudio(1f));
    }

    private IEnumerator ResumeAudio(float time) {
        yield return new WaitForSeconds(time);
        //audioSource.UnPause();
    }
}
