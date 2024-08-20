using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBot : MonoBehaviour
{
    public Bullet Shot;
    public Transform ShotSpawn;
    public float ShotSpeed = 10f;
    public float FireRate = 2f;
    public float BulletTime = 5f;

    private float nextFire;

    private GameObject player;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool isLookingUp;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootRegularly());
    }

    // Update is called once per frame
    void Update()
    {
        isLookingUp = player.transform.position.y > transform.position.y;
        animator.SetBool("isLookingUp", isLookingUp);

        if (Time.time > nextFire)
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
        else
        {
            int shotAxis = sprite.flipX ? 1 : -1;
            shotDirection = Quaternion.Euler(0, 0, 90 * shotAxis);
            ShotSpawn.position = transform.position + transform.right * (-shotAxis) * sprite.size.x / 100f;
        }
        Shot.speed = ShotSpeed;
        Shot.TimeToLive = BulletTime;
        Shot.tag = "Enemy";
        Instantiate(Shot, ShotSpawn.position, shotDirection);
    }

    private IEnumerator ShootRegularly()
    {
        for (; ; )
        {
            shoot();
            yield return new WaitForSeconds(2f);
        }
    }
}
