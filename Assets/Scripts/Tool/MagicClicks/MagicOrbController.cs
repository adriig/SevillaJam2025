using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MagicOrbController : MonoBehaviour, IPointerClickHandler
{
    [Header("Animation Settings")]
    [SerializeField]
    private float animationTime = 1f;

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
        if (transform.localScale == Vector3.zero || transform.localScale.x < 0.01f)
        {
            transform.localScale = Vector3.one;
            Debug.Log("MagicOrbController: Escala inicial era 0, configurada a (1,1,1)");
        }
    }

    private void Start()
    {
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

        ApplyFrameSprite(currentFrame);
    }

    private void Update()
    {
        if (!initialized || wasClicked)
            return;

        elapsedTime += Time.deltaTime;

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

        if (parentTool != null)
        {
            parentTool.OnOrbClicked();
        }

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

        var img = GetComponent<Image>();
        if (img != null)
        {
            img.sprite = orbSprites[idx];
            return;
        }

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = orbSprites[idx];
            return;
        }
    }
}
