using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TCPSocketClient : MonoBehaviour
{
    // Raspberry Pi'nin IP adresi ve port numarası
    private string serverAddress = "192.168.2.35"; // Raspberry Pi'nin IP adresi
    private int serverPort = 12345; // Raspberry Pi ile iletişim için kullanılacak port numarası

    private TcpClient client; // TCP istemcisi
    private NetworkStream stream; // Ağ akışı

    public string customString;


    [ContextMenu("SendCustomData")]
    async void SendCustomData()
    {
        await SendDataAsync(customString);
    }

    async void Start()
    {
        // TCP istemcisini başlatma ve Raspberry Pi'ye bağlanma
        client = new TcpClient();
        await client.ConnectAsync(serverAddress, serverPort);
        Debug.Log("Bağlantı kuruldu.");

        // Ağ akışını elde etme
        stream = client.GetStream();

        // Raspberry Pi'ye veri gönderme
        await SendDataAsync("Merhaba Raspberry Pi!");

        // Raspberry Pi'den veri alma
        string receivedData = await ReceiveDataAsync();
        Debug.Log("Raspberry Pi'den gelen veri: " + receivedData);

        // Alınan veriyi işleme
        // Örneğin, sıcaklık değerini almak için veriyi işleyebilir ve Unity sahnesinde bir şeyler yapabilirsiniz.
    }

    void OnDestroy()
    {
        // Ağ akışını ve TCP istemcisini kapatma ve serbest bırakma
        if (stream != null)
        {
            stream.Close();
        }
        if (client != null)
        {
            client.Close();
        }
    }

    // Raspberry Pi'ye veri gönderme
    async Task SendDataAsync(string data)
    {
        // Gönderilecek veriyi UTF-8 kodlamasına dönüştür
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        // Veriyi ağ akışına asenkron olarak yazma
        await stream.WriteAsync(dataBytes, 0, dataBytes.Length);
        Debug.Log("Veri gönderildi: " + data);
    }

    // Raspberry Pi'den veri alma
    async Task<string> ReceiveDataAsync()
    {
        // Alınacak veri için bir tampon oluştur
        byte[] buffer = new byte[1024];

        // Veriyi ağ akışından asenkron olarak okuma
        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

        // Alınan veriyi UTF-8 kodlamasına dönüştür ve döndür
        string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        return data;
    }
}