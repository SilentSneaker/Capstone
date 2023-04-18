using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{   
    public Canvas UICanvas;
    public TMP_Dropdown viewDropdown;
    public TMP_Dropdown starTypeDropdown;
    public TMP_InputField factTextBox;
    public TMP_InputField personalTextBox;
    public TMP_InputField picture;
    private TMP_InputField newTextbox;
    public GameObject yellowSun;
    public GameObject redGiant;
    public GameObject whiteDwarf;

    public GameObject activeSun;

    public Vector3 sunCoordinates = new Vector3(0f, 0f, 0f);

    //public Button option;
    private int hiddenOptionIndex;

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.Find("ObjectInfoUI").GetComponent<Canvas>();
        viewDropdown = UICanvas.transform.Find("ViewDropdown").GetComponent<TMP_Dropdown>();
        viewDropdown.onValueChanged.AddListener(OnViewDropdownValueChanged);

        starTypeDropdown = UICanvas.transform.Find("StarTypeDropdown").GetComponent<TMP_Dropdown>();
        starTypeDropdown.onValueChanged.AddListener(OnStarDropdownValueChanged);

        activeSun = Instantiate(yellowSun, sunCoordinates, yellowSun.transform.rotation);
        activeSun.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnViewDropdownValueChanged(int index)
    {

        if (newTextbox != null)
        {
            Debug.Log("In destroy textbox function");
            Destroy(newTextbox.gameObject);
        }
        // Option 0 - Model view (No TextBoxes only the view dropdown in the top left)
        if (index == 0)
        {
            Debug.Log("Selected Model View");

        }
        // Option 1 - Info view (Brings up text box and displays Facts and the History of the object)
        else if (index == 1)
        {
            Debug.Log("Selected Info View");
            newTextbox = Instantiate(factTextBox, Vector3.zero, Quaternion.identity);
            newTextbox.transform.SetParent(GameObject.Find("ObjectInfoUI").transform, false);
            newTextbox.textComponent.alignment = TextAlignmentOptions.Center;

            // Set the text of the textbox to the selected option
            //newTextbox.text = viewDropdown.options[index].text;

            // Fit the textbox within the screen constraints
            RectTransform newTextboxRectTransform = newTextbox.GetComponent<RectTransform>();
            newTextboxRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.pivot = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);

            //Debug.Log(newTextbox);
        }
        // Option 2 - Personal view (Adds a textbox to the canvas and displays the adjusted information that the user put in)
        else if (index == 2)
        {
            Debug.Log("Selected Personal View");
            //Debug.Log("Selected Info View");
            newTextbox = Instantiate(personalTextBox, Vector3.zero, Quaternion.identity);
            newTextbox.transform.SetParent(GameObject.Find("ObjectInfoUI").transform, false);
            newTextbox.textComponent.alignment = TextAlignmentOptions.Center;

            // Set the text of the textbox to the selected option
            //newTextbox.text = viewDropdown.options[index].text;

            // Fit the textbox within the screen constraints
            RectTransform newTextboxRectTransform = newTextbox.GetComponent<RectTransform>();
            newTextboxRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.pivot = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);
        }
        // Option 3 - Picture view (Shows different photos of the object, if there are more than five there should be an option to view more)
        else if (index == 3)
        {
            Debug.Log("Selected Picture View");

            newTextbox = Instantiate(picture, Vector3.zero, Quaternion.identity);
            newTextbox.transform.SetParent(GameObject.Find("ObjectInfoUI").transform, false);
            newTextbox.textComponent.alignment = TextAlignmentOptions.Center;

            // Set the text of the textbox to the selected option
            //newTextbox.text = viewDropdown.options[index].text;

            // Fit the textbox within the screen constraints
            RectTransform newTextboxRectTransform = newTextbox.GetComponent<RectTransform>();
            newTextboxRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.pivot = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);
        }
    }

    public void OnStarDropdownValueChanged(int optionIndex)
    {
        // Do something based on the selected option index
        if(optionIndex == 0)
        {
            Debug.Log("Yellow Sun selected");
            if(activeSun != yellowSun)
            {
                Destroy(activeSun);
                activeSun = Instantiate(yellowSun, sunCoordinates, yellowSun.transform.rotation);
                activeSun.SetActive(true);
            }
            
        }
        else if(optionIndex == 1)
        {
            Debug.Log("Red Giant selected");
            if (activeSun != redGiant)
            {
                Destroy(activeSun);
                activeSun = Instantiate(redGiant, sunCoordinates, yellowSun.transform.rotation);
                activeSun.SetActive(true);
            }
        }
        else if(optionIndex == 2)
        {
            Debug.Log("White Dwarf selected");
            if (activeSun != whiteDwarf)
            {
                Destroy(activeSun);
                activeSun = Instantiate(whiteDwarf, sunCoordinates, yellowSun.transform.rotation);
                activeSun.SetActive(true);
            }
        }
    }
}
