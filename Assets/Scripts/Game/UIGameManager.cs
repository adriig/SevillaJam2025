using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image characterRenderer;
    [SerializeField] private Image overflowCharacterRenderer;
    [SerializeField] private HorizontalLayoutGroup toolsRenderer;

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
}
