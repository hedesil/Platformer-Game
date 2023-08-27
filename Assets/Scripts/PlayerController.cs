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
    private bool leftButtonTap = false;
    private bool rightButtonTap = false;

    public bool jumpButtonTap = false;

    public bool isAlive = true;

    public GameManager manager;

    public Animator animator;

    public AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip hurtSound;
    public AudioClip jumpSound;

    public bool playerCanMove = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {
        // Player Jump
        PlayerJump();
        // Set Animations
        animator.SetBool("Grounded", movement.m_Grounded);
        animator.SetBool("IsAlive", isAlive);

    }

    void FixedUpdate() // Usamos este FixedUpdate para que el update ocurra independientemente de la gráfica o procesador del ordeandor (en el update simple puede dar errores el movimiento)
    {
        
        if (isAlive == true && playerCanMove == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
                PlayerMovement(horizontalInput);
            }
            else
            {
                PlayerMovement(CalculateMovementDirection());
            }
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
            Die();
            Destroy(collision.gameObject);
            manager.UpdateHelpText("Has pisao el sarmiento y has echao una potica");
        }

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            manager.spawnPoint = collision.gameObject.transform; // Al spawn le asignamos la posición del nuevo cartel
        }

        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            playerCanMove = false;
            animator.SetBool("Move", false);
            movement.Stop();
            manager.FinishLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isAlive)
            {
                manager.UpdateHelpText("No puedes escapar Tomasín wa-ja-ja-ja");
                Die();
            }
        }

        if (collision.gameObject.CompareTag("WeakPoint"))
        {
            collision.transform.parent.GetComponent<BoxCollider2D>().enabled = false; // Desactivamos el boxCollider del enemigo para que no nos mate si tocamos sin querer después de matarlo
            Destroy(collision.transform.parent.gameObject);
        }
    }

    public void Die()
    {
        movement.Stop();
        playerCanMove = false;
        isAlive = false;
        animator.SetBool("Move", false);
        animator.SetTrigger("Die"); // Pasamos el trigger para que el animator lance el trigger Die
        audioSource.PlayOneShot(hurtSound, 1f);
    }

    private void PlayerJump()
    {
        if ((Input.GetButtonDown("Jump") || jumpButtonTap == true) && isAlive == true && playerCanMove == true)
        {
            jumpButtonTap = false;
            if (movement.m_Grounded == true)
            {
                animator.SetTrigger("Jump");
                movement.Jump();
            }
            audioSource.PlayOneShot(jumpSound, 1f);
        }
    }

    private void PlayerMovement(float direction)
    {
        if (direction == 0)
        {
            animator.speed = 1f;
            animator.SetBool("Move", false);
        }
        else
        {
            if (isAlive && movement.m_Grounded)
            {
                animator.speed = 1 * Mathf.Abs(direction); // Hacer que con un gamePad la velocidad sea progresiva
                animator.SetBool("Move", true);

            }
        }
        movement.Move(direction * speed * Time.deltaTime); // deltaTime sería como los "por hora", tiempo que transcurre entre cada frame del juego
    }

    public void MoveLeft()
    {
        if (isAlive == true)
        {
            leftButtonTap = true;
        }
    }

    public void MoveRight()
    {
        if (isAlive == true)
        {
            rightButtonTap = true;
        }
    }

    public void Jump()
    {
        if (isAlive == true)
        {
            jumpButtonTap = true;
        }
    }

    public void StopMovement()
    {
        horizontalInput = 0f;
        leftButtonTap = false;
        rightButtonTap = false;
        jumpButtonTap = false;
    }

    private float CalculateMovementDirection()
    {
        if (leftButtonTap == true)
        {
            return -1f;
        }
        else if (rightButtonTap == true)
        {
            return 1f;
        }
        else
        {
            return 0f;
        }
    }
}
