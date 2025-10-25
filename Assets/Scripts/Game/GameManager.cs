using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Character> characters;
    [HideInInspector]
    public Character activeCharacter = null;
    [HideInInspector]
    public Tool activeTool = null;
    public static GameManager Instance { get; private set; }
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

    public void Start()
    {
        if (characters == null || characters.Count == 0)
        {
            throw new Exception("No characters assigned to GameManager.");
        }
        activeCharacter = characters[0];
        HandleActiveCharacter();
    }

    private void SetActiveTool(Tool newTool)
    {
        if (activeTool != null)
        {
            activeTool.ResetCursor();
        }
        activeTool = newTool;
        if (activeTool != null)
        {
            activeTool.SetToolCursor();
        }
    }

    public void NextCharacter()
    {
        int currentIndex = characters.IndexOf(activeCharacter);
        int nextIndex = (currentIndex + 1) % characters.Count;
        activeCharacter = characters[nextIndex];
        HandleActiveCharacter();
    }

    public void Next()
    {
        NextTool();
        NextSprite();
    }

    private void NextTool()
    {
        activeTool.DisableAll();
        SetActiveTool(activeCharacter.NextTool());
    }

    private Coroutine spriteChangeCoroutine;
    private bool isChangingSprites = false;

    public void NoMoreTools()
    {
        if (!isChangingSprites)
        {
            isChangingSprites = true;
            spriteChangeCoroutine = StartCoroutine(AutomaticSpriteChange());
        }
    }

    public void StopSpriteChange()
    {
        if (isChangingSprites && spriteChangeCoroutine != null)
        {
            StopCoroutine(spriteChangeCoroutine);
            isChangingSprites = false;
        }
    }

    private IEnumerator AutomaticSpriteChange()
    {
        while (isChangingSprites)
        {
            yield return new WaitForSeconds(2f); // Espera 2 segundos
            NextSprite();
        }
    }

    private void NextSprite()
    {
        activeCharacter.NextCharacterSprite();
        Debug.Log(activeCharacter.activeOverflowCharacterSprite);
        UpdateUI();
    }

    public void HandleActiveCharacter()
    {
        activeCharacter.InitializeCharacter();
        SetActiveTool(activeCharacter.activeTool);
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIGameManager.Instance.UpdateCharacterImage(activeCharacter.activeCharacterSprite.sprite);
        UIGameManager.Instance.UpdateOverflowCharacterImage(activeCharacter.activeOverflowCharacterSprite?.sprite);
        UIGameManager.Instance.RenderTools(activeCharacter.Tools);

    }
}
