using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float maxSpeed;
    private float speed;

    [SerializeField]
    private Renderer bgRenderer;

    public RaceLocalAudioManager localAudioManager;

    public float minPitch = 0.5f; 
    public float maxPitch = 1.5f; 

    // Start is called before the first frame update
    void Start()
    {
        if (localAudioManager == null)
        {
            localAudioManager = FindObjectOfType<RaceLocalAudioManager>();
            if (localAudioManager == null)
            {
                Debug.LogWarning("RaceLocalAudioManager not found.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }

    public void StartRace(GameObject sender, object data)
    {
        localAudioManager.LocalEngineSource.Play();
        StartCoroutine(changeSpeed(0, maxSpeed, 3f));
    }

    public void StopRace(GameObject sender, object data)
    {
        StartCoroutine(changeSpeed(speed, 0, 1f));
    }

    public IEnumerator changeSpeed(float oldValue, float newValue, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            speed = Mathf.Lerp(oldValue, newValue, t / duration);

            if (localAudioManager != null && localAudioManager.LocalEngineSource != null)
            {
                float normalizedSpeed = speed / maxSpeed; 
                localAudioManager.LocalEngineSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed); 
            }
            yield return null;
        }
        speed = newValue;

        if (localAudioManager != null && localAudioManager.LocalEngineSource != null)
        {
            float finalNormalizedSpeed = speed / maxSpeed;
            localAudioManager.LocalEngineSource.pitch = Mathf.Lerp(minPitch, maxPitch, finalNormalizedSpeed);
        }
    }
}