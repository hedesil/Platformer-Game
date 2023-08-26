using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private float positionLeft;
    private float positionRight;
    public Animator animator;

    private bool isMovingRight = true;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        positionLeft = gameObject.transform.position.x - distance;
        positionRight = gameObject.transform.position.x + distance;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Movement", true);
        if (isMovingRight)
        {
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (transform.position.x >= positionRight)
        {
            spriteRenderer.flipX = true;
            isMovingRight = false;
        }

        if (transform.position.x <=  positionLeft)
        {
            spriteRenderer.flipX = false;
            isMovingRight = true;
        }

    }
}
