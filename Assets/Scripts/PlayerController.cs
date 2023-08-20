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
            movement.Jump();
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

        }

        if (collision.gameObject.CompareTag("PoisonedCherry"))
        {
            Destroy(collision.gameObject);
            isAlive = false;
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
        if (collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PoisonedCherry"))
        {
            isAlive = false;
        }

        if (collision.gameObject.CompareTag("WeakPoint"))
        {
            collision.transform.parent.GetComponent<BoxCollider2D>().enabled = false; // Desactivamos el boxCollider del enemigo para que no nos mate si tocamos sin querer después de matarlo
            Destroy(collision.transform.parent.gameObject);
        }
    }

}
