using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownActivation : MonoBehaviour
{

    public static GameObject uiCanvas;
    public TMP_Dropdown viewDropdown;
    public TMP_Dropdown starTypeDropdown;
    public TMP_Dropdown moonDropdown;
    public TMP_Dropdown planetNav;
    private void Start()
    {
        uiCanvas = GameObject.Find("ObjectInfoUI");

        viewDropdown = uiCanvas.transform.Find("ViewDropdown").GetComponent<TMP_Dropdown>();
        viewDropdown.interactable = false;
        viewDropdown.GetComponent<CanvasGroup>().alpha = 0;

        moonDropdown = uiCanvas.transform.Find("Moon Selector").GetComponent<TMP_Dropdown>();
        moonDropdown.interactable = false;
        moonDropdown.GetComponent<CanvasGroup>().alpha = 0;

        starTypeDropdown = uiCanvas.transform.Find("StarTypeDropdown").GetComponent<TMP_Dropdown>();
        starTypeDropdown.interactable = true;
        starTypeDropdown.GetComponent<CanvasGroup>().alpha = 1;

        planetNav = uiCanvas.transform.Find("Planet Nav").GetComponent<TMP_Dropdown>();
        planetNav.interactable = true;
        planetNav.GetComponent<CanvasGroup>().alpha = 1;
    }
    
    public void ShowDropdown()
    {
        if (viewDropdown == null)
        {
            Debug.Log("viewDropdown is null");
        }
        else
        {
            //Debug.Log("viewDropdown is not null");
        }

        viewDropdown.interactable = true;
        viewDropdown.GetComponent<CanvasGroup>().alpha = 1;

        starTypeDropdown.interactable = false;
        starTypeDropdown.GetComponent<CanvasGroup>().alpha = 0;

        moonDropdown.interactable = true;
        moonDropdown.GetComponent<CanvasGroup>().alpha = 1;

        planetNav.interactable = false;
        planetNav.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void RemoveDropdown()
    {
        viewDropdown.interactable = false;
        viewDropdown.GetComponent<CanvasGroup>().alpha = 0;

        moonDropdown.interactable = false;
        moonDropdown.GetComponent<CanvasGroup>().alpha = 0;

        starTypeDropdown.interactable = true;
        starTypeDropdown.GetComponent<CanvasGroup>().alpha = 1;

        planetNav.interactable = true;
        planetNav.GetComponent<CanvasGroup>().alpha = 1;
    }
}
