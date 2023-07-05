using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlanetNavigation : MonoBehaviour
{
    GameObject planetNav;
    TMP_Dropdown navDropdown;
    // Start is called before the first frame update
    void Start()
    {
        planetNav = gameObject;
        navDropdown = planetNav.GetComponent<TMP_Dropdown>();
        navDropdown.onValueChanged.AddListener(MoveCameratoPlanet);
    }

    private void MoveCameratoPlanet(int index)
    {
        GameObject selectedPlanet = GameObject.Find(navDropdown.options[index].text);

        Camera.main.transform.position = new Vector3(selectedPlanet.transform.position.x, selectedPlanet.transform.position.y, selectedPlanet.transform.position.z + 250);

        Vector3 lookDirection = selectedPlanet.transform.position - Camera.main.transform.position;

        // Rotate the camera to face the target object
        Camera.main.transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    // Update is called once per frame
    void Update()
    {

    }


}