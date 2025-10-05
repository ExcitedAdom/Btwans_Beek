using UnityEngine;

public class playerMo : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private bool isGrounded = false;
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- Move Left/Right ---
        float move = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        rb.linearVelocity = new UnityEngine.Vector2(move * moveSpeed, rb.linearVelocity.y);

        // --- Jump ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new UnityEngine.Vector2(rb.linearVelocityX, jumpForce);
            isGrounded = false; // prevent double jump

        }
        if (move > 0)
            spriteRenderer.flipX = false; // facing right
        else if (move < 0)
            spriteRenderer.flipX = true;  // facing left
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Simple ground check: if we touch something tagged "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
}
