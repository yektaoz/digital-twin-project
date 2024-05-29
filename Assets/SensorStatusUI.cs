using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorStatusUI : MonoBehaviour
{

    private DataReceiver dataReceiver;
    public Image connectionIcon;

    public Text statusText;

    public Text humidityValueText, lightValueText, airQualityValueText, temperatureValueText;

    public Button connectToServerButton;

    private void Start()
    {
        dataReceiver = FindObjectOfType<DataReceiver>();
        dataReceiver.OnDataStartReceiving += DataConnected;
        dataReceiver.OnDataConnectionError += ConnectionError;
        dataReceiver.OnDataReceiveWithData += UpdateStatus;

        connectToServerButton.onClick.AddListener(dataReceiver.StartConnection);

        ConnectionLost();
    }

    private void OnDestroy()
    {
        dataReceiver.OnDataStartReceiving -= DataConnected;
        dataReceiver.OnDataConnectionError -= ConnectionError;
        dataReceiver.OnDataReceiveWithData -= UpdateStatus;

        connectToServerButton.onClick.RemoveAllListeners();
    }


    public void UpdateStatus(DataReceiver.SensorData sensorData = null)
    {
        if(sensorData == null)
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
        connectionIcon.color = Color.green;
        statusText.text = "Connected";
        connectToServerButton.gameObject.SetActive(false);
        UpdateStatus();
    }

    public void ConnectionError()
    {
        connectionIcon.color = Color.red;
        statusText.text = "Error";
        connectToServerButton.gameObject.SetActive(true);
        UpdateStatus();
    }

    public void ConnectionLost()
    {
        connectionIcon.color = Color.red;
        statusText.text = "Disconnected";
        connectToServerButton.gameObject.SetActive(true);
        UpdateStatus();
    }




}
