using UnityEngine;

public class ModelViewerCamera : MonoBehaviour
{
    public Transform target; // Hedef nesnenin referansı
    public float rotationSpeed = 5f; // Döndürme hızı
    public float autoRotationSpeed = 2f; // Otomatik dönüş hızı
    public float zoomSpeed = 5f; // Zoom hızı
    public float minZoom = 1f; // Min zoom mesafesi
    public float maxZoom = 10f; // Max zoom mesafesi
    public float minYHeight = 1f; // Min yükseklik
    public float maxYHeight = 5f; // Max yükseklik

    private float currentZoom = 5f;
    private bool isRotating = false;
    

    private Vector3 lastMousePosition;

    private void Start()
    {
        currentZoom = 10;
    }

    void Update()
    {
        // Mouse girişleri alınır
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Sol fare düğmesi basılı tutuluyor mu kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        // Sol fare düğmesi basılı tutuluyorsa kamerayı döndürme
        if (isRotating)
        {
            float deltaX = Input.mousePosition.x - lastMousePosition.x;
            float deltaY = Input.mousePosition.y - lastMousePosition.y;

            transform.RotateAround(target.position, Vector3.up, rotationSpeed * deltaX);
            transform.RotateAround(target.position, transform.right, -rotationSpeed * deltaY);

            lastMousePosition = Input.mousePosition;
        }
        else
        {
            // Sol fare düğmesi basılı değilse kamera kendi etrafında otomatik olarak dönmeye devam etme
            transform.RotateAround(target.position, Vector3.up, autoRotationSpeed * Time.deltaTime);
        }

        // Zoom yapma
        currentZoom -= scrollInput * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Kamerayı hedefe doğru kaydırma
        transform.LookAt(target);

        // Kamera yüksekliği ayarı
        float yHeight = Mathf.Clamp(transform.position.y, minYHeight, maxYHeight);
        transform.position = new Vector3(transform.position.x, yHeight, transform.position.z);

        // Kamera zoom ayarı
        transform.position = (transform.position - target.position).normalized * currentZoom + target.position;
    }


    public void ChangeToTarget(GameObject _newTar)
    {

        target = _newTar.transform;

    }

}
