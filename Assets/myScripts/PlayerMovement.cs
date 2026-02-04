using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    private Vector2 movement;
    private Animator animator;
    private float xPostLastFrame;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        bool isMoving = Mathf.Abs(input) > 0.01f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        movement.x = input * currentSpeed * Time.deltaTime;
        movement.y = 0;

        transform.Translate(movement);
        FlipCharacter();

        // Animator control
        animator.SetBool("isWalking", isMoving && !isRunning);
        animator.SetBool("isRunning", isMoving && isRunning);
    }

    private void FlipCharacter()
    {
        if (transform.position.x > xPostLastFrame)
        {
            //moving right
            spriteRenderer.flipX = false;
        }

        else if (transform.position.x < xPostLastFrame)
        {
            //moving left
            spriteRenderer.flipX = true;
        }

        xPostLastFrame = transform.position.x;
    }
}
