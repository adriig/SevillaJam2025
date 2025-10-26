using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldZoneController
    : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerMoveHandler
{
    [SerializeField]
    private float requiredHoldDistance = 2500f;
    
    [Header("Blink Effect")]
    [SerializeField]
    private float blinkSpeed = 1f; 
    
    private bool isPointerInside = false;
    private bool isClickHeld = false;
    private bool isCompleted = false;
    private Vector2 lastPointerPosition;
    private float accumulatedDistance = 1f;
    private Image imageComponent;
    private float blinkTimer = 0f;

    [SerializeField]
    private float maximumAlpha = 1f;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogWarning("HoldZoneController: No se encontró componente Image para efecto de parpadeo.");
        }
    }

    private void Update()
    {
        if (imageComponent != null && !isCompleted)
        {
            blinkTimer += Time.deltaTime * blinkSpeed;
            float alpha = Mathf.PingPong(blinkTimer, maximumAlpha);
            Color color = imageComponent.color;
            color.a = alpha;
            imageComponent.color = color;
        }

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
            Debug.Log($"HoldZoneController: Progreso de hold: {progress}%");
        }
    }

    private void OnHoldComplete()
    {
        isCompleted = true;
        isClickHeld = false;
        GameManager.Instance.Next();
    }

    public void Reset()
    {
        isCompleted = false;
        isClickHeld = false;
        accumulatedDistance = 1f;
        blinkTimer = 0f;
        
        if (imageComponent != null)
        {
            Color color = imageComponent.color;
            color.a = 0f;
            imageComponent.color = color;
        }
    }
}
