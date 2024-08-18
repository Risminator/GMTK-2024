using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipManager : MonoBehaviour
{
    public static ToolTipManager _instance;

    public TextMeshProUGUI textComponent;

    public Vector3 Offset;

    private RectTransform rectTransform;

    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            rectTransform = GetComponent<RectTransform>();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetAndShowToolTip(GameObject sender, string message)
    {
        float senderY = sender.GetComponent<SpriteRenderer>().bounds.size.y;
        float tooltipHeight = rectTransform.rect.height * canvas.GetComponent<RectTransform>().localScale.y;
        transform.position = sender.transform.position + new Vector3(0f, (senderY + tooltipHeight)/ 2f, 0f);
        Debug.Log(transform.position);
        gameObject.SetActive(true);
        textComponent.text = message;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
