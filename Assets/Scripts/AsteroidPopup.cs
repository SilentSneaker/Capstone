using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class AsteroidPopup : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    bool isMouseOverRawImage = false;
    public GameObject clickedUI;
    GameObject textObject;
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the mouse is over a RawImage
        clickedUI = eventData.pointerCurrentRaycast.gameObject;
        if (clickedUI.name != "2011-21 Asteroid Panel" && clickedUI.name != "Future Asteroid Panel" && clickedUI.name != "Past Asteroid Panel")
        {
            // Handle mouse enter event on the RawImage
            Debug.Log("Mouse entered a RawImage! RawImage name: " + clickedUI.name);

            textObject = clickedUI.transform.GetChild(0).gameObject;

            textObject.SetActive(true);
        }
        else
        {
            textObject.SetActive(false);
        }

        clickedUI = null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("No Longer over RawImage");

        //textObject.SetActive(false);

        isMouseOverRawImage = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

