using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private ContraLocalAudioManager localAudioManager;

    private void Awake()
    {
        GameObject localAudioObject = GameObject.FindGameObjectWithTag("LocalAudio");
        if (localAudioObject != null)
        {
            localAudioManager = localAudioObject.GetComponent<ContraLocalAudioManager>();
        }
        else
        {
            Debug.LogWarning("LocalAudioManager not found");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        localAudioManager.PlayRandomExplosion();
        
        // ???? 
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }
}
