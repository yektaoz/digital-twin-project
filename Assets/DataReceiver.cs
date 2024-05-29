using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DataReceiver : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private string serverIp = "192.168.2.35"; // Raspberry Pi IP adresi
    private int serverPort = 12345;  // Sunucu bağlantı noktası

    public event Action OnDataStartReceiving;
    public event Action OnDataConnectionError;
    public event Action OnLightDataReceive;
    public event Action<SensorData> OnDataReceiveWithData;
    public event Action<float> OnLightDataReceiveWithValue;

    public SensorData lastReceivedData;
    private bool isDataConnected;


    async void Start()
    {

    }

    public void StartConnection()
    {
        GetSensorData();
    }

   [ContextMenu("Connect to get sensor data")]
    async void GetSensorData()
    {
        // TCP istemcisine bağlanma
        await ConnectToServerAsync();
    }

    async Task ConnectToServerAsync()
    {
        try
        {
            client = new TcpClient();
            await client.ConnectAsync(serverIp, serverPort);
            stream = client.GetStream();
            OnDataStartReceiving?.Invoke();
            isDataConnected = true;
            StartReceivingData();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Bağlantı hatası: {ex.Message}");
            OnDataConnectionError?.Invoke();
            isDataConnected = false;

        }
    }


    async void StartReceivingData()
    {
        byte[] buffer = new byte[256];

        try
        {
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log($"Received: {message}");

                    // JSON verisini SensorData nesnesine deserialize et
                    SensorData data = JsonUtility.FromJson<SensorData>(message);

                    lastReceivedData = data;

                    if (data != null)
                    {
                        Debug.Log($"Received light value: {data.light}");
                        Debug.Log($"Received temperature: {data.temperature}");
                        Debug.Log($"Received humidity: {data.humidity}");
                        Debug.Log($"Received air Quality: {data.airQuality}");

                        // Gelen lightValue değerini 0 ile 100000 arasında sınırlayın
                        data.light = Mathf.Clamp(data.light, 0, 100000);

                        // Event tetikleyin veya veriyi kullanın
                        OnLightDataReceiveWithValue?.Invoke(data.light);

                        // Diğer verileri burada işleyin
                        float temperature = data.temperature;
                        float humidity = data.humidity;
                        float airQuality = data.airQuality;

                        // İlgili işlemler
                        Debug.Log($"Processed temperature: {temperature}");
                        Debug.Log($"Processed humidity: {humidity}");
                        Debug.Log($"Processed airQuality: {airQuality}");

                        OnDataReceiveWithData?.Invoke(lastReceivedData);

                    }
                    else
                    {
                        Debug.LogError("Failed to parse JSON message!");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Veri okuma hatası: {ex.Message}");
        }
    }


    [System.Serializable]
    public class SensorData
    {
        public float temperature;
        public float humidity;
        public float light;
        public float airQuality;
    }

    void OnDestroy()
    {
        // Bağlantıları kapatma
        if (stream != null)
        {
            stream.Close();
        }

        if (client != null)
        {
            client.Close();
        }
    }
}