using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SwapToAsteroids : MonoBehaviour
{
    public GameObject asteroidView;
    public GameObject UI;
    public Button switchToAsteroid;
    public Button switchToUI;
    // Start is called before the first frame update
    void Start()
    {
        switchToAsteroid.onClick.AddListener(SwitchToAsteroid);
        switchToUI.onClick.AddListener(SwitchToUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchToAsteroid()
    {
        UI.SetActive(false);
        asteroidView.SetActive(true);
    }

    void SwitchToUI()
    {
        UI.SetActive(true);
        asteroidView.SetActive(false);
    }
}
