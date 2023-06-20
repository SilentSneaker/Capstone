using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBehavior : MonoBehaviour
{
    public GameObject clickedObject;
    public string selectedTag;
    private Camera mainCamera;
    public Vector3 ogCamPos;
    private bool zoomedIn = false;

    public UIController uIController;


    NASAImageAPI imageAPI;


    // Stores the activation script in the object
    private DropdownActivation objectDropdown;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ogCamPos = mainCamera.transform.position;

        //objectDropdown = new DropdownActivation();

        uIController = GameObject.Find("ObjectInfoUI").GetComponent<UIController>();

        imageAPI = GameObject.Find("ObjectInfoUI").GetComponent<NASAImageAPI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit objectHit;
            Physics.Raycast(ray, out objectHit);
            if (objectHit.collider != null)
            {
                if (objectHit.collider.tag != "UI" && zoomedIn == false)
                {
                    //selectedTag = objectHit.collider.tag;
                    clickedObject = objectHit.collider.gameObject;

                    Debug.Log(clickedObject);

                    string substringToRemove = "(Clone)";
                    string spaceToRemove = " ";
                    string stringToModify = clickedObject.name;
                    if (stringToModify.Contains(substringToRemove))
                    {
                        // Remove the substring from the original string
                       stringToModify.Replace(substringToRemove, "");                       

                        imageAPI.SetSearchQuery(stringToModify);

                        // Output the modified string
                        //Debug.Log(stringToModify);
                    }
                    if (stringToModify.Contains(spaceToRemove))
                    {
                        stringToModify.Replace(spaceToRemove, "_");

                    }
                    else
                    {
                        imageAPI.SetSearchQuery(clickedObject.name);
                    }

                    //Starts Images API to get photos
                    StartCoroutine(imageAPI.FetchImageData());


                    uIController.ChangeText(clickedObject);
                    //Debug.Log(clickedObject);
                    if (zoomedIn == false)
                    {
                        objectDropdown = clickedObject.GetComponent<DropdownActivation>();
                        objectDropdown.ShowDropdown();

                        Camera.main.transform.position = new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y, clickedObject.transform.position.z - 3);
                        
                        zoomedIn = true;
                    }
                }
                
            }
            else if (!EventSystem.current.IsPointerOverGameObject() && zoomedIn == true)
            {
                uIController.UnloadImages();

                Camera.main.transform.position = ogCamPos;

                imageAPI.DeleteImages();

                Debug.Log("Clicked on no object");
                uIController.MakeTextboxesInvisible();
                zoomedIn = false;
                if (objectDropdown != null)
                {

                    objectDropdown.RemoveDropdown();
                }
            }
        }
        /*GameObject getClickedObject (out RaycastHit hit)
        {
            GameObject target = null;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast (ray.origin, ray.direction * 10, out hit))
            {
                if (!isPointerOverUIObject()) { }
            }
        }*/
    }

   
}
