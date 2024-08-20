using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Звук 
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }
}
