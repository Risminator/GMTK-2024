using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAudioManager : MonoBehaviour
{
    private AudioManager audioManager;

    public AudioSource LocalSFXSource;
    public AudioSource LocalAmbienceSource;

    public AudioClip wind;
    public AudioClip step1;
    public AudioClip step2;
    public AudioClip step3;
    public AudioClip step4;
    public AudioClip step5;
    public AudioClip step6;
    public AudioClip step7;

    private Coroutine stepCoroutine;
    private Coroutine ambiencePanCoroutine;
    private AudioClip[] stepClips;
    private Queue<AudioClip> recentStepsQueue = new Queue<AudioClip>();
    private int maxRecentSteps = 3;

    private void Awake()
    {
        LocalSFXSource.volume = 0.25f;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        stepClips = new AudioClip[] { step1, step2, step3, step4, step5, step6, step7 };
    }

    void Start()
    {
        PlayAmbience(wind);
        audioManager.PlaySoundtrack(audioManager.PeaceIntroAudioClip, audioManager.PeaceFirstLoopAudioClip);
        StartAmbiencePanning();
    }

    private void PlayAmbience(AudioClip clip)
    {
        LocalAmbienceSource.clip = clip;
        LocalAmbienceSource.loop = true;
        LocalAmbienceSource.volume = 0.7f;
        LocalAmbienceSource.Play();
    }

    private void StartAmbiencePanning()
    {
        if (ambiencePanCoroutine == null)
        {
            ambiencePanCoroutine = StartCoroutine(AmbiencePanningRoutine());
        }
    }

    private IEnumerator AmbiencePanningRoutine()
    {
        float panSpeed = 1.5f; 
        float panRange = 0.5f;

        while (true)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * panSpeed;
                LocalAmbienceSource.panStereo = Mathf.Lerp(-panRange, panRange, Mathf.PingPong(t, 1));
                yield return null;
            }
        }
    }

    public void StartPlayingSteps()
    {
        if (stepCoroutine == null)
        {
            stepCoroutine = StartCoroutine(PlayStepsRoutine());
        }
    }

    public void StopPlayingSteps()
    {
        if (stepCoroutine != null)
        {
            StopCoroutine(stepCoroutine);
            stepCoroutine = null;
        }
    }

    private IEnumerator PlayStepsRoutine()
    {
        while (true)
        {
            PlayStep();
            yield return new WaitForSeconds(0.55f);
        }
    }

    public void PlayStep()
    {
        if (stepClips.Length == 0) return;

        AudioClip selectedClip = null;
        int attempts = 0;
        do
        {
            int randomIndex = Random.Range(0, stepClips.Length);
            selectedClip = stepClips[randomIndex];
            attempts++;
        }
        while (recentStepsQueue.Contains(selectedClip) && attempts < 10);

        LocalSFXSource.PlayOneShot(selectedClip);

        recentStepsQueue.Enqueue(selectedClip);

        if (recentStepsQueue.Count > maxRecentSteps)
        {
            recentStepsQueue.Dequeue();
        }
    }
}