using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionSpawner : MonoBehaviour
{
    public GameObject Obstruction;
    public float SpawnRate;
    private enum Road
    {
        Top,
        Middle,
        Bottom,
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnObstructions()
    {
        Road road = (Road)Random.Range(0, 3);
        switch (road)
        {
            case Road.Top:
                Obstruction.transform.position = new Vector3(8f, 1.2f, 0f);
                break;
            case Road.Middle:
                Obstruction.transform.position = new Vector3(8f, -0.1f, 0f);
                break;
            case Road.Bottom:
                Obstruction.transform.position = new Vector3(8f, -1.4f, 0f);
                break;
        }

        Instantiate(Obstruction);
    }

    public void StartRace(GameObject sender, object data)
    {
        InvokeRepeating("SpawnObstructions", 0f, SpawnRate);
    }

    public void StopRace(GameObject sender, object data)
    {
        CancelInvoke();
    }

}
