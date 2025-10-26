using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

[CreateAssetMenu(fileName = "New DragAndLeaveTool", menuName = "Game/Tool/DragAndLeaveTool")]
public class DragAndLeaveTool : Tool
{
    private GameObject draggableElement;
    private GameObject dropArea;
    private GameObject container;

    public override void InitializeTool(Characters character, string key)
    {
        base.InitializeTool(character, key);
        draggableElement = GetGameObject(character, key + "Draggable");
        dropArea = GetGameObject(character, key + "DropArea");
        container = GetGameObject(character, key + "Container");
    }

    public override void DisableAll()
    {
        base.DisableAll();

        draggableElement.SetActive(false);
        dropArea.SetActive(false);
        container.SetActive(false);
    }

    public override void UseTool()
    {
        base.UseTool();
        DisableAll();

        draggableElement.SetActive(true);
        dropArea.SetActive(true);
        container.SetActive(true);
    }
}
