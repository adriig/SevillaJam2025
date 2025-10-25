using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DragAndLeaveTool", menuName = "Game/Tool/DragAndLeaveTool")]


public class DragAndLeaveTool : Tool
{
    private Dictionary<(Characters character, int position), GameObject> draggableElements;
    private Dictionary<(Characters character, int position), GameObject> dropAreas;
    private Dictionary<(Characters character, int position), GameObject> containers;

    public override void InitializeTool()
    {
        base.InitializeTool();
        initializeDraggableElementsDictionary();
        initializeDropAreasDictionary();
        initializeContainersDictionary();
    }

    private void initializeDraggableElementsDictionary()
    {
        draggableElements = new Dictionary<(Characters character, int position), GameObject>();

        var uiManager = UIGameManager.Instance;
        draggableElements.Add((Characters.Skeleton, 0), uiManager.skeletonDraggableBone);
        // Puedes añadir más elementos así:
        // draggableElements.Add((Characters.Skeleton, 1), uiManager.otroElemento);
    }

    public void initializeDropAreasDictionary()
    {
        dropAreas = new Dictionary<(Characters character, int position), GameObject>();

        var uiManager = UIGameManager.Instance;
        dropAreas.Add((Characters.Skeleton, 0), uiManager.skeletonBoneDropArea);
        // Puedes añadir más áreas así:
        // dropAreas.Add((Characters.Skeleton, 1), uiManager.otraArea);
    }

    private void initializeContainersDictionary()
    {
        containers = new Dictionary<(Characters character, int position), GameObject>();

        var uiManager = UIGameManager.Instance;
        containers.Add((Characters.Skeleton, 0), uiManager.skeletonBoneContainer);
        // Puedes añadir más contenedores así:
        // containers.Add((Characters.Skeleton, 1), uiManager.otroContenedor);
    }

    public override void DisableAll()
    {
        base.DisableAll();
        
        foreach (var gameObject in draggableElements.Values)
        {
            if (gameObject != null) gameObject.SetActive(false);
        }
        foreach (var gameObject in dropAreas.Values)
        {
            if (gameObject != null) gameObject.SetActive(false);
        }
        foreach (var gameObject in containers.Values)
        {
            if (gameObject != null) gameObject.SetActive(false);
        }
    }

    public override void UseTool(Characters character)
    {
        DisableAll();
        base.UseTool(character);

        var key = (character, position);
        
        if (draggableElements.TryGetValue(key, out GameObject draggableElement))
        {
            draggableElement.SetActive(true);
        }
        if (dropAreas.TryGetValue(key, out GameObject dropArea))
        {
            dropArea.SetActive(true);
        }
        if (containers.TryGetValue(key, out GameObject container))
        {
            container.SetActive(true);
        }
    }
}