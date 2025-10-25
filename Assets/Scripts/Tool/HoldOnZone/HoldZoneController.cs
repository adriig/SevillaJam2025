using UnityEngine;
using UnityEngine.EventSystems;

public class HoldZoneController : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] private float requiredHoldDistance = 2500f;
    private bool isPointerInside = false;
    private bool isClickHeld = false;
    private bool isCompleted = false;
    private Vector2 lastPointerPosition;
    private float accumulatedDistance = 1f;

    private void Update()
    {
        if (isClickHeld && !isCompleted)
        {
            if (accumulatedDistance >= requiredHoldDistance)
            {
                OnHoldComplete();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
        if (isClickHeld && !isCompleted)
        {
            lastPointerPosition = eventData.position;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !isCompleted)
        {
            isClickHeld = true;
            lastPointerPosition = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isClickHeld = false;
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (isClickHeld && isPointerInside && !isCompleted)
        {
            Vector2 currentPos = eventData.position;
            float delta = Vector2.Distance(currentPos, lastPointerPosition);
            accumulatedDistance += delta;
            lastPointerPosition = currentPos;
            float progress = Mathf.Clamp01(accumulatedDistance / requiredHoldDistance) * 100f;
            UIGameManager.Instance.SetOverflowCharacterOpacity(progress);
        }
    }

    private void OnHoldComplete()
    {
        isCompleted = true;
        isClickHeld = false;
        GameManager.Instance.Next();
    }

}
