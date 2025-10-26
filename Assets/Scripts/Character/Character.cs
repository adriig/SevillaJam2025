using System.Collections.Generic;
using UnityEngine;

public enum Characters
{
    Skeleton,
    Golem,
    Ghost,
    PixelArt,
}

[CreateAssetMenu(fileName = "New Character", menuName = "Game/Character")]
public class Character : ScriptableObject
{
    [SerializeField]
    public List<CharacterSprite> Sprites;

    [SerializeField]
    public List<CharacterTool> Tools;

    [HideInInspector]
    public CharacterTool activeTool = null;

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
        return activeTool.tool;
    }

    public void NextCharacterSprite()
    {
        int nextIndex = (currentSpriteIndex + 1) % Sprites.Count;

        if (nextIndex == 0)
        {
            Debug.Log("ULTIMO SPRITE");
            GameManager.Instance.NoMoreSprites();
            return;
        }

        if (Sprites[nextIndex].type == CharacterSpriteType.Overflow)
        {
            activeOverflowCharacterSprite = Sprites[nextIndex] as OverflowCharacterSprite;
            nextIndex = (nextIndex + 1) % Sprites.Count;
        }
        else
        {
            activeOverflowCharacterSprite = null;
        }
        activeCharacterSprite = Sprites[nextIndex];
        currentSpriteIndex = nextIndex;
    }

    public void UseActiveTool()
    {
        if (activeTool != null)
        {
            activeTool.tool.InitializeTool(character, activeTool.key);
            activeTool.tool.UseTool();
        }
        else
        {
            Debug.LogWarning("No active tool to use.");
        }
    }
}
