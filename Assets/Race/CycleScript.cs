using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CycleScript : MonoBehaviour
{
    public bool IsControllable = false;
    public GameEvent OnRaceStart;
    public GameEvent OnPlayerDeath;
    public GameEvent OnFinish;
    private SpriteRenderer sprite;
    public float IFramesSec = 2f;
    private static int score = 0;

    public enum Road
    {
        Top,
        Middle,
        Bottom,
    }
    private Road _road = Road.Middle;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move(4.4f, 0, 4f));
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

    public IEnumerator Move(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = new Vector3(-5f, Mathf.Lerp(oldValue, newValue, t / duration), 0f);
            yield return null;
        }
        transform.position = new Vector3 (-5f, newValue, 0f);
        GetControl();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnPlayerDeath.Raise(gameObject, "Player");
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator GetVulnerableWithDelay()
    {
        yield return new WaitForSeconds(IFramesSec);
        gameObject.layer = LayerMask.NameToLayer("Player");
        sprite.color = new Color(1, 1, 1, 1);
    }

    public void OnPointAdded()
    {
        score++;
        if (score == 20)
        {
            OnFinish.Raise(gameObject, null);
        }
    }
}
