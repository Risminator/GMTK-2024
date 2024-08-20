using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongCamera : MonoBehaviour
{
    private Camera cam;
    public GameEvent OnFall;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartZoomOut(GameObject sender, object data)
    {
        StartCoroutine(ZoomOut(5, 12, 3f));
    }

    private IEnumerator ZoomOut(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            cam.orthographicSize = Mathf.Lerp(oldValue, newValue, t / duration);
            yield return null;
        }
        cam.orthographicSize = newValue;
        OnFall.Raise(gameObject, null);
    }
}
