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

    // Stores the activation script in the object
    private DropdownActivation objectDropdown;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ogCamPos = mainCamera.transform.position;

        objectDropdown = new DropdownActivation();

        uIController = GameObject.Find("ObjectInfoUI").GetComponent<UIController>();
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
                Camera.main.transform.position = ogCamPos;
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

    //public static bool isPointerOverUIObject()
    //{
    //    // Get the position of the mouse cursor
    //    Vector2 mousePosition = Input.mousePosition;

    //    // Create a new pointer event data with the current event system
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    eventDataCurrentPosition.position = mousePosition;

    //    // Create a list to hold the results of the raycast
    //    List<RaycastResult> results = new List<RaycastResult>();

    //    // Perform a raycast to determine which UI element the mouse is currently over
    //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

    //    // Check if any of the raycast results are child objects of the canvas that this script is attached to
    //    foreach (RaycastResult result in results)
    //    {
    //        if (result.gameObject.transform.IsChildOf(transform))
    //        {
    //            return true;
    //        }
    //    }

    //    // If none of the raycast results were child objects of this canvas, return false
    //    return false;
    //}
}
