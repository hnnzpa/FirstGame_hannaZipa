using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private CapsuleCollider2D col;
    private Animator _anim;
    private SpriteRenderer _sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();   
    }

    void Update()
    {
        // Funciona con teclado (A/D, flechas) Y mando (joystick izquierdo) automáticamente
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
        
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        float absSpeed = Mathf.Abs(horizontal);
        _anim.SetFloat("SpeedX", absSpeed);
        
        //Flip de la animació
        if (horizontal > 0.01f) _sr.flipX = false;
        else if (horizontal < -0.01f) _sr.flipX = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.CapsuleCast(
            col.bounds.center,
            col.size,
            col.direction,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );
    }
}