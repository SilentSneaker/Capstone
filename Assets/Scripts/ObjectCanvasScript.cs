using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCanvasScript : MonoBehaviour
{
    public Text objectName;

    Transform unit;
    public Transform Canvas;
    public Camera mainCamera;
    public float baseDistance = 10f;
    public float scaleFactor = 0.1f;
    public Transform O_GParent;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        O_GParent = transform.parent;
        mainCamera = Camera.main;
        unit = transform.parent;
        transform.SetParent(Canvas);
        objectName.transform.localScale = new Vector3(1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        Text ren = GetComponent<Text>();
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);

        transform.position = unit.position - offset;
        transform.rotation = Quaternion.Euler(mainCamera.transform.rotation.eulerAngles);
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Adjust the scale of the textbox based on the distance
        float scale = 1f + (distance * scaleFactor);
        transform.localScale = new Vector3(scale, scale, scale);
        if(distance < baseDistance)
        {
            ren.enabled = false;
        }
        else
        {
             ren.enabled = true;
        }
    }
    
     private void OnMouseDown() {
        Camera.main.transform.LookAt(O_GParent.transform.position);
    }
}
