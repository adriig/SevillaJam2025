using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleHandler : MonoBehaviour, IPointerClickHandler
{
    [Header("Bubble Settings")]
    [SerializeField] private float shrinkAmount = 0.2f;
    [SerializeField] private float minSize = 0.5f;

    private Vector3 originalScale;
    private int clickCount = 0;

    private void Start()
    {
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning("BubbleHandler necesita un Collider2D para detectar clicks");
        }

        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.activeTool != null)
        {
            HandleBubbleClick();
        }
    }

    private void HandleBubbleClick()
    {
        clickCount++;

        Vector3 newScale = transform.localScale - (Vector3.one * shrinkAmount);

        if (newScale.x < minSize || newScale.y < minSize)
        {
            Destroy(gameObject);
            return;
        }

        transform.localScale = newScale;

        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider != null)
        {
            circleCollider.radius = newScale.x / 2f;
        }
    }

    public void ResetBubble()
    {
        transform.localScale = originalScale;
        clickCount = 0;
    }
}