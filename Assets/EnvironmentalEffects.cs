using UnityEngine;

public class EnvironmentalEffects : MonoBehaviour
{

    public GameObject poorAir, goodAir;
    public GameObject lowTemperature, highTemperature;
    public GameObject highHumidity;

    public DataReceiver dataReceiver;

    public float hotTemperature = 25;
    public float coldTemperature = 15;


    private void Start()
    {
        dataReceiver = FindObjectOfType<DataReceiver>();

        dataReceiver.OnDataReceiveWithData += UpdateEffects;
        UpdateEffects();
    }

    private void OnDestroy()
    {
        dataReceiver.OnDataReceiveWithData -= UpdateEffects;   
    }

    public void UpdateEffects(DataReceiver.SensorData sensorData = null)
    {
        if(sensorData == null)
        {
            goodAir.SetActive(false);
            poorAir.SetActive(false);

            highHumidity.SetActive(false);

            lowTemperature.SetActive(false);
            highTemperature.SetActive(false);
            return;
        }

        goodAir.SetActive(sensorData.airQuality == 0);
        poorAir.SetActive(sensorData.airQuality == 1);

        highHumidity.SetActive(sensorData.humidity > 50);
        
        lowTemperature.SetActive(sensorData.temperature < 15);
        highTemperature.SetActive(sensorData.temperature > 25);
    }

}
