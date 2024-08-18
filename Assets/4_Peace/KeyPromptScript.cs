using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPromptScript : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        hideKeyPrompt();
    }

    public void switchKeyPrompt(GameObject sender, object data)
    {
        if (sender == transform.parent.gameObject)
        {
            if (data is bool)
            {
                sprite.enabled = (bool)data;
            }
            else
            {
                sprite.enabled = false;
            }
        }
    }

    public void showKeyPrompt()
    {
        sprite.enabled = true;
    }

    public void hideKeyPrompt()
    {
        sprite.enabled = false;
    }
}
