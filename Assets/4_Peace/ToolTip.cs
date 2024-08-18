using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    public string message;
    // Start is called before the first frame update
    public void OnInteraction(GameObject sender, object data)
    {
        if (data is string)
        {
            ToolTipManager._instance.SetAndShowToolTip(sender, (string)data);
        }
    }


    public void OnPlayerExit(GameObject sender, object data)
    {
        if (data is bool && (bool)data == false)
        {
            ToolTipManager._instance.HideTooltip();
        }
    }
}
