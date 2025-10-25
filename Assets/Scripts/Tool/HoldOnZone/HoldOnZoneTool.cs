using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New HoldOnZoneTool", menuName = "Game/Tool/HoldOnZoneTool")]
public class HoldOnZoneTool : Tool
{
    private Dictionary<(Characters character, int position), GameObject> characterGameObjects;

    public override void InitializeTool()
    {
        base.InitializeTool();
        InitializeCharacterDictionary();
    }

    private void InitializeCharacterDictionary()
    {
        characterGameObjects = new Dictionary<(Characters character, int position), GameObject>();

        var uiManager = UIGameManager.Instance;
        characterGameObjects.Add((Characters.Skeleton, 0), uiManager.skeletonHoldOnZone);
        characterGameObjects.Add((Characters.Skeleton, 1), uiManager.skeletonHoldOnBone1);
        characterGameObjects.Add((Characters.Skeleton, 2), uiManager.skeletonHoldOnBone2);
        characterGameObjects.Add((Characters.Skeleton, 3), uiManager.skeletonHoldOnBone3);
    }

    public override void DisableAll()
    {
        base.DisableAll();
        foreach (var gameObject in characterGameObjects.Values)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public override void UseTool(Characters character)
    {
        base.UseTool(character);

        var key = (character, position);
        if (characterGameObjects.ContainsKey(key))
        {
            characterGameObjects[key].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"No se encontró HoldZone para {character} en posición {position}");
        }
    }
}