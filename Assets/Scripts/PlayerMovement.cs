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

    const float clampedMovementSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        Vector2 targetVelocity;

        var movement = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(movement));

        if (movement > 0)
        {
            transform.localScale = new Vector3(-1, 0, 0);
        }
        else
            transform.localScale = new Vector3(1, 0, 0);

        targetVelocity = new Vector2(movement, 0) * Time.deltaTime * movementSpeed;

        rigidbody.velocity += targetVelocity;
        rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -clampedMovementSpeed, clampedMovementSpeed), rigidbody.velocity.y);

        Debug.Log($"Target Velocity: { rigidbody.velocity.x}");
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.001f)
        {
            rigidbody.AddForce(new Vector2(rigidbody.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
    }
}
