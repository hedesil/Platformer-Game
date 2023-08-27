using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BotonSalida : MonoBehaviour
{
   private  AudioSource audioSource;
    private RuntimePlatform platform;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Agrega un listener al botón para detectar clics
        Button boton = GetComponent<Button>();
        boton.onClick.AddListener(ReproducirSonidoYSalir);
    }

    private void ReproducirSonidoYSalir()
    {
        // Reproduce el sonido
        audioSource.Play();
        StartCoroutine(WaitAndPauseSound());
        // Pausa durante un breve período para que el sonido se escuche
        // Cambia el valor del tiempo según sea necesario
        Time.timeScale = 0.5f;

        // Asegúrate de que el sonido se reproduzca por un tiempo
        // (ajusta el tiempo de acuerdo a la duración del sonido)
        Invoke("ExitGame", 1.0f);
    }

    private void ExitGame()
    {
        // Restaura el tiempo normal
        Time.timeScale = 1.0f;

        if (platform != RuntimePlatform.WebGLPlayer)
        {
           StartCoroutine(WaitAndExit(1f));
        }
    }

     private IEnumerator WaitAndExit(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }

    private IEnumerator WaitAndPauseSound() {
        yield return new WaitForSeconds(5f);
        audioSource.Pause();
    }
}
