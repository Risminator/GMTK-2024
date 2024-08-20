using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class RunNGunScript : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;

    private ContraLocalAudioManager localAudioManager;

    private bool isLying = false;
    private bool isLookingUp = false;
    private bool isLookingDown = false;

    public Bullet Shot;
    public Transform ShotSpawn;
    public float ShotSpeed = 20f;
    public float FireRate = 0.125f;
    public float BulletTime = 1f;
    public float IFramesSec = 2f;

    private float nextFire;
    private bool isDead = false;
    private Rigidbody2D body;

    public static RunNGunScript _instance;

    private void Awake()
    {
        GameObject localAudioObject = GameObject.FindGameObjectWithTag("LocalAudio");
        if (localAudioObject != null)
        {
            localAudioManager = localAudioObject.GetComponent<ContraLocalAudioManager>();
        }
        else
        {
            Debug.LogWarning("LocalAudioManager not found");
        }

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
            body = GetComponent<Rigidbody2D>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite.color = new Color(1, 1, 1, 0.5f);
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        StartCoroutine(GetVulnerableWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            updateLyingAnimation();
            updateLookingUpAnimation();

            if ((Input.GetKey(KeyCode.X) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.F)) && Time.time > nextFire)
            {
                shoot();
            }
        }
    }

    private void shoot()
    {

        localAudioManager.PlaySFX(localAudioManager.shot);
        Quaternion shotDirection;
        nextFire = Time.time + FireRate;
        if (isLookingUp)
        {
            shotDirection = Quaternion.Euler(0, 0, 0);
            ShotSpawn.position = transform.position + transform.up * sprite.size.y / 10f;
        }
        else if (checkLookingDown())
        {
            shotDirection = Quaternion.Euler(0, 0, 180);
            ShotSpawn.position = transform.position;
        }
        else
        {
            int shotAxis = sprite.flipX ? 1 : -1;
            shotDirection = Quaternion.Euler(0, 0, 90 * shotAxis);
            ShotSpawn.position = transform.position + transform.right * (-shotAxis) * sprite.size.x / 100f;
            if (isLying && body.velocity.x == 0)
            {
                ShotSpawn.position -= new Vector3(0f, 0.5f, 0f);
            }
        }
        Shot.speed = ShotSpeed;
        Shot.TimeToLive = BulletTime;
        Shot.gameObject.layer = LayerMask.NameToLayer("Player");
        Instantiate(Shot, ShotSpawn.position, shotDirection);
    }

    private bool checkLookingDown()
    {
        return animator.GetBool("isJumping") && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S));
    }

    private void updateLyingAnimation()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            isLying = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            isLying = false;
        }

        animator.SetBool("isLying", isLying);
    }

    private void updateLookingUpAnimation()
    {
        if (!isLying && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            isLookingUp = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            isLookingUp = false;
        }

        animator.SetBool("isLookingUp", isLookingUp);
    }

    public void Destruct()
    {
            Destroy(gameObject);
    }

    public void OnDeath(GameObject sender, object data)
    {
        if (data is string && (string)data == "Player")
        {
            localAudioManager.PlaySFX(localAudioManager.death);
            isDead = true;
        }
    }

    private IEnumerator GetVulnerableWithDelay()
    {
        yield return new WaitForSeconds(IFramesSec);
        gameObject.layer = LayerMask.NameToLayer("Player");
        sprite.color = new Color(1, 1, 1, 1);
    }
}