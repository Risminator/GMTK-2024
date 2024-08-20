using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public GameObject Explosion;
    private Animator anim;
    private BoxCollider2D boxCollider;
    public GameEvent OnPlayerDeath;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Instantiate(Explosion, transform.position, Quaternion.Euler(0, 0 ,0));
        anim.SetBool("isDead", true);
        boxCollider.enabled = false;
        OnPlayerDeath.Raise(gameObject, null);
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Player" && collision.transform.tag == "Enemy")
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((tag == "Player" && collision.transform.tag == "Enemy") || (tag == "Enemy" && collision.transform.tag == "Player"))
        {
            Die();
        }
    }
}
