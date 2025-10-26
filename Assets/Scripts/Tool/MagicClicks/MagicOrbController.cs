using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicOrbController : MonoBehaviour, IPointerClickHandler
{
    [Header("Animation Settings")]
    [SerializeField]
    private float animationTime = 1f; // total time for the full 12-frame animation

    private float elapsedTime = 0f;
    private int totalFrames = 12;
    private int currentFrame = 0;
    private float frameInterval = 0f;

    private bool wasClicked = false;
    private bool initialized = false;
    private MagicClicksTool parentTool;

    [SerializeField]
    public List<Sprite> orbSprites;

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
        // Setup animation timings
        animationTime = Mathf.Max(0.01f, animationTime);
        frameInterval = animationTime / (float)totalFrames;
        elapsedTime = 0f;
        currentFrame = 0;
        initialized = true;

        Debug.Log($"MagicOrbController: Start - animationTime: {animationTime}s, frameInterval: {frameInterval}s");

        if (GetComponent<Image>() == null && GetComponent<SpriteRenderer>() == null && GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning("MagicOrbController: El GameObject necesita un Image (UI) o SpriteRenderer para mostrar sprites, o un Collider2D para detectar clicks");
        }

        // Initialize first frame if available
        ApplyFrameSprite(currentFrame);
    }

    private void Update()
    {
        if (!initialized || wasClicked)
            return;

        // Advance animation time
        elapsedTime += Time.deltaTime;

        // Calculate how many frames to advance (in case of frame drops)
        if (frameInterval > 0f)
        {
            int framesToAdvance = Mathf.FloorToInt(elapsedTime / frameInterval);
            if (framesToAdvance > 0)
            {
                elapsedTime -= framesToAdvance * frameInterval;
                for (int i = 0; i < framesToAdvance; i++)
                {
                    currentFrame++;
                    if (currentFrame >= totalFrames || currentFrame >= orbSprites.Count)
                    {
                        // Animation finished
                        Destroy(gameObject);
                        return;
                    }
                    ApplyFrameSprite(currentFrame);
                }
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
        elapsedTime = 0f;
        currentFrame = 0;
        wasClicked = false;
        initialized = true;
        ApplyFrameSprite(currentFrame);
        Debug.Log($"MagicOrbController: Reseteado - animationTime: {animationTime}s, totalFrames: {totalFrames}");
    }

    private void ApplyFrameSprite(int frameIndex)
    {
        if (orbSprites == null || orbSprites.Count == 0)
            return;

        int idx = Mathf.Clamp(frameIndex, 0, orbSprites.Count - 1);

        // Try UI Image first
        var img = GetComponent<Image>();
        if (img != null)
        {
            img.sprite = orbSprites[idx];
            return;
        }

        // Try SpriteRenderer
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = orbSprites[idx];
            return;
        }
    }
}
