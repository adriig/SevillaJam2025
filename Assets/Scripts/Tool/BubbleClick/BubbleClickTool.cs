using UnityEngine;

[CreateAssetMenu(fileName = "New BubbleClickTool", menuName = "Game/Tool/BubbleClickTool")]
public class BubbleClickTool : Tool
{
    [SerializeField]
    private int clicksNeeded = 3;

    private GameObject bubbleClickObject;

    public override void InitializeTool(Characters character, string key)
    {
        base.InitializeTool(character, key + "BubbleClick");
        bubbleClickObject = GetGameObject(character, key + "BubbleClick");
        if (!bubbleClickObject)
        {
            Debug.LogError("BubbleClick GameObject not found for key: " + key);
            return;
        }

        BubbleClickHandler handler = bubbleClickObject.GetComponent<BubbleClickHandler>();
        if (!handler)
        {
            Debug.LogError("BubbleClickHandler component not found on: " + bubbleClickObject.name);
            return;
        }

        handler.SetRequiredClicks(clicksNeeded);
        handler.Reset();
    }

    public override void DisableAll()
    {
        base.DisableAll();
        if (bubbleClickObject)
        {
            bubbleClickObject.SetActive(false);
        }
    }

    public override void UseTool()
    {
        base.UseTool();
        if (bubbleClickObject)
        {
            bubbleClickObject.SetActive(true);
        }
    }
}