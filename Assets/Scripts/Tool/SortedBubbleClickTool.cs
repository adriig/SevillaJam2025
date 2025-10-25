using UnityEngine;

[CreateAssetMenu(fileName = "New SortedBubbleClickTool", menuName = "Game/Tool/SortedBubbleClickTool")]
public class SortedBubbleClickTool : Tool
{
    public new void InitializeTool()
    {
        base.InitializeTool();
        Debug.Log("Initializing SortedBubbleClickTool");
    }

    public new void UseTool()
    {
        base.UseTool();
        Debug.Log("Using SortedBubbleClickTool");
    }
}