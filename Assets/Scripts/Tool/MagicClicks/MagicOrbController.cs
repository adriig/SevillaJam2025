using UnityEngine;
using UnityEngine.EventSystems;

public class MagicOrbController : MonoBehaviour, IPointerClickHandler
{
    [Header("Scale Settings")]
    [SerializeField]
    private float maxScale = 2f;

    [SerializeField]
    private float growSpeed = 1f;

    [SerializeField]
    private float shrinkSpeed = 3f;

    private Vector3 initialScale;
    private Vector3 targetScale;
    private bool isGrowing = true;
    private bool wasClicked = false;
    private bool initialized = false;
    private MagicClicksTool parentTool;

    public void SetParentTool(MagicClicksTool tool)
    {
        parentTool = tool;
    }

    private void Awake()
    {
        // Asegurar que el objeto tiene una escala válida inicial
        if (transform.localScale == Vector3.zero || transform.localScale.x < 0.01f)
        {
            transform.localScale = Vector3.one;
            Debug.Log("MagicOrbController: Escala inicial era 0, configurada a (1,1,1)");
        }
    }

    private void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale * maxScale;
        initialized = true;

        Debug.Log(
            $"MagicOrbController: Start - Escala inicial: {initialScale}, Target: {targetScale}"
        );

        if (GetComponent<UnityEngine.UI.Image>() == null && GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning(
                "MagicOrbController: El GameObject necesita un Image (UI) o Collider2D para detectar clicks"
            );
        }
    }

    private void Update()
    {
        if (!initialized || wasClicked)
            return;

        if (isGrowing)
        {
            // Crecer hacia maxScale
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                targetScale,
                growSpeed * Time.deltaTime
            );

            // Si alcanzamos el tamaño máximo (con tolerancia), empezar a decrecer
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                isGrowing = false;
                Debug.Log("MagicOrbController: Tamaño máximo alcanzado, comenzando a decrecer");
            }
        }
        else
        {
            // Decrecer rápidamente hacia 0
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                Vector3.zero,
                shrinkSpeed * Time.deltaTime
            );

            // Si es muy pequeño, destruir
            if (transform.localScale.x < 0.05f)
            {
                Debug.Log("MagicOrbController: Orbe demasiado pequeño, autodestruyéndose");
                Destroy(gameObject);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (wasClicked)
        {
            Debug.Log("MagicOrbController: Click ignorado (ya fue clickeado)");
            return;
        }

        wasClicked = true;
        Debug.Log("¡Orbe clickeado exitosamente! ✓");

        // Notificar al tool padre
        if (parentTool != null)
        {
            parentTool.OnOrbClicked();
        }

        // Destruir inmediatamente al hacer click
        Destroy(gameObject);
    }

    public void Reset()
    {
        if (initialScale == Vector3.zero)
        {
            initialScale = Vector3.one;
            Debug.Log("MagicOrbController: Reset - initialScale era zero, configurado a (1,1,1)");
        }

        transform.localScale = initialScale;
        targetScale = initialScale * maxScale;
        isGrowing = true;
        wasClicked = false;
        initialized = true;
        Debug.Log($"MagicOrbController: Reseteado - Escala: {initialScale}, Target: {targetScale}");
    }
}
