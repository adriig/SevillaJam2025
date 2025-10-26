using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]
    private Image characterRenderer;

    [SerializeField]
    private Image overflowCharacterRenderer;

    [SerializeField]
    private HorizontalLayoutGroup toolsRenderer;

    [Header("Clock UI")]
    [SerializeField]
    private GameObject clock;

    [SerializeField]
    private List<Sprite> clockSprites;

    [Header("Character GameObjects")]
    [Header("Skeleton Character Objects")]
    [SerializeField]
    public GameObject skeletonHoldOnZone;

    [SerializeField]
    public GameObject skeletonDraggableBone;

    [SerializeField]
    public GameObject skeletonBoneDropArea;

    [SerializeField]
    public GameObject skeletonHoldOnBone1;

    [SerializeField]
    public GameObject skeletonHoldOnBone2;

    [SerializeField]
    public GameObject skeletonHoldOnBone3;

    [Header("Golem Character Objects")]
    [SerializeField]
    public GameObject golemHoldOnZone;
    [SerializeField]
    public GameObject golemBubbleClick1;
    [SerializeField]
    public GameObject golemBubbleClick2;
    [SerializeField]
    public GameObject golemBubbleClick3;
    [SerializeField]
    public GameObject golemHoldOnZone2;
    [Header("Slime Character Objects")]
    [SerializeField]
    public GameObject slimeBubbleClick1;
    [SerializeField]
    public GameObject slimeBubbleClick2;
    [SerializeField]
    public GameObject slimeBubbleClick3;
    [SerializeField]
    public GameObject slimeBubbleCClick4;
    [SerializeField]
    public GameObject slimeBubbleClick5;
    [SerializeField]
    public GameObject slimeSword;
    [SerializeField]
    public GameObject slimeSwordSafeArea;
    [Header("PixelArt Character Objects")]
    [SerializeField]
    public GameObject pixelBubbleClick;
    [SerializeField]
    public GameObject pixelArea1;
    [SerializeField]
    public GameObject pixelArea2;

    [HideInInspector]
    public static UIGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateCharacterImage(Sprite characterSprite)
    {
        if (characterRenderer != null)
        {
            characterRenderer.sprite = characterSprite;
        }
    }

    public void UpdateOverflowCharacterImage(Sprite overflowSprite)
    {
        if (overflowCharacterRenderer != null)
        {
            if (overflowSprite == null)
            {
                overflowCharacterRenderer.gameObject.SetActive(false);
            }
            else
            {
                overflowCharacterRenderer.gameObject.SetActive(true);
                overflowCharacterRenderer.sprite = overflowSprite;
                SetOverflowCharacterOpacity(0f);
            }
        }
    }

    public void SetOverflowCharacterOpacity(float opacityPercentage)
    {
        if (overflowCharacterRenderer != null)
        {
            float invertedPercentage = 100f - Mathf.Clamp(opacityPercentage, 0f, 100f);
            float alpha = invertedPercentage / 100f;

            Color color = overflowCharacterRenderer.color;
            color.a = alpha;
            overflowCharacterRenderer.color = color;

            if (alpha == 0f)
            {
                overflowCharacterRenderer.gameObject.SetActive(false);
            }
            else if (!overflowCharacterRenderer.gameObject.activeSelf)
            {
                overflowCharacterRenderer.gameObject.SetActive(true);
            }
        }
    }

    public void ResetTools()
    {
        foreach (Transform child in toolsRenderer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RenderTools(List<Tool> tools)
    {
        ResetTools();
        foreach (var tool in tools)
        {
            GameObject toolIcon = new GameObject(tool.name);
            Image iconImage = toolIcon.AddComponent<Image>();
            iconImage.sprite = tool.icon;
            toolIcon.transform.SetParent(toolsRenderer.transform);
        }
    }

    internal GameObject getGameObject(string key)
    {
        Debug.Log(key);
        switch (key)
        {
            case "Skeleton_HoldOnZone":
                return skeletonHoldOnZone;
            case "Skeleton_Draggable":
                return skeletonDraggableBone;
            case "Skeleton_DropArea":
                return skeletonBoneDropArea;
            case "Skeleton_1HoldOnZone":
                return skeletonHoldOnBone1;
            case "Skeleton_2HoldOnZone":
                return skeletonHoldOnBone2;
            case "Skeleton_3HoldOnZone":
                return skeletonHoldOnBone3;
            case "Golem_HoldOnZone":
                return golemHoldOnZone;
            case "Golem_1BubbleClick":
                return golemBubbleClick1;
            case "Golem_2BubbleClick":
                return golemBubbleClick2;
            case "Golem_3BubbleClick":
                return golemBubbleClick3;
            case "Golem_2HoldOnZone":
                return golemHoldOnZone2;
            case "Slime_1BubbleClick":
                return slimeBubbleClick1;
            case "Slime_2BubbleClick":
                return slimeBubbleClick2;
            case "Slime_3BubbleClick":
                return slimeBubbleClick3;
            case "Slime_4BubbleClick":
                return slimeBubbleCClick4;
            case "Slime_5BubbleClick":
                return slimeBubbleClick5;
            case "Slime_Draggable":
                return slimeSword;
            case "Slime_DropArea":
                return slimeSwordSafeArea;
            case "PixelArt_BubbleClick":
                return pixelBubbleClick;
            case "PixelArt_1HoldOnZone":
                return pixelArea1;
            case "PixelArt_2HoldOnZone":
                return pixelArea2;
            default:
                Debug.LogWarning("No GameObject found for key: " + key);
                return null;
        }
    }

    public void UpdateClockSprite(int timeRemaining)
    {
        if (clock == null || clockSprites == null || clockSprites.Count == 0)
        {
            return;
        }

        // Total time is always 60 seconds, 13 sprites
        const int totalTime = 60;
        const int totalSprites = 13;
        float timePerSprite = (float)totalTime / (float)totalSprites; // 60/13 ≈ 4.615 seconds per sprite

        // Calculate sprite index based on remaining time (inverted so it progresses forward)
        int spriteIndex = Mathf.FloorToInt((totalTime - timeRemaining) / timePerSprite);
        spriteIndex = Mathf.Clamp(spriteIndex, 0, totalSprites - 1);

        // Ensure we don't exceed available sprites
        if (spriteIndex >= clockSprites.Count)
        {
            spriteIndex = clockSprites.Count - 1;
        }

        // Try to update Image component
        Image clockImage = clock.GetComponent<Image>();
        if (clockImage != null)
        {
            clockImage.sprite = clockSprites[spriteIndex];
            return;
        }

        // Try to update SpriteRenderer component
        SpriteRenderer clockSpriteRenderer = clock.GetComponent<SpriteRenderer>();
        if (clockSpriteRenderer != null)
        {
            clockSpriteRenderer.sprite = clockSprites[spriteIndex];
        }
    }
}

internal class SeraializeFieldAttribute : Attribute { }
