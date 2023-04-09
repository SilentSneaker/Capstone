using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownActivation : MonoBehaviour
{

    public static GameObject uiCanvas;
    public TMP_Dropdown viewDropdown;
    private void Start()
    {
        uiCanvas = GameObject.Find("ObjectInfoUI");
        viewDropdown = uiCanvas.GetComponentInChildren<TMP_Dropdown>();
    }
    
    public void ShowDropdown()
    {
        if (viewDropdown == null)
        {
            Debug.Log("viewDropdown is null");
        }
        else
        {
            Debug.Log("viewDropdown is not null");
        }

        viewDropdown.interactable = true;
        viewDropdown.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void RemoveDropdown()
    {
        viewDropdown.interactable = false;
        viewDropdown.GetComponent<CanvasGroup>().alpha = 0;
    }
}
