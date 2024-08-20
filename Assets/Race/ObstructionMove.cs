using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionMove : MonoBehaviour
{
    public float maxSpeed = 7.5f;
    private float speed;
    public GameEvent OnPointAdded;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeSpeed(0, maxSpeed, 3f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0f, 0f);
        if (transform.position.x < -7.5f)
        {
            OnPointAdded.Raise(gameObject, null);
            Destroy(gameObject);
        }
    }

    public void StartRace(GameObject sender, object data)
    {
        //areRacing = true;
        StartCoroutine(changeSpeed(0, maxSpeed, 3f));
    }

    public void StopRace(GameObject sender, object data)
    {
        //areRacing = false;
        StartCoroutine(changeSpeed(speed, 0, 1f));
    }

    public IEnumerator changeSpeed(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            speed = Mathf.Lerp(oldValue, newValue, t / duration);
            yield return null;
        }
        speed = newValue;
    }
}
