using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class CycleScript : MonoBehaviour
{
    public bool IsControllable = false;
    public GameEvent OnRaceStart;
    public GameEvent OnPlayerDeath;
    public GameEvent OnFinish;
    private SpriteRenderer sprite;
    public float IFramesSec = 2f;
    private Rigidbody2D rb;

    private AudioManager audioManager;

    private RaceLocalAudioManager localAudioManager;

    public enum Road
    {
        Top,
        Middle,
        Bottom,
    }
    private Road _road = Road.Middle;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        GameObject localAudioObject = GameObject.FindGameObjectWithTag("LocalAudio");
        if (localAudioObject != null)
        {
            localAudioManager = localAudioObject.GetComponent<RaceLocalAudioManager>();
        }
        else
        {
            Debug.LogWarning("LocalAudioManager not found");
        }

        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move(4.4f, 0, 4f, 1));
        sprite.color = new Color(1, 1, 1, 0.5f);
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        StartCoroutine(GetVulnerableWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsControllable)
        {
            updatePosition();
        }
    }

    public void GetControl()
    {

        IsControllable = true;
        OnRaceStart.Raise(gameObject, null);
    }

    private void updatePosition()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W)) && _road != Road.Top))
        {
            _road--;
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.S)) && _road != Road.Bottom))
        {
            _road++;
        }


        switch (_road)
        {
            case Road.Top:
                transform.position = new Vector3(-5f, 1.3f, 0f);
                break;
            case Road.Middle:
                transform.position = new Vector3(-5f, 0f, 0f);
                break;
            case Road.Bottom:
                transform.position = new Vector3(-5f, -1.2f, 0f);
                break;
        }
    }

    public IEnumerator Move(float oldValue, float newValue, float duration, int i)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            if (i == 1)
            {
                transform.position = new Vector3(-5f, Mathf.Lerp(oldValue, newValue, t / duration), 0f);
            }
            else if (i == 0)
            {
                transform.position = new Vector3(Mathf.Lerp(oldValue, newValue, t / duration), 0f, 0f);
            }

            yield return null;
        }
        transform.position = new Vector3 (-5f, newValue, 0f);
        GetControl();
    }

    public IEnumerator MoveFinishing(float oldValue, float newValue, float duration, int i)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            if (i == 1)
            {
                transform.position = new Vector3(-5f, Mathf.Lerp(oldValue, newValue, t / duration), 0f);
            }
            else if (i == 0)
            {
                transform.position = new Vector3(Mathf.Lerp(oldValue, newValue, t / duration), 0f, 0f);
            }

            yield return null;
        }
        transform.position = new Vector3(-5f, newValue, 0f);
        SceneManager.LoadScene("Contra");
        //GetControl();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnPlayerDeath.Raise(gameObject, "Player");
        Die();
    }

    private void Die()
    {
        localAudioManager.PlaySFX(localAudioManager.crash);
        Destroy(gameObject);
    }

    private IEnumerator GetVulnerableWithDelay()
    {
        yield return new WaitForSeconds(IFramesSec);
        gameObject.layer = LayerMask.NameToLayer("Player");
        sprite.color = new Color(1, 1, 1, 1);
    }

    public void OnFinishGame(GameObject sender, object data)
    {
        sprite.color = new Color(1, 1, 1, 0.5f);
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        StartCoroutine(MoveFinishing(-5f, 10f, 3f, 0));
    }
}
