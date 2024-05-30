using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelsUI : MonoBehaviour
{
    private DataReceiver dataReceiver;
    public Toggle toggleSwitch;
    public GameObject uiObject;
    public bool isActive;

    public Text humidityValueText, lightValueText, airQualityValueText, temperatureValueText;

    private void Start()
    {
        UpdateUI();
        dataReceiver = FindObjectOfType<DataReceiver>();
        dataReceiver.OnDataStartReceiving += DataConnected;
        dataReceiver.OnDataConnectionError += ConnectionError;
        dataReceiver.OnDataReceiveWithData += UpdateStatus;
        
        ConnectionLost();
    }

    public void SwitchState()
    {
        isActive = !isActive;
        UpdateUI();
    }

    private void OnDestroy()
    {
        dataReceiver.OnDataStartReceiving -= DataConnected;
        dataReceiver.OnDataConnectionError -= ConnectionError;
        dataReceiver.OnDataReceiveWithData -= UpdateStatus;
    }

    private void UpdateUI()
    {
        toggleSwitch.SetIsOnWithoutNotify(isActive);
        uiObject.SetActive(isActive);
    }

    public void UpdateStatus(DataReceiver.SensorData sensorData = null)
    {
        if (sensorData == null)
        {
            lightValueText.text = "-";
            humidityValueText.text = "-";
            temperatureValueText.text = "-";
            airQualityValueText.text = "-";
        }
        else
        {

            lightValueText.text = sensorData.light.ToString();
            humidityValueText.text = sensorData.humidity.ToString();
            temperatureValueText.text = sensorData.temperature.ToString();
            airQualityValueText.text = sensorData.airQuality == 0 ? "Good" : "Poor";
        }

    }

    public void DataConnected()
    {
        UpdateStatus();
    }
    public void ConnectionError()
    {
        UpdateStatus();
    }
    public void ConnectionLost()
    {
        UpdateStatus();
    }

}
