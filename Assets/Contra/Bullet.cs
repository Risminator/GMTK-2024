using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    public float TimeToLive = 1f;
    public bool IsPlayer = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("Ready", false);
        // Звук выстрела здесь!
        // ..................

        rb.velocity = transform.up * speed;
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(TimeToLive);
        anim.SetBool("Ready", true);
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPlayer && collision.transform.tag == "Enemy")
        {
            
        }
        else if (!IsPlayer && collision.transform.tag == "Player")
        {
            
        }
    }
}
