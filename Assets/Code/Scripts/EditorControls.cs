using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EditorControls : MonoBehaviour
{
    [System.Serializable]
    public class EventControl
    {
        public KeyCode keyCode;
        public UnityEvent unityEvent;
    }

    public EventControl[] eventControls;
    public GameObject uiPanel;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var item in eventControls)
            {
                if (Input.GetKeyDown(item.keyCode))
                {
                    // The key defined in the current EventControl is pressed
                    // You can do something here, for example, invoke the associated UnityEvent
                    item.unityEvent.Invoke();
                }
            }
        }   
    }

    public void TogglePanel() => uiPanel.SetActive(!uiPanel.activeSelf);


}
