using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HoldOnZoneTool", menuName = "Game/Tool/HoldOnZoneTool")]
public class HoldOnZoneTool : Tool
{
    private GameObject holdOnZone;

    public override void InitializeTool(Characters character, string key)
    {
        base.InitializeTool(character, key + "HoldOnZone");
        holdOnZone = GetGameObject(character, key + "HoldOnZone");
        if (!holdOnZone)
        {
            Debug.LogError("HoldOnZone GameObject not found for key: " + key);
            return;
        }
        HoldZoneController holdZoneController = holdOnZone.GetComponent<HoldZoneController>();
        holdZoneController.Reset();
    }

    public override void DisableAll()
    {
        base.DisableAll();
        holdOnZone.SetActive(false);
    }

    public override void UseTool()
    {
        base.UseTool();

        holdOnZone.SetActive(true);
    }
}
