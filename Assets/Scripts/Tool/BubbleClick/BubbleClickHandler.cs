using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleClickHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private int requiredClicks;

    private int currentClicks = 0;
    private bool isPointerInside = false;
    private bool isCompleted = false;

    public void SetRequiredClicks(int clicks)
    {
        if (requiredClicks <= 0)
            requiredClicks = Mathf.Max(1, clicks);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !isCompleted && isPointerInside)
        {
            currentClicks++;
            if (currentClicks >= requiredClicks)
            {
                OnClicksComplete();
            }
        }
    }

    private void OnClicksComplete()
    {
        isCompleted = true;
        GameManager.Instance.Next();
    }

    public void Reset()
    {
        isCompleted = false;
        currentClicks = 0;
    }
}
