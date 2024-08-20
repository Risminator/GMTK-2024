using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    //[SerializeField] AudioSource SFXSource;

    [Header("Race Soundtrack")]
    public AudioClip RaceFirstLoopAudioClip;
    public AudioClip RaceSecondLoopAudioClip;

    [Header("Contra Soundtrack")]
    public AudioClip ContraIntroAudioClip;
    public AudioClip ContraFirstLoopAudioClip;
    public AudioClip ContraSecondLoopAudioClip;

    [Header("Peace Soundtrack")]
    public AudioClip PeaceIntroAudioClip;
    public AudioClip PeaceFirstLoopAudioClip;
    public AudioClip PeaceSecondLoopAudioClip;
    public AudioClip PeaceEndAudioClip;

    void Start()
    {
        //musicSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        
    }

    public void PlaySoundtrack(AudioClip introClip, AudioClip loopClip)
    {
        StartCoroutine(PlayIntro(introClip, loopClip));
    }


    private IEnumerator PlayIntro(AudioClip introClip, AudioClip loopClip)
    {
        yield return PlayClip(introClip, false);
        yield return PlayClip(loopClip, true);
    }

    public IEnumerator PlayClip(AudioClip clip, bool loop)
    {
        if (musicSource.isPlaying)
        {
            musicSource.loop = false;

            while (musicSource.isPlaying)
            {
                yield return null;
            }
        }

        musicSource.loop = loop;
        musicSource.clip = clip;
        musicSource.Play();

        while (musicSource.isPlaying)
        {
            yield return null;
        }
    }
}
