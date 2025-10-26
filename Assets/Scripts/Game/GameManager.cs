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

        [Header("Per-Character Timer")]
        [SerializeField]
        private int characterTimeSeconds = 60;

        [SerializeField]
        private bool autoAdvanceOnTimeout = false;

        private Coroutine characterTimerCoroutine;
        private int characterTimeRemaining;

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
        if (currentIndex == characters.Count - 1)
        {
            NoMoreCharacters();
            return;
        }
        int nextIndex = (currentIndex + 1) % characters.Count;
        activeCharacter = characters[nextIndex];
        HandleActiveCharacter();
    }

    private void StartCharacterTimer()
    {
        if (characterTimerCoroutine != null)
        {
            StopCoroutine(characterTimerCoroutine);
            characterTimerCoroutine = null;
        }

        characterTimeRemaining = Mathf.Max(0, characterTimeSeconds);
        characterTimerCoroutine = StartCoroutine(CharacterTimerRoutine());
    }

    private IEnumerator CharacterTimerRoutine()
    {
        int elapsed = 0;
        
        if (UIGameManager.Instance != null)
        {
            UIGameManager.Instance.UpdateClockSprite(characterTimeRemaining);
        }
        
        while (characterTimeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            characterTimeRemaining -= 1;
            elapsed += 1;
            Debug.Log($"[GameManager] Character '{activeCharacter?.name}' - seconds elapsed: {elapsed}, remaining: {characterTimeRemaining}s");
            
            if (UIGameManager.Instance != null)
            {
                UIGameManager.Instance.UpdateClockSprite(characterTimeRemaining);
            }
        }

        Debug.Log($"[GameManager] Character '{activeCharacter?.name}' timer finished.");

        characterTimerCoroutine = null;

        if (autoAdvanceOnTimeout)
        {
            NextCharacter();
        }
        else
        {
            Time.timeScale = 0f;
            Debug.Log("[GameManager] Time reached 0 - Game Over!");
            
            if (WinLoseMenu.Instance != null)
            {
                WinLoseMenu.Instance.ShowLoseMenu();
            }
            else
            {
                Debug.LogError("[GameManager] WinLoseMenu.Instance is null - cannot show lose menu.");
            }
        }
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

    public void NoMoreSprites()
    {
        NextCharacter();
    }

    public void NoMoreCharacters()
    {
        Time.timeScale = 0f;
        Debug.Log("[GameManager] No more characters - You Win!");

        if (WinLoseMenu.Instance != null)
        {
            WinLoseMenu.Instance.ShowWinMenu();
        }
        else
        {
            Debug.LogError("[GameManager] WinLoseMenu.Instance is null - cannot show win menu.");
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
            yield return new WaitForSeconds(4f);
            NextSprite();
        }
    }

    private void NextSprite()
    {
        activeCharacter.NextCharacterSprite();
        UpdateUI();
    }

    public void HandleActiveCharacter()
    {
        StopSpriteChange();
        
        activeCharacter.InitializeCharacter();
        SetActiveTool(activeCharacter.activeTool.tool);
        UpdateUI();
        StartCharacterTimer();
    }

    public void UpdateUI()
    {
        UIGameManager.Instance.UpdateCharacterImage(activeCharacter.activeCharacterSprite.sprite);
        UIGameManager.Instance.UpdateOverflowCharacterImage(
            activeCharacter.activeOverflowCharacterSprite?.sprite
        );
    }
}
