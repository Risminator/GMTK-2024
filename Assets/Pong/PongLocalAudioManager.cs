using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongLocalAudioManager : MonoBehaviour
{
    public AudioSource LocalSFXSource;

    public AudioClip PingAudioClip;
    public AudioClip PongAudioClip;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
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
