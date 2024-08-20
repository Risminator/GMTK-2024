using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContraLocalAudioManager : MonoBehaviour
{
    private AudioManager audioManager;

    public AudioSource LocalSFXSource;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
}
