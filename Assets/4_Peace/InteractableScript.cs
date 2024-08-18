using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InteractableScript : MonoBehaviour
{
    private Light2D lightSource;
    private float lightDuration = 0.5f;
    private float minIntensity = 0.5f;
    private float maxIntensity = 2f;

    private void Awake()
    {
        lightSource = GetComponent<Light2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(changeLightIntensity(lightSource.intensity, maxIntensity, lightDuration));
            Debug.Log("Collided!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(changeLightIntensity(lightSource.intensity, minIntensity, lightDuration));
            Debug.Log("Collided!");
        }
    }

    public IEnumerator changeLightIntensity(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            lightSource.intensity = Mathf.Lerp(oldValue, newValue, t / duration);
            yield return null;
        }
        lightSource.intensity = newValue;
        Debug.Log(lightSource.intensity);
    }
}
