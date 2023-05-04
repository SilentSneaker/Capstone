using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackButton : MonoBehaviour
{
    public Button backButton;
    public GameObject accountPrefab;
    // Start is called before the first frame update
    void Start()
    {
        backButton = gameObject.GetComponent<Button>();
        backButton.onClick.AddListener(BackButtonListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackButtonListener()
    {
        Debug.Log("Back Button Clicked");
        accountPrefab = gameObject.transform.parent.gameObject;
        GameObject UICanvas = accountPrefab.transform.parent.gameObject;
        Debug.Log(accountPrefab);
        Destroy(accountPrefab);
        Camera.main.GetComponent<ClickBehavior>().enabled = true;
        Camera.main.GetComponent<CameraMovement>().enabled = true;
        UICanvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;
    }
}
