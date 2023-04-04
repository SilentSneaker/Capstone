using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBehavior : MonoBehaviour
{
    public GameObject Sun;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Camera.main.transform.position = new Vector3(Sun.transform.position.x,Sun.transform.position.y, Sun.transform.position.z - 3);
            
        }
        if (Input.GetMouseButtonUp(0))
        {

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
