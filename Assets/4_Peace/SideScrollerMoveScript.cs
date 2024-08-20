using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SideScrollerMoveScript : MonoBehaviour
{
    protected Rigidbody2D body;
    protected SpriteRenderer sprite;

    public float MaxSpeed = 10f;
    public float Accel = 5f;
    public float Friction = 5f;
    public Vector2 BoxSize;
    public float CastDistance;
    public LayerMask GroundLayer;

    private AudioSource audioSource;

    public AudioClip contraAudioClip;

    public AudioClip PeaceIntroAudioClip;
    public AudioClip PeaceFirstLoopAudioClip;
    public AudioClip PeaceSecondLoopAudioClip;
    public AudioClip PeaceEndAudioClip;

    public float jumpVelocity = 6f;

    public float CoyoteTime = 0.2f;
    protected float coyoteTimeCounter;

    protected float jumpBufferTime = 0.2f;
    protected float jumpBufferTimeCounter;

    protected bool Grounded;
    protected Animator animator;

    private GameObject player;

    public bool IsControllable = true;
    public bool IsPlayer = true;

    public bool isContra;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        if (!isContra)
        {
            StartCoroutine(StartPlaySoundtrack(PeaceIntroAudioClip, PeaceFirstLoopAudioClip));
        }

        if (!IsPlayer)
        {
            StartCoroutine(jumpRegularly());
        }
    }

    IEnumerator StartPlaySoundtrack(AudioClip introClip, AudioClip loopClip)
    {
        yield return PlayClip(introClip, false);
        yield return PlayClip(loopClip, true);
    }

    IEnumerator PlayClip(AudioClip clip, bool loop)
    {
        if (audioSource.isPlaying)
        {
            audioSource.loop = false;

            while (audioSource.isPlaying)
            {
                yield return null;
            }
        }

        audioSource.loop = loop;
        audioSource.clip = clip;
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }
    }


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsControllable)
        {
            calculateHorizontalMovement();
            checkVerticalMovement();
        }
    }

    private void calculateHorizontalMovement()
    {
        Debug.Log(Vector2.Distance(player.transform.position, transform.position));
        float direction;
        if (IsPlayer)
        {
            direction = Input.GetAxisRaw("Horizontal");
        }
        else if (Vector2.Distance(player.transform.position, transform.position) < 7f)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 0.01)
            {
                direction = 0;
            }
            else
            {
                direction = Mathf.Sign(player.transform.position.x - transform.position.x);
            }
        }
        else
        {
            direction = 0;
        }
        
        animator.SetBool("isMoving", direction != 0);
        // Moving
        if (direction != 0)
        {
            // In Contra there is no acceleration
            if (Accel == 0)
            {
                body.velocity = new Vector2(MaxSpeed * Mathf.Sign(direction), body.velocity.y);
            }
            else
            {
                body.velocity += new Vector2(direction * Accel * Time.deltaTime, 0);
                body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -MaxSpeed, MaxSpeed), body.velocity.y);
            }
            // Sprite flip
            if (direction < 0 != sprite.flipX)
            {
                // Резкие повороты body.velocity = new Vector2(-body.velocity.x, body.velocity.y);
                sprite.flipX = direction < 0;
            }
        }
        else
        {
            // Slowing down (friction)
            if (Mathf.Abs(body.velocity.x) > Friction * Time.deltaTime && Accel != 0)
            {
                body.velocity -= new Vector2(Mathf.Sign(body.velocity.x) * Friction * Time.deltaTime, 0);
            }
            else
            // Full Stop
            {
                body.velocity = new Vector2(0, body.velocity.y);
            }
        }

        animator.SetFloat("xVelocity", Mathf.Abs(body.velocity.x));
    }

    private void checkVerticalMovement()
    {
        if (IsPlayer)
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

            if (Accel != 0 && Input.GetButtonUp("Jump") && body.velocity.y > 0f)
            {
                accelerateFall();
                coyoteTimeCounter = 0f;
            }
        }
        else
        {
            isGrounded();
        }

        animator.SetFloat("yVelocity", body.velocity.y);
    }

    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpVelocity);
        //body.AddForce(new Vector2(body.velocity.x, jumpVelocity * 10));
    }

    private IEnumerator jumpRegularly()
    {
        for (; ; )
        {
            if (isGrounded()) jump();
            yield return new WaitForSeconds(5f);
        }
    }

    private void accelerateFall()
    {
        body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.5f);
    }

    private bool isGrounded()
    {
        Grounded = Physics2D.BoxCast(transform.position, BoxSize, 0, -transform.up, CastDistance, GroundLayer);
        animator.SetBool("isJumping", !Grounded);
        return Grounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * CastDistance, BoxSize);
    }
}
