using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseSound() {
        audioSource.Pause();
        StartCoroutine(WaitAndResumeSound(5f));
    }

    private IEnumerator WaitAndResumeSound(float time) {
        yield return new WaitForSeconds(time);
        audioSource.UnPause();
    }
}
