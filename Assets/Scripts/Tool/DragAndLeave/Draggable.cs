using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject safeArea;
    private Vector3 originalPosition;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            rectTransform.position = eventData.position;
        }
        else
        {
            Vector3 worldPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out worldPosition
            );
            rectTransform.position = worldPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (safeArea != null)
        {
            RectTransform safeAreaRect = safeArea.GetComponent<RectTransform>();
            if (safeAreaRect != null)
            {
                Vector2 localPointerPosition;
                if (
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        safeAreaRect,
                        eventData.position,
                        eventData.pressEventCamera,
                        out localPointerPosition
                    )
                )
                {
                    if (safeAreaRect.rect.Contains(localPointerPosition))
                    {
                        GameManager.Instance.Next();
                        return;
                    }
                }
            }
        }

        transform.position = originalPosition;
    }
}
