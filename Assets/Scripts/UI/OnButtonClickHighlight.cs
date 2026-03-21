using UnityEngine.UI;
using UnityEngine;
using System;

public class OnButtonClickHighlight : MonoBehaviour
{
    private Button highlightButton;

    // Start is called before the first frame update
    void Start()
    {
        highlightButton = GetComponent<Button>();
        
        if(highlightButton != null)
        {
            highlightButton.onClick.AddListener(HighlightButton);
        }
    }

    private void HighlightButton()
    {
        
    }
}
