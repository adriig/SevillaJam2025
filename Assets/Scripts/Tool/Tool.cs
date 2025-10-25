using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Game/Tool")]
public abstract class Tool : ScriptableObject
{
    [SerializeField] public Sprite icon;
    [SerializeField] public bool cursorReplace;

    public void SetToolCursor()
    {
        if (cursorReplace)
        {
            Debug.Log("Setting cursor");
            Cursor.SetCursor(icon.texture, Vector2.zero, CursorMode.Auto);
        }
    }

    public void ResetCursor()
    {
        if (cursorReplace)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void InitializeTool()
    {
        Debug.Log("Initializing tool");
    }

    public void UseTool()
    {
        Debug.Log("Using tool");
    }
}