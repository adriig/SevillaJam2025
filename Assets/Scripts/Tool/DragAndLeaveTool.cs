using UnityEngine;

[CreateAssetMenu(fileName = "New DragAndLeaveTool", menuName = "Game/Tool/DragAndLeaveTool")]
public class DragAndLeaveTool : Tool
{
    public new void InitializeTool()
    {
        base.InitializeTool();
        Debug.Log("Initializing DragAndLeaveTool");
    }

    public new void UseTool()
    {
        base.UseTool();
        Debug.Log("Using DragAndLeaveTool");
    }
}