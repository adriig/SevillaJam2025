using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Tool", menuName = "Game/Tool")]
public abstract class Tool : ScriptableObject
{
    [SerializeField]
    public Sprite icon;

    [SerializeField]
    public bool cursorReplace;

    public GameObject GetGameObject(Characters character, String name)
    {
        string key = character.ToString() + "_" + name;
        return UIGameManager.Instance.getGameObject(key);
    }

    public void SetToolCursor()
    {
        if (cursorReplace)
        {
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

    public virtual void setAllToFalse(Dictionary<Characters, GameObject> dictionary)
    {
        foreach (var kvp in dictionary)
        {
            kvp.Value.SetActive(false);
        }
    }

    public virtual void DisableAll() { }

    public virtual void InitializeTool(Characters character, string key) { }

    public virtual void UseTool() { }
}
