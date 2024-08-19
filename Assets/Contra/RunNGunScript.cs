using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunNGunScript : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;

    private bool isLying = false;
    private bool isLookingUp = false;
    private bool isLookingDown = false;

    public GameObject Shot;
    public Transform ShotSpawn;
    public float FireRate;

    private float nextFire;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateLyingAnimation();
        updateLookingUpAnimation();

        if ((Input.GetKey(KeyCode.X) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.F)) && Time.time > nextFire)
        {
            shoot();
        }
    }

    private void shoot()
    {
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
            if (isLying)
            {
                ShotSpawn.position -= new Vector3(0f, 0.5f, 0f);
            }
        }
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
}