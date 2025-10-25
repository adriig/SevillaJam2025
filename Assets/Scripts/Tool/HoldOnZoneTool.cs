using UnityEngine;

[CreateAssetMenu(fileName = "New HoldOnZoneTool", menuName = "Game/Tool/HoldOnZoneTool")]
public class HoldOnZoneTool : Tool
{
    public new void InitializeTool()
    {
        base.InitializeTool();
        Debug.Log("Initializing HoldOnZoneTool");
    }

    public new void UseTool()
    {
        base.UseTool();
        Debug.Log("Using HoldOnZoneTool");
    }
}