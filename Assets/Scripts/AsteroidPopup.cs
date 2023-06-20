using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AsteroidPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isMouseOverRawImage = false;
    GameObject textObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if the mouse is over a RawImage
        var rawImage = eventData.pointerCurrentRaycast.gameObject.GetComponent<RawImage>();
        isMouseOverRawImage = rawImage != null;
        if (isMouseOverRawImage)
        {
            
            // Handle mouse enter event on the RawImage
            Debug.Log("Mouse entered a RawImage! RawImage name: " + rawImage.name);
            
            textObject = rawImage.transform.GetChild(0).gameObject;

            textObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("No Longer over RawImage");

        textObject.SetActive(false);

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
