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

    // Stores the activation script in the object
    private DropdownActivation objectDropdown;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ogCamPos = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit objectHit;
            if (Physics.Raycast(ray, out objectHit) && !isPointerOverUIObject())
            {
                //selectedTag = objectHit.collider.tag;
                clickedObject = objectHit.collider.gameObject;

                if (zoomedIn == false)
                {
                    objectDropdown = clickedObject.GetComponent<DropdownActivation>();
                    objectDropdown.ShowDropdown();

                    Camera.main.transform.position = new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y, clickedObject.transform.position.z - 3);
                    zoomedIn = true;
                }
            }
            else if (!isPointerOverUIObject() && zoomedIn == true)
            {
                Camera.main.transform.position = ogCamPos;
                zoomedIn = false;
                if(objectDropdown != null)
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

    public static bool isPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
