using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerMoveScript : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sprite;

    public float MaxSpeed = 10f;
    public float Accel = 5f;
    public float Friction = 5f;
    public Vector2 BoxSize;
    public float CastDistance;
    public LayerMask GroundLayer;

    public float jumpVelocity = 6f;

    private bool isControllable = true;

    public float CoyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateHorizontalMovement();
        checkVerticalMovement();
    }

    private void calculateHorizontalMovement()
    {
        float direction = Input.GetAxis("Horizontal");
        // Moving
        if (direction != 0)
        {
            body.velocity += new Vector2(direction * Accel * Time.deltaTime, 0);
            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -MaxSpeed, MaxSpeed), body.velocity.y);
            if (direction < 0 != sprite.flipX)
            {
                // Резкие повороты body.velocity = new Vector2(-body.velocity.x, body.velocity.y);
                sprite.flipX = direction < 0;
            }
        }
        else
        {
            // Slowing down (friction)
            if (Mathf.Abs(body.velocity.x) > Friction * Time.deltaTime)
            {
                body.velocity -= new Vector2(Mathf.Sign(body.velocity.x) * Friction * Time.deltaTime, 0);
            }
            else
            // Full Stop
            {
                body.velocity = new Vector2(0, body.velocity.y);
            }
        }
    }

    private void checkVerticalMovement()
    {
        // CoyoteTimeCounter update
        if (isGrounded()) coyoteTimeCounter = CoyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;

        // JumpBufferTimeCounter update
        if (Input.GetButtonDown("Jump")) jumpBufferTimeCounter = jumpBufferTime;
        else jumpBufferTimeCounter -= Time.deltaTime;

        // Jump check
        if (coyoteTimeCounter > 0f && jumpBufferTimeCounter > 0)
        {
            jump();
        }

        if (Input.GetButtonUp("Jump") && body.velocity.y > 0f)
        {
            accelerateFall();
            coyoteTimeCounter = 0f;
        }
    }

    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        //body.AddForce(new Vector2(body.velocity.x, jumpVelocity * 10));
    }

    private void accelerateFall()
    {
        body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(transform.position, BoxSize, 0, -transform.up, CastDistance, GroundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up*CastDistance, BoxSize);
    }
}
