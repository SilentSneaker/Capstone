using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoonDropdown : MonoBehaviour
{
    TMP_Dropdown moonSelector;

    public UIController controller;

    public GameObject selectedMoon;

    // Start is called before the first frame update
    void Start()
    {
        moonSelector = gameObject.GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeOptions(string name)
    {

        Debug.Log("Entered ChangeOptions method");
        GameObject parentObj = GameObject.Find(name);

        if (parentObj.tag == "Planet")
        {
            moonSelector.gameObject.SetActive(true);

            moonSelector.ClearOptions();
            // Create a new list of options
            TMP_Dropdown.OptionDataList newOptions = new TMP_Dropdown.OptionDataList();

            int childCount = parentObj.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = parentObj.transform.GetChild(i);
                GameObject childObject = childTransform.gameObject;

                // Check if the child meets your exclusion criteria
                if (!ShouldExcludeChild(childObject))
                {
                    // Add the child's name as an option
                    newOptions.options.Add(new TMP_Dropdown.OptionData(childObject.name));
                }
            }

            moonSelector.options = newOptions.options;

            moonSelector.onValueChanged.AddListener(HandleDropdownSelection);
        }
    }

    void HandleDropdownSelection(int index)
    {
        Debug.Log("Entered Listener method");
        // Get the selected option from the dropdown
        TMP_Dropdown.OptionData selectedOption = moonSelector.options[index];
        string selectedText = selectedOption.text;

        selectedMoon = GameObject.Find(selectedText);

        controller.ViewMoon(selectedMoon);

        // Do something with the selected option text
        Debug.Log("Selected option: " + selectedText);
    }

    private bool ShouldExcludeChild(GameObject child)
    {
        bool ans = false;
        if(child.name.Contains("Name") || child.name.Contains("Ring"))
        {
            ans = true;
        }
        // Example exclusion criteria: Exclude children with "Exclude" in their name
        return ans;
    }

}
