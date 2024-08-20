using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerScript : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.Follow == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) { cam.Follow = player.transform; }
        }
    }
}
