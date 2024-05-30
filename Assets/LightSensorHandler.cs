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


    public float calibrationValue = 100000;
    public float normalizedMax => calibrationValue * dataMultiplier;

    public float InvertedValue => normalizedMax - LightValue;

    public float LightValue => dataMultiplier * lightSensorData;

    public void UpdateLightData(DataReceiver.SensorData sensorData) => lightSensorData = sensorData.light;

    private DataReceiver dataReceiver;


    #region Simple Mode

    public bool isSimpleMode;
    public float thresholdToDim;
    public float simpleModeBrightIntensity;

    public bool IsDim => calibrationValue + thresholdToDim < lightSensorData;

    #endregion


    public void CalibrateTheSensor()
    {
        calibrationValue = lightSensorData;
    }

    private void Start()
    {
        dataReceiver = FindObjectOfType<DataReceiver>();
        dataReceiver.OnDataReceiveWithData += UpdateLightData;
    }

    private void OnDestroy()
    {
        dataReceiver.OnDataReceiveWithData -= UpdateLightData;
    }

    private void Update()
    {
        if (syncWithSensor)
        {
            if (smoothSync)
            {
                if (isSimpleMode)
                {
                    if (IsDim)
                    {
                        lightSource.intensity = Mathf.Lerp(lightSource.intensity, 0, 2f * Time.deltaTime);
                    }
                    else
                    {
                        lightSource.intensity = Mathf.Lerp(lightSource.intensity, simpleModeBrightIntensity, 2f * Time.deltaTime);
                    }
                }
                else
                {
                    lightSource.intensity = Mathf.Lerp(lightSource.intensity, InvertedValue, 2f * Time.deltaTime);
                }
            }
            else
            {

                if (isSimpleMode)
                {
                    if (IsDim)
                    {
                        lightSource.intensity = 0;
                    }
                    else
                    {
                        lightSource.intensity = simpleModeBrightIntensity;
                    }
                }
                else
                {
                    lightSource.intensity = InvertedValue;
                }
            }
        }
    }




}
