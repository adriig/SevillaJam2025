using UnityEngine;
using UnityEngine.EventSystems;

public class BubbleHandler : MonoBehaviour, IPointerClickHandler
{
    [Header("Bubble Settings")]
    [SerializeField] private float shrinkAmount = 0.2f;  // Cuánto se reduce en cada click
    [SerializeField] private float minSize = 0.5f;       // Tamaño mínimo antes de destruirse
    
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
        Debug.Log($"Click detectado en {gameObject.name}");

        if (GameManager.Instance.activeTool != null)
        {
            HandleBubbleClick();
        }
    }

    private void HandleBubbleClick()
    {
        clickCount++;
        
        // Reducir el tamaño
        Vector3 newScale = transform.localScale - (Vector3.one * shrinkAmount);
        
        // Asegurarse de que no sea más pequeño que el tamaño mínimo
        if (newScale.x < minSize || newScale.y < minSize)
        {
            // Si es demasiado pequeño, destruir la burbuja
            Destroy(gameObject);
            return;
        }
        
        // Aplicar la nueva escala
        transform.localScale = newScale;
        
        // Opcional: ajustar el collider si es circular
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