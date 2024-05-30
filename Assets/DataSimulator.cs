using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DataSimulator : MonoBehaviour
{

    public Toggle simulationToggle;
    private bool isActive;
    private DataReceiver dataReceiver;

    public Button setDataButton;
    public InputField humidityInput, lightInput, airQualityInput, temperatureInput;



    private void Start()
    {
        dataReceiver = FindObjectOfType<DataReceiver>();
        dataReceiver.OnDataStartReceiving += KillSimulation;
        setDataButton.onClick.AddListener(SetData);

        UpdateUI();
    }

    public void StartSimulation()
    {
        dataReceiver.isSimulated = true;
        StartCoroutine(SimulationProcedure());
    }

    public void KillSimulation()
    {
        dataReceiver.isSimulated = false;
        StopAllCoroutines();
    }

    public void SwitchState()
    {
        isActive = !isActive;

        if (!isActive)
        {
            KillSimulation();
        }
        else
        {
            StartSimulation();
        }

        UpdateUI();
    }

    public IEnumerator SimulationProcedure()
    {
        while (true)
        {
            dataReceiver.SimulationPush();
            yield return new WaitForSeconds(0.2f);
            yield return null;
        }
    }

    public void SetData()
    {
        float temp = 0;
        float air = 0;
        float hum = 0;
        float light = 0;

        if (airQualityInput.text != null)
        {
            air = Mathf.Clamp01(float.Parse(airQualityInput.text));
        }
        if (temperatureInput.text != null)
        {
            temp = Mathf.Clamp(float.Parse(temperatureInput.text), 0, 100);
        }
        if (humidityInput.text != null)
        {
            hum = Mathf.Clamp(float.Parse(humidityInput.text), 0, 100);
        }
        if (lightInput.text != null)
        {
            light = float.Parse(lightInput.text);
        }

        dataReceiver.simulatedData.airQuality = air;
        dataReceiver.simulatedData.temperature = temp;
        dataReceiver.simulatedData.humidity = hum;
        dataReceiver.simulatedData.light = light;
    }


    private void UpdateUI()
    {
        simulationToggle.SetIsOnWithoutNotify(isActive);
    }


    private void OnDestroy()
    {
        setDataButton.onClick.RemoveAllListeners();
        dataReceiver.OnDataStartReceiving -= KillSimulation;
    }



}
