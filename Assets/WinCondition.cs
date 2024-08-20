using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    private Text textLabel;
    private Canvas canvas;
    public GameEvent OnFinish;
    private PongLocalAudioManager audioComponent;

    private void Awake()
    {
        textLabel = GetComponent<Text>();
        canvas = GameObject.FindGameObjectWithTag("Score").GetComponent<Canvas>();

        GameObject audioManager = GameObject.FindGameObjectWithTag("LocalAudio");
        if (audioManager != null)
        {
            audioComponent = audioManager.GetComponent<PongLocalAudioManager>();
        }
        else
        {
            Debug.LogWarning("LocalAudioManager not found");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Convert.ToInt32(textLabel.text) >= 3)
        {
            canvas.enabled = false;
            audioComponent.PlaySFX(audioComponent.Explosion);
            OnFinish.Raise(gameObject, null);
        }
    }
}
