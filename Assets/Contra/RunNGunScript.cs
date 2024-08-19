using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunNGunScript : MonoBehaviour
{
    private Animator animator;
    private bool isLying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isLying = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isLying = false;
        }

        animator.SetBool("isLying", isLying);
    }
}

