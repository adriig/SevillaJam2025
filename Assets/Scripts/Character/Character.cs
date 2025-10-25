using UnityEngine;
using System.Collections.Generic;

public enum Characters
{
    Skeleton,
    Golem,
    Ghost,
    PixelArt
}

[CreateAssetMenu(fileName = "New Character", menuName = "Game/Character")]
public class Character : ScriptableObject
{
    [SerializeField] public List<CharacterSprite> Sprites;
    [SerializeField] public List<Tool> Tools;
    [HideInInspector]
    public Tool activeTool = null;
    [HideInInspector]
    public CharacterSprite activeCharacterSprite = null;
    [HideInInspector]
    public OverflowCharacterSprite activeOverflowCharacterSprite = null;
    [HideInInspector]
    private int currentSpriteIndex = 0;
    [SerializeField]
    public Characters character;
    public void InitializeCharacter()
    {
        Debug.Log("Initializing character: " + name);
        activeTool = Tools[0];
        UseActiveTool();
        
        currentSpriteIndex = 0;
        if (Sprites[0].type == CharacterSpriteType.Overflow)
        {
            activeOverflowCharacterSprite = Sprites[0] as OverflowCharacterSprite;
            currentSpriteIndex = 1;
        }
        else
        {
            activeOverflowCharacterSprite = null;
        }
        activeCharacterSprite = Sprites[currentSpriteIndex];
    }

    public void SpriteChangeHandle()
    {
        // Lógica para manejar el cambio de sprite si es necesario
    }

    public void OnFinishToolHandle()
    {
        // Lógica para manejar el final de la herramienta si es necesario
        // NextTool();
    }

    public Tool NextTool()
    {
        int currentIndex = Tools.IndexOf(activeTool);
        if (currentIndex == Tools.Count - 1)
        {
            Debug.Log("ULTIMA HERRAMIENTA");
            GameManager.Instance.NoMoreTools();
            return null;
        }
        int nextIndex = (currentIndex + 1) % Tools.Count;
        activeTool = Tools[nextIndex];
        UseActiveTool();
        return activeTool;
    }

    public void NextCharacterSprite()
    {
        Debug.Log("Current active sprite: " + activeCharacterSprite.name);
        Debug.Log("Current overflow sprite: " + activeOverflowCharacterSprite?.name);
        Debug.Log("Total sprites in list: " + Sprites.Count);
        
        int nextIndex = (currentSpriteIndex + 1) % Sprites.Count;
        Debug.Log($"Initial indices - Current: {currentSpriteIndex}, Next: {nextIndex}");
        
        Debug.Log($"Sprite at nextIndex ({nextIndex}) is of type: {Sprites[nextIndex].type}");
        
        if (Sprites[nextIndex].type == CharacterSpriteType.Overflow)
        {
            Debug.Log("Found overflow sprite, updating indices...");
            activeOverflowCharacterSprite = Sprites[nextIndex] as OverflowCharacterSprite;
            nextIndex = (nextIndex + 1) % Sprites.Count;
            Debug.Log($"After overflow adjustment - Next: {nextIndex}");
        }
        else
        {
            Debug.Log("No overflow sprite found");
            activeOverflowCharacterSprite = null;
        }
        activeCharacterSprite = Sprites[nextIndex];
        currentSpriteIndex = nextIndex; // Actualizamos el índice actual
        Debug.Log("Updated active sprite to: " + activeCharacterSprite.name);
        Debug.Log(nextIndex + " - " + Sprites.IndexOf(activeCharacterSprite));
        Debug.Log(activeCharacterSprite.name);
        Debug.Log(activeOverflowCharacterSprite?.name);
        Debug.Log("------------------------------");
    }

    public void UseActiveTool()
    {
        if (activeTool != null)
        {
            activeTool.InitializeTool();
            activeTool.UseTool(character);
        }
        else
        {
            Debug.LogWarning("No active tool to use.");
        }
    }
}