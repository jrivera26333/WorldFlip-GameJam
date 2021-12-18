using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Animator animator;

    [SerializeField] float movementSpeed = 1;
    [SerializeField] float jumpForce = 1;

    float timeElapsed = 0;
    const float clampedMovementSpeed = 5;

    bool hasMoved, isLanded;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfIsGrounded();
        Movement();
        Jump();
    }

    private void CheckIfIsGrounded()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed > 1f)
        {
            if (transform.position.y == transform.position.y)
                animator.SetBool("isJumping", false);

            timeElapsed = 0;
        }
    }

    void Movement()
    {
        Vector2 targetVelocity;

        var movement = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(movement));

        if (movement < 0)
        {
            hasMoved = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement > 0)
        {
            hasMoved = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(hasMoved)
        {
            hasMoved = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }


        targetVelocity = new Vector2(movement, 0) * Time.deltaTime * movementSpeed;

        rigidbody.velocity += targetVelocity;
        rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -clampedMovementSpeed, clampedMovementSpeed), rigidbody.velocity.y);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.001f)
        {
            animator.SetBool("isJumping", true);
            rigidbody.AddForce(new Vector2(rigidbody.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
    }
}
