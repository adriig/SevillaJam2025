using UnityEngine;

[CreateAssetMenu(fileName = "New BubbleJitterClickTool", menuName = "Game/Tool/BubbleJitterClickTool")]
public class BubbleJitterClickTool : Tool
{
    public new void InitializeTool()
    {
        base.InitializeTool();
        Debug.Log("Initializing BubbleJitterClickTool");
    }

    public new void UseTool()
    {
        base.UseTool();
        Debug.Log("Using BubbleJitterClickTool");
    }
}