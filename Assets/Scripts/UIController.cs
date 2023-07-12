using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class UIController : MonoBehaviour
{
    #region Variables
    public Canvas UICanvas;
    public TMP_Dropdown viewDropdown;
    public TMP_Dropdown starTypeDropdown;

    public TMP_InputField factTextBox;
    public TMP_InputField personalTextbox;

    public GameObject imagePrefab;
    public GameObject imageGallery;

    public static TMP_InputField displayFactTextbox;
    public static TMP_InputField displayPersonalTextbox;

    public GameObject yellowSun;
    public GameObject redGiant;
    public GameObject whiteDwarf;

    public GameObject activeSun;

    public StarInfo starInfo;
    public MoonInfo moonInfo;
    public PlanetInfo planetInfo;
    public DwarfPlanetInfo dwarfPlanetInfo;

    public int starSelected = 0;

    public Vector3 sunCoordinates = new Vector3(0f, 0f, 0f);

    public Button profile;
    public Button roverPhotos;
    public Button topDownView;
    public Button angledView;

    public GameObject accountPrefab;
    public GameObject objectInfoUI;

    public ImageLoader imageLoader;

    bool viewingMars;
    TMP_Dropdown roverDropdown;
    TMP_Dropdown cameraSelector;

    AdjustUserInfo adjustUserInfo;

    public MoonDropdown moonDropdown;

    SOLoader selectedObject;

    RoverPicManager picManager;

    NASAImageAPI imageAPI;

    string selectedObj;

    bool viewingMoon = false;

    GameObject selectedMoon;

    GameObject sun;

    float distanceFromMoon = 10f;

    public Button asteroidView;

    //SOLoader selectedObject;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.Find("ObjectInfoUI").GetComponent<Canvas>();
        viewDropdown = UICanvas.transform.Find("ViewDropdown").GetComponent<TMP_Dropdown>();
        viewDropdown.onValueChanged.AddListener(OnViewDropdownValueChanged);

        roverPhotos = UICanvas.transform.Find("Rover Photos").GetComponent<Button>();
        roverPhotos.onClick.AddListener(ShowRoverPhotos);

        imageGallery = UICanvas.transform.Find("ImageGallery").gameObject;
        roverDropdown = imageGallery.transform.Find("Image Selector").GetComponent<TMP_Dropdown>();
        roverDropdown.onValueChanged.AddListener(ChangeRover);

        //cameraSelector = imageGallery.transform.Find("Camera Selector").GetComponent<TMP_Dropdown>();
        //cameraSelector.onValueChanged.AddListener(ChangeCamera);

        picManager = gameObject.GetComponent<RoverPicManager>();

        sun = GameObject.Find("Sun");

        adjustUserInfo = UICanvas.GetComponent<AdjustUserInfo>();

        imageAPI = gameObject.GetComponent<NASAImageAPI>();


        //Instatiate the info of objects
        starInfo = UICanvas.GetComponent<StarInfo>();
        moonInfo = UICanvas.GetComponent<MoonInfo>();
        planetInfo = UICanvas.GetComponent<PlanetInfo>();
        dwarfPlanetInfo = UICanvas.GetComponent<DwarfPlanetInfo>();

        imageLoader = GameObject.Find("ObjectInfoUI").GetComponent<ImageLoader>();

        starTypeDropdown = UICanvas.transform.Find("StarTypeDropdown").GetComponent<TMP_Dropdown>();
        starTypeDropdown.onValueChanged.AddListener(OnStarDropdownValueChanged);

        //activeSun = Instantiate(yellowSun, sunCoordinates, yellowSun.transform.rotation);
        //activeSun.SetActive(true);

        profile = UICanvas.transform.Find("Profile").GetComponent<Button>();
        profile.onClick.AddListener(OnProfileClick);

        topDownView = UICanvas.transform.Find("2D model").GetComponent<Button>();
        topDownView.onClick.AddListener(TopDownView);

        angledView = UICanvas.transform.Find("3D Model").GetComponent<Button>();
        angledView.onClick.AddListener(AngledView);

    }

    private void ChangeCamera(int arg0)
    {
        if(arg0 == 0)
        {
            picManager.SetCamera("navcam");
        }
        else if (arg0 == 1)
        {
            picManager.SetCamera("fhaz");
        }
        else if (arg0 == 2)
        {
            picManager.SetCamera("rhaz");
        }
    }

    private void ChangeRover(int option)
    {
        if (option == 0)
        {
            picManager.SetRover("curiosity");
            picManager.SetMaxSolDate(3970);
        }
        else if (option == 1)
        {
            picManager.SetRover("spirit");
            picManager.SetMaxSolDate(2209);
        }
        else if (option == 2)
        {
            picManager.SetRover("opportunity");
            picManager.SetMaxSolDate(5353);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(viewingMoon == true)
        {
            Vector3 midpoint = (sun.transform.position + selectedMoon.transform.position) / 2f;

            // Calculate the direction vector from the Sun to the Moon
            Vector3 direction = (selectedMoon.transform.position - sun.transform.position).normalized;

            // Set the camera position to be closer to the Moon
            Camera.main.transform.position = selectedMoon.transform.position - direction * distanceFromMoon;
            Camera.main.transform.LookAt(selectedMoon.transform);//new Vector3(selectedMoon.transform.position.x, selectedMoon.transform.position.y, selectedMoon.transform.position.z - 100);
        }
    }

    private void OnViewDropdownValueChanged(int index)
    {

        // Option 0 - Model view (No TextBoxes only the view dropdown in the top left)
        if (index == 0)
        {
            displayFactTextbox.gameObject.SetActive(false);
            displayPersonalTextbox.gameObject.SetActive(false);
            imageGallery.SetActive(false);

            UICanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
            //Debug.Log("Selected Model View");

        }
        // Option 1 - Info view (Brings up text box and displays Facts and the History of the object)
        else if (index == 1)
        {
            displayFactTextbox.gameObject.SetActive(true);
            displayPersonalTextbox.gameObject.SetActive(false);
            imageGallery.SetActive(false);

            UICanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;

            // Fit the textbox within the screen constraints
            RectTransform newTextboxRectTransform = displayFactTextbox.GetComponent<RectTransform>();
            newTextboxRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.pivot = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);
            //Debug.Log(newTextbox);
        }
        // Option 2 - Personal view (Adds a textbox to the canvas and displays the adjusted information that the user put in)
        else if (index == 2)
        {
            UnloadImages();

            displayFactTextbox.gameObject.SetActive(false);
            displayPersonalTextbox.gameObject.SetActive(true);
            imageGallery.SetActive(false);

            UICanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;

            displayPersonalTextbox.text = "You would weigh " + adjustUserInfo.CalculateWeight(selectedObject.gravity) + " pounds on " + selectedObject.name;

            // Fit the textbox within the screen constraints
            RectTransform newTextboxRectTransform = displayPersonalTextbox.GetComponent<RectTransform>();
            newTextboxRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.pivot = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);
        }
        // Option 3 - Picture view (Shows different photos of the object, if there are more than five there should be an option to view more)
        else if (index == 3)
        {
            displayFactTextbox.gameObject.SetActive(false);
            displayPersonalTextbox.gameObject.SetActive(false);
            imageGallery.SetActive(true);

            UICanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0;

            // Fit the textbox within the screen constraints
            RectTransform newTextboxRectTransform = imageGallery.GetComponent<RectTransform>();
            newTextboxRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.pivot = new Vector2(0.5f, 0.5f);
            newTextboxRectTransform.sizeDelta = new Vector2(Screen.width * 0.8f, Screen.height * 0.8f);
            if(viewingMars == true)
            {
                roverPhotos.gameObject.SetActive(true);
            }

            //Start Image API Loading
            imageLoader.LoadLibraryImages(imageGallery, selectedObject.name);
        }
        
    }

    public void OnStarDropdownValueChanged(int optionIndex)
    {
        starSelected = optionIndex;
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

    public void OnProfileClick()
    {
        Debug.Log("Profile clicked");
        GameObject newAccount = Instantiate(accountPrefab, objectInfoUI.transform);
        CanvasScaler scaler = UICanvas.GetComponent<CanvasScaler>();
        scaler.matchWidthOrHeight = 1f;
        Camera.main.GetComponent<ClickBehavior>().enabled = false;
        Camera.main.GetComponent<CameraMovement>().enabled = false;
    }

    public void TopDownView()
    {
        Debug.Log("Top Down View Clicked");
        Camera.main.transform.position = new Vector3(0f, 750000f, 0f);
        Camera.main.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    public void AngledView()
    {
        Debug.Log("Angled View Clicked");
        Camera.main.transform.position = new Vector3(100f, 0f, -1100f);
        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void ChangeText(GameObject clickedObject)
    {
        Debug.Log(clickedObject);
        if(displayPersonalTextbox == null)
        {
            displayPersonalTextbox = Instantiate(personalTextbox, Vector3.zero, Quaternion.identity);
            displayPersonalTextbox.transform.SetParent(GameObject.Find("ObjectInfoUI").transform, false);
            displayPersonalTextbox.textComponent.alignment = TextAlignmentOptions.Center;
            displayPersonalTextbox.gameObject.SetActive(false);
        }
        if(displayFactTextbox == null)
        {
            displayFactTextbox = Instantiate(factTextBox, Vector3.zero, Quaternion.identity);
            displayFactTextbox.transform.SetParent(GameObject.Find("ObjectInfoUI").transform, false);
            displayFactTextbox.textComponent.alignment = TextAlignmentOptions.Center;
            displayFactTextbox.gameObject.SetActive(false);
        }
        if (clickedObject.tag == "Sun")
        {
            selectedObj = clickedObject.name;

            moonDropdown.ChangeOptions(selectedObj);

            //Debug.Log("Made it into the UIController 2");
            #region If statements for checking the name of the suns
            if (clickedObject.name.Trim() == "Yellow Sun" || clickedObject.name == "Yellow Sun(Clone)" || clickedObject.name == "Sun Model")
            {
                displayFactTextbox.text = starInfo.GetInfo(0);
            }
            else if (clickedObject.name.Trim() == "Red Giant" || clickedObject.name == "Red Giant(Clone)" || clickedObject.name == "Red Giant Model")
            {
                displayFactTextbox.text = starInfo.GetInfo(1);
            }
            else if (clickedObject.name.Trim() == "White Dwarf" || clickedObject.name == "White Dwarf(Clone)" || clickedObject.name == "White Dwarf Model")
            {
                displayFactTextbox.text = starInfo.GetInfo(2);
            }
            #endregion
        }
        else if (clickedObject.tag == "Moon")
        {
            selectedObj = clickedObject.name;

            moonDropdown.ChangeOptions(selectedObj);

            #region If statements for checking the name of the moons
            if (clickedObject.name.Trim() == "Moon" || clickedObject.name == "Moon model" || clickedObject.name == "Moon Model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(0);
            }
            else if (clickedObject.name.Trim() == "Phobos" || clickedObject.name == "Phobos(Clone)" || clickedObject.name == "Phobos model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(1);
            }
            else if (clickedObject.name.Trim() == "Deimos" || clickedObject.name == "Deimos(Clone)" || clickedObject.name == "Deimos model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(2);
            }
            else if (clickedObject.name.Trim() == "Io" || clickedObject.name == "Io(Clone)" || clickedObject.name == "Io model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(3);
            }
            else if (clickedObject.name.Trim() == "Europa" || clickedObject.name == "Europa(Clone)" || clickedObject.name == "Europa model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(4);
            }
            else if (clickedObject.name.Trim() == "Ganymede" || clickedObject.name == "Ganymede(Clone)" || clickedObject.name == "Ganymede model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(5);
            }
            else if (clickedObject.name.Trim() == "Callisto" || clickedObject.name == "Callisto(Clone)" || clickedObject.name == "Callisto model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(6);
            }
            else if (clickedObject.name.Trim() == "Mimas" || clickedObject.name == "Mimas(Clone)" || clickedObject.name == "Mimas model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(7);
            }
            else if (clickedObject.name.Trim() == "Enceladus" || clickedObject.name == "Enceladus(Clone)" || clickedObject.name == "Enceladus model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(8);
            }
            else if (clickedObject.name.Trim() == "Tethys" || clickedObject.name == "Tethys(Clone)" || clickedObject.name == "Tethys model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(9);
            }
            else if (clickedObject.name.Trim() == "Dione" || clickedObject.name == "Dione(Clone)" || clickedObject.name == "Dione model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(10);
            }
            else if (clickedObject.name.Trim() == "Rhea" || clickedObject.name == "Rhea(Clone)" || clickedObject.name == "Rhea model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(11);
            }
            else if (clickedObject.name.Trim() == "Titan" || clickedObject.name == "Titan(Clone)" || clickedObject.name == "Titan model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(12);
            }
            else if (clickedObject.name.Trim() == "Iapetus" || clickedObject.name == "Iapetus(Clone)" || clickedObject.name == "Iapetus model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(13);
            }
            else if (clickedObject.name.Trim() == "Miranda" || clickedObject.name == "Miranda(Clone)" || clickedObject.name == "Miranda model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(14);
            }
            else if (clickedObject.name.Trim() == "Ariel" || clickedObject.name == "Ariel(Clone)" || clickedObject.name == "Ariel model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(15);
            }
            else if (clickedObject.name.Trim() == "Umbriel" || clickedObject.name == "Umbriel(Clone)" || clickedObject.name == "Umbriel model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(16);
            }
            else if (clickedObject.name.Trim() == "Titania" || clickedObject.name == "Titania(Clone)" || clickedObject.name == "Titania model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(17);
            }
            else if (clickedObject.name.Trim() == "Oberon" || clickedObject.name == "Oberon(Clone)" || clickedObject.name == "Oberon model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(18);
            }
            else if (clickedObject.name.Trim() == "Triton" || clickedObject.name == "Triton(Clone)" || clickedObject.name == "Triton model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(19);
            }
            else if (clickedObject.name.Trim() == "Charon" || clickedObject.name == "Charon(Clone)" || clickedObject.name == "Charon model")
            {
                displayFactTextbox.text = moonInfo.GetInfo(20);
            }
            else
            {
                displayFactTextbox.text = "Information on this celestial body is scarce, if you wish to learn more use a search engine.";
            }
            #endregion
        }
        else if (clickedObject.tag == "Planet")
        {
            #region If statements for checking the name of the planets
            selectedObj = clickedObject.name;

            moonDropdown.ChangeOptions(selectedObj);

            if (clickedObject.name.Trim() == "Mercury" || clickedObject.name == "Mercury(Clone)" || clickedObject.name == "Mercury Model")
            {
                displayFactTextbox.text = planetInfo.GetInfo(0);
            }
            else if (clickedObject.name.Trim() == "Venus" || clickedObject.name == "Venus(Clone)" || clickedObject.name == "Venus Model")
            {
                displayFactTextbox.text = planetInfo.GetInfo(1);
            }
            else if (clickedObject.name.Trim() == "Earth" || clickedObject.name == "Earth(Clone)" || clickedObject.name == "Earth Model")
            {
                displayFactTextbox.text = planetInfo.GetInfo(2);
                asteroidView.gameObject.SetActive(true);
            }
            else if (clickedObject.name.Trim() == "Mars" || clickedObject.name == "Mars(Clone)" || clickedObject.name == "Mars Model")
            {
                displayFactTextbox.text = planetInfo.GetInfo(3);
                viewingMars = true;
            }
            else if (clickedObject.name.Trim() == "Jupiter" || clickedObject.name == "Jupiter(Clone)" || clickedObject.name == "Jupiter Model")
            {
                displayFactTextbox.text = planetInfo.GetInfo(4);
            }
            else if (clickedObject.name.Trim() == "Saturn" || clickedObject.name == "Saturn(Clone)" || clickedObject.name == "Saturn Model")
            {
                displayFactTextbox.text = planetInfo.GetInfo(5);
            }
            else if (clickedObject.name.Trim() == "Uranus" || clickedObject.name == "Uranus(Clone)" || clickedObject.name == "Uranus Model")
            {
                displayFactTextbox.text = planetInfo.GetInfo(6);
            }
            else if (clickedObject.name.Trim() == "Neptune" || clickedObject.name == "Neptune(Clone)" || clickedObject.name == "Neptune Model")
            {
                displayFactTextbox.text = planetInfo.GetInfo(7);
            }
            #endregion
        }
        else if(clickedObject.tag == "Dwarf Planet")
        {
            //Debug.Log("In Dwarf planet if statement");
            #region If statements for checking the name of the planets
            selectedObj = clickedObject.name;

            moonDropdown.ChangeOptions(selectedObj);

            if (clickedObject.name.Trim() == "Pluto" || clickedObject.name == "Pluto(Clone)" || clickedObject.name == "Pluto Model")
            {
                displayFactTextbox.text = dwarfPlanetInfo.GetInfo(0);
            }
            else if (clickedObject.name.Trim() == "Ceres" || clickedObject.name == "Ceres(Clone)" || clickedObject.name == "Ceres Model")
            {
                displayFactTextbox.text = dwarfPlanetInfo.GetInfo(1);
            }
            else if (clickedObject.name.Trim() == "Makemake" || clickedObject.name == "Makemake(Clone)" || clickedObject.name == "Makemake Model")
            {
                displayFactTextbox.text = dwarfPlanetInfo.GetInfo(2);
            }
            else if (clickedObject.name.Trim() == "Haumea" || clickedObject.name == "Haumea(Clone)" || clickedObject.name == "Haumea Model")
            {
                displayFactTextbox.text = dwarfPlanetInfo.GetInfo(3);
            }
            else if (clickedObject.name.Trim() == "Eris" || clickedObject.name == "Eris(Clone)" || clickedObject.name == "Eris Model")
            {
                displayFactTextbox.text = dwarfPlanetInfo.GetInfo(4);
            }
            #endregion
        }
        selectedObject = clickedObject.GetComponent<SOLoader>();
    }

    public void MakeTextboxesInvisible()
    {
        displayFactTextbox.gameObject.SetActive(false);
        displayPersonalTextbox.gameObject.SetActive(false);
        imageGallery.gameObject.SetActive(false);
        if (roverPhotos.IsActive())
        {
            roverDropdown.gameObject.SetActive(false);
            roverPhotos.gameObject.SetActive(false);            
            viewingMars = false;
        }
        Debug.Log("Destroyed TextBoxes");
    }

    public void ShowRoverPhotos()
    {
        imageLoader.LoadImage(imageGallery);
        roverDropdown.gameObject.SetActive(true);
    }

    public void UnloadImages()
    {
        imageLoader.ClearImages(imageGallery);
    }

    public void ViewMoon(GameObject moon)
    {
        selectedMoon = moon;
        viewingMoon = true;
        ChangeText(moon);
    }

    public void ResetCamera()
    {
        viewingMoon = false;
    }
}
