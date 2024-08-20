using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject Player;
    public float RespawnTime = 3f;

    public void OnPlayerDeath(GameObject sender, object data)
    {
        if (data is string && (string)data == "Player")
        {
            StartCoroutine(RespawnAfterDelay());
        }
    }

    public IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(RespawnTime);
        Instantiate(Player, transform.position, transform.rotation);
    }
}
