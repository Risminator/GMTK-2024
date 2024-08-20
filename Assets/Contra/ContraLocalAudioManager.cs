using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContraLocalAudioManager : MonoBehaviour
{
    private AudioManager audioManager;

    public AudioSource LocalSFXSource;

    [Header("")]
    public AudioClip shot;
    public AudioClip death;
    public AudioClip explosion1;
    public AudioClip explosion2;
    public AudioClip explosion3;

    private AudioClip[] explosionsClips;
    private AudioClip lastPlayedExplosion;

    private void Awake()
    {
        LocalSFXSource.volume -= 0.8f;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        explosionsClips = new AudioClip[] { explosion1, explosion2, explosion3 };
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlaySoundtrack(audioManager.ContraIntroAudioClip, audioManager.ContraFirstLoopAudioClip);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySFX(AudioClip clip)
    {
        LocalSFXSource.PlayOneShot(clip);
    }

    public void PlayRandomExplosion()
    {
        AudioClip selectedExplosion;
        do
        {
            selectedExplosion = explosionsClips[Random.Range(0, explosionsClips.Length)];
        } while (selectedExplosion == lastPlayedExplosion);

        PlaySFX(selectedExplosion);
        lastPlayedExplosion = selectedExplosion;
    }
}