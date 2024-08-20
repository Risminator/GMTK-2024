using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text AIScore;

    private PongLocalAudioManager localAudioManager;
    private SpriteRenderer someSprite;

    private bool isFinished = false;

    private int hitCounter;
    private Rigidbody2D rb;
    public Sprite NewSprite;

    private void Awake()
    {
        GameObject localAudioObject = GameObject.FindGameObjectWithTag("LocalAudio");
        if (localAudioObject != null)
        {
            localAudioManager = localAudioObject.GetComponent<PongLocalAudioManager>();
        }
        else
        {
            Debug.LogWarning("LocalAudioManager not found");
        }
        someSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        if (!isFinished)
        {
            Invoke("StartBall", 2f);
        }
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection, yDirection;
        if(transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<BoxCollider2D>().bounds.size.y;
        if(yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);
        }

        if(collision.gameObject.name == "Player")
        {
            localAudioManager.PlaySFX(localAudioManager.PingAudioClip);
        }

        if (collision.gameObject.name == "AI")
        {
            localAudioManager.PlaySFX(localAudioManager.PongAudioClip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0)
        {
            ResetBall();
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
        }
        else if (transform.position.x < 0)
        {
            ResetBall();
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }

    public void OnFinishGame(GameObject sender, object data)
    {
        someSprite.sprite = NewSprite;
        isFinished = true;
    }

    public void OnFall(GameObject sender, object data)
    {
        StartCoroutine(ZoomOut(transform.position.y, transform.position.y - 20f, 1f));
    }

    private IEnumerator ZoomOut(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = new Vector3(0f, Mathf.Lerp(oldValue, newValue, t / duration), 0f);
            yield return null;
        }
        transform.position = new Vector3(0f, newValue, 0f);
        // оепеирх мю якедсчысч яжемс
    }
}
