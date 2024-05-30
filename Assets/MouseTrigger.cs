using UnityEngine;
using UnityEngine.Events;

public class ColliderTrigger : MonoBehaviour
{
    public UnityEvent onMouseClickTrigger;
    public bool isActiveWithHovering;

    private bool isMouseOver = false;

    private void OnMouseEnter()
    {
        isMouseOver = true;

        if (isActiveWithHovering && onMouseClickTrigger != null)
        {
            onMouseClickTrigger.Invoke();
        }
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    private void Update()
    {
        if (!isActiveWithHovering && isMouseOver && Input.GetMouseButtonDown(0))
        {
            if (onMouseClickTrigger != null)
            {
                onMouseClickTrigger.Invoke();
            }
        }
    }
}   