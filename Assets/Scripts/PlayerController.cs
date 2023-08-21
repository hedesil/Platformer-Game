using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput = 0f;
    public float speed = 5f;
    public PlayerMovement movement;
    public bool isAlive = true;

    public GameManager manager;
    
    public Animator animator;

    public AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip hurtSound;
    public AudioClip jumpSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {
        // Player movement
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Player Jump
        if (Input.GetButtonDown("Jump") && isAlive == true)
        {
            if (movement.m_Grounded)
            {
                Debug.Log("ACTIVAR TRIGGER SALTO");
                animator.SetTrigger("Jump");
            }
            movement.Jump();
            audioSource.PlayOneShot(jumpSound, 1f);
        }

        // Set Animations
        animator.SetBool("Grounded", movement.m_Grounded);
        animator.SetBool("IsAlive", isAlive);

        if (horizontalInput == 0)
        {
            animator.speed = 1f;
            animator.SetBool("Move", false);
        }
        else
        {
            if (isAlive && movement.m_Grounded)
            {
                animator.speed = 1 * Mathf.Abs(horizontalInput); // Hacer que con un gamePad la velocidad sea progresiva
            }
            animator.SetBool("Move", true);
        }
    }

    void FixedUpdate() // Usamos este FixedUpdate para que el update ocurra independientemente de la gráfica o procesador del ordeandor (en el update simple puede dar errores el movimiento)
    {
        if (isAlive == true)
        {
            movement.Move(horizontalInput * speed * Time.deltaTime); // deltaTime sería como los "por hora", tiempo que transcurre entre cada frame del juego
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            manager.totalCoins++;
            audioSource.PlayOneShot(coinSound, 1f);
        }

        if (collision.gameObject.CompareTag("PoisonedCherry"))
        {
            Destroy(collision.gameObject);
            isAlive = false;
            animator.SetTrigger("Die");
            Debug.Log("Te la comiste");
        }


        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            manager.spawnPoint = collision.gameObject.transform; // Al spawn le asignamos la posición del nuevo cartel
        }

        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            manager.FinishLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Enemy"))
        {
            if (isAlive)
            {
                isAlive = false;
                animator.SetTrigger("Die"); // Pasamos el trigger para que el animator lance el trigger Die
                audioSource.PlayOneShot(hurtSound, 1f);
            }
        }

        if (collision.gameObject.CompareTag("WeakPoint"))
        {
            collision.transform.parent.GetComponent<BoxCollider2D>().enabled = false; // Desactivamos el boxCollider del enemigo para que no nos mate si tocamos sin querer después de matarlo
            Destroy(collision.transform.parent.gameObject);
        }
    }

}
