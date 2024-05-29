using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public float distanceToOpen, farDistance, dist, alpha;
    private Transform camTransf;
    private CanvasGroup canvasGroup;

    private bool isOnLimit;

    void Start()
    {
        camTransf = Camera.main.transform;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
         dist = Vector3.Distance(camTransf.position, transform.position);

        if (dist < farDistance)
        {
            isOnLimit = true;
        }
        else
        {
            isOnLimit = false;
        }

        if (isOnLimit)
        {
             alpha = 1 - dist / distanceToOpen;
            alpha = Mathf.Clamp01(alpha);

            // Alpha değerini güncelle
            canvasGroup.alpha = alpha;
        }
        else
        {
            // Eğer limit dışındaysa, alpha değerini sıfıra ayarla
            canvasGroup.alpha = 0f;
        }
    }
}