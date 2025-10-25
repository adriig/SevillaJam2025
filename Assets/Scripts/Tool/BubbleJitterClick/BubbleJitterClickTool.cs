using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BubbleJitterClickTool", menuName = "Game/Tool/BubbleJitterClickTool")]
public class BubbleJitterClickTool : Tool
{
    [SerializeField] public List<GameObject> bubblesRef;
    public new void InitializeTool()
    {
        base.InitializeTool();
        Debug.Log("Initializing BubbleJitterClickTool");
    }

    public new void UseTool(Characters character)
    {
        base.UseTool(character);
        Debug.Log($"Using BubbleJitterClickTool on {character}");
    }
}