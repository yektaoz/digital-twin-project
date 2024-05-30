using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractiveElementsUI : MonoBehaviour
{
    private DataReceiver dataReceiver;
    public InteractiveElement[] interactiveElements;

    [System.Serializable]
    public class InteractiveElement
    {
        public string name;
        public bool isActivated;
        public UnityEvent OnUnityEvents;
        public UnityEvent OffUnityEvents;
        public Toggle toggle;
    }

    private void Start()
    {
        SwitchAll();
    }
    
    public void SwitchAll()
    {
        foreach (var item in interactiveElements)
        {
            SwitchToggle(item.name);
        }
    }


    private InteractiveElement GetElement(string name)
    {
        return Array.Find(interactiveElements, var => var.name == name);
    }

    public void SwitchToggle(string name)
    {
        InteractiveElement _element = GetElement(name);
        if (_element.isActivated)
        {
            _element.isActivated = false;
            _element.toggle.isOn = _element.isActivated;
            _element.OffUnityEvents?.Invoke();
        }
        else
        {
            _element.isActivated = true;
            _element.toggle.SetIsOnWithoutNotify(_element.isActivated);
            _element.OnUnityEvents?.Invoke();
        }
    }

}
