using UnityEngine;
using System.Collections.Generic;

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
    public void InitializeCharacter()
    {
        activeTool = Tools[0];
        if (Sprites[0].type == CharacterSpriteType.Overflow)
        {
            activeOverflowCharacterSprite = Sprites[0] as OverflowCharacterSprite;
        }
        else
        {
            activeOverflowCharacterSprite = null;
        }
        activeCharacterSprite = Sprites[1];
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

    public void NextTool()
    {
        int currentIndex = Tools.IndexOf(activeTool);
        int nextIndex = (currentIndex + 1) % Tools.Count;
        activeTool = Tools[nextIndex];
    }

    public void NextCharacterSprite()
    {
        int currentIndex = Sprites.IndexOf(activeCharacterSprite);
        int nextIndex = (currentIndex + 1) % Sprites.Count;
        if (Sprites[nextIndex].type == CharacterSpriteType.Overflow)
        {
            activeOverflowCharacterSprite = Sprites[nextIndex] as OverflowCharacterSprite;
            nextIndex = (nextIndex + 1) % Sprites.Count;
        }
        activeCharacterSprite = Sprites[nextIndex];
    }

    public Tool GetActiveTool()
    {
        Debug.Log(activeTool);
        return activeTool;
    }
}