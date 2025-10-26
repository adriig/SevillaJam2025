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

    [Header("Character GameObjects")]
    [Header("Skeleton Character Objects")]
    [SerializeField]
    public GameObject skeletonHoldOnZone;

    [SerializeField]
    public GameObject skeletonBoneContainer;

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
    [SeraializeField]
    public GameObject golemHoldOnZone;

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
        switch (key)
        {
            case "Skeleton_HoldOnZone":
                return skeletonHoldOnZone;
            case "Skeleton_Container":
                return skeletonBoneContainer;
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
            default:
                Debug.LogWarning("No GameObject found for key: " + key);
                return null;
        }
    }
}

internal class SeraializeFieldAttribute : Attribute { }
