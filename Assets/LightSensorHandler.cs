using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSensorHandler : MonoBehaviour
{


    public Light lightSource;
    public bool syncWithSensor;
    public bool smoothSync;

    public float lightSensorData;
    public float dataMultiplier;


    public float minusMax = 100000;
    public float normalizedMax => minusMax * dataMultiplier;

    public float InvertedValue => normalizedMax - LightValue;

    public float LightValue => dataMultiplier * lightSensorData;
    public void UpdateLightData(float lastData) => lightSensorData = lastData;

    public DataReceiver lightSensorReceiver;


    private void Start()
    {
        lightSensorReceiver.OnLightDataReceiveWithValue += UpdateLightData;
    }

    private void OnDestroy()
    {
        lightSensorReceiver.OnLightDataReceiveWithValue -= UpdateLightData;
    }

    private void Update()
    {
        if (syncWithSensor)
        {
            if (smoothSync)
            {
                lightSource.intensity = Mathf.Lerp(lightSource.intensity, InvertedValue, 2f * Time.deltaTime);
            }
            else
            {
                lightSource.intensity = InvertedValue;
            }
        }
    }




}
