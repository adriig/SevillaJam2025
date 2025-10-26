using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New BubbleJitterClickTool",
    menuName = "Game/Tool/BubbleJitterClickTool"
)]
public class BubbleJitterClickTool : Tool
{
    [SerializeField]
    public List<GameObject> bubblesRef;

    public new void InitializeTool(Characters character, string key)
    {
        base.InitializeTool(character, key);
    }

    public new void UseTool()
    {
        base.UseTool();
    }
}
