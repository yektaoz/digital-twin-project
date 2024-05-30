using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelsUI : MonoBehaviour
{

    public Toggle toggleSwitch;
    public GameObject uiObject;
    public bool isActive;

    private void Start()
    {
        UpdateUI();
    }

    public void SwitchState()
    {
        isActive = !isActive;
        UpdateUI();
    }

    private void UpdateUI()
    {
        toggleSwitch.isOn = isActive;
        uiObject.SetActive(isActive);
    }
}
