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

    public void PrepareToDie(LayerMask layer)
    {
        Instantiate(Explosion, transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.Euler(0, 0 ,0));
        Instantiate(Explosion, transform.position + new Vector3(0.3f, -0.1f, 0f), Quaternion.Euler(0, 0 ,0));
        Instantiate(Explosion, transform.position + new Vector3(-0.4f, -0.2f, 0f), Quaternion.Euler(0, 0 ,0));
        OnPlayerDeath.Raise(gameObject, LayerMask.LayerToName(layer));
        anim.SetBool("isDead", true);
        boxCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            PrepareToDie(gameObject.layer);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.layer == LayerMask.NameToLayer("Player") && collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) || (gameObject.layer == LayerMask.NameToLayer("Enemy") && collision.gameObject.layer == LayerMask.NameToLayer("Player")))
        {
            PrepareToDie(gameObject.layer);
        }
    }
}
