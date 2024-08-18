using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;

public class InteractableScript : MonoBehaviour
{
    private Light2D lightSource;
    private bool interactable = false;
    private float lightDuration = 0.5f;
    private float minIntensity = 0.5f;
    private float maxIntensity = 2f;
    public GameEvent onInteraction;
    public GameEvent onInteractableStatusChanged;
    public ToolTip toolTip;

    private void Awake()
    {
        lightSource = GetComponent<Light2D>();
        toolTip = GetComponent<ToolTip>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactable)
            {
                interact();
            }
        }
    }

    private void interact()
    {
        interactable = false;
        onInteraction.Raise(gameObject, toolTip.message);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(changeLightIntensity(lightSource.intensity, maxIntensity, lightDuration));
            interactable = true;
            onInteractableStatusChanged.Raise(gameObject, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(changeLightIntensity(lightSource.intensity, minIntensity, lightDuration));
            interactable = false;
            onInteractableStatusChanged.Raise(gameObject, false);
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
    }
}
