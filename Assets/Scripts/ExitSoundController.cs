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
        StartCoroutine(WaitAndExit());
    }

    private IEnumerator WaitAndExit() {
        yield return new WaitForSeconds(5f);
        audioSource.Pause();
    }
}
