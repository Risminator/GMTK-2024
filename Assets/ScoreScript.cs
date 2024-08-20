using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public GameEvent OnFinish;
    private int score = 0;
    public int ScoreToWin = 20;

    public void OnPointAdded(GameObject sender, object data)
    {
        score++;
        if (score == ScoreToWin)
        {
            OnFinish.Raise(gameObject, null);
        }
    }
}
