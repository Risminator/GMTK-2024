using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animation anim;
    public float speed;
    public float TimeToLive = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Звук выстрела здесь!
        // ..................

        rb.velocity = transform.up * speed;
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(TimeToLive);
        Destruct();
    }

    private void Destruct()
    {

        Destroy(gameObject);
    }
}
