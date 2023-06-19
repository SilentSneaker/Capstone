using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class SwapPanels : MonoBehaviour
{
    public Button futureAsteroids;
    public Button pastAsteroids;
    public Button teensAsteroids;
    public GameObject pastPanel;
    public GameObject presentPanel;
    public GameObject futurePanel;

    private void Start()
    {
        futureAsteroids.onClick.AddListener(ChangeToFuturePanel);
        pastAsteroids.onClick.AddListener(ChangeToPastPanel);
    }

    private void ChangeToPastPanel()
    {
        presentPanel.SetActive(false);
        pastPanel.SetActive(true);
        teensAsteroids = pastPanel.transform.Find("2010-2023 Asteroids").GetComponent<Button>();
    }

    private void ChangeToFuturePanel()
    {
        presentPanel.SetActive(false);
        futurePanel.SetActive(true);
        teensAsteroids = futurePanel.transform.Find("2010-2023 Asteroids").GetComponent<Button>();
        teensAsteroids.onClick.AddListener(ReturnToCurrentPanel);
    }

    private void ReturnToCurrentPanel()
    {
        teensAsteroids.onClick.RemoveListener(ReturnToCurrentPanel);
        //pastPanel.SetActive(false);
        futurePanel.SetActive(false);
        presentPanel.SetActive(true);
    }
}
