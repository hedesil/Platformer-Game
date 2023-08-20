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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Enemy"))
        {
            isAlive = false;
        }
    }

}
