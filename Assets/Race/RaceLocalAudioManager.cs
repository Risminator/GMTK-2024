using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceLocalAudioManager : MonoBehaviour
{
    public AudioSource LocalSFXSource;
    public AudioSource LocalEngineSource;

    private AudioManager audioManager;

    public AudioClip crash;
    public AudioClip engine;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        LocalSFXSource.volume -= 0.8f;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlaySoundtrack(audioManager.RaceFirstLoopAudioClip, audioManager.RaceSecondLoopAudioClip);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(AudioClip clip)
    {
        LocalSFXSource.PlayOneShot(clip);
    }
}
