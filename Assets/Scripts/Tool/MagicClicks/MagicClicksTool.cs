using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MagicClicksTool", menuName = "Game/Tool/MagicClicksTool")]
public class MagicClicksTool : Tool
{
    [SerializeField]
    private GameObject orbPrefab;

    [SerializeField]
    private float spawnInterval = 2f;

    [SerializeField]
    private int maxSimultaneousOrbs = 5;

    [Header("Objetivo")]
    [SerializeField]
    private int orbsToClick = 5;

    private Canvas targetCanvas;
    private MagicOrbSpawner spawner;
    private int clickedOrbs = 0;

    public override void InitializeTool(Characters character, string key)
    {
        base.InitializeTool(character, key + "MagicClicks");

        // Resetear contador
        clickedOrbs = 0;

        targetCanvas = FindFirstObjectByType<Canvas>();
        if (targetCanvas == null)
        {
            Debug.LogError("MagicClicksTool: No se encontró Canvas en la escena.");
            return;
        }

        spawner = targetCanvas.GetComponentInChildren<MagicOrbSpawner>();
        if (spawner == null)
        {
            GameObject spawnerObj = new GameObject("MagicOrbSpawner");
            spawnerObj.transform.SetParent(targetCanvas.transform);
            spawner = spawnerObj.AddComponent<MagicOrbSpawner>();
        }

        Debug.Log($"MagicClicksTool: Inicializado correctamente para {character} - Objetivo: {orbsToClick} orbes");
    }

    public override void DisableAll()
    {
        base.DisableAll();

        if (spawner != null)
        {
            spawner.ClearAllOrbs();
            Debug.Log("MagicClicksTool: DisableAll - Spawner detenido y orbes limpiados");
        }

        // Resetear contador al deshabilitar
        clickedOrbs = 0;
    }

    public override void UseTool()
    {
        base.UseTool();

        // Resetear contador al usar la herramienta
        clickedOrbs = 0;

        if (orbPrefab == null)
        {
            Debug.LogError("MagicClicksTool: No se asignó el prefab Orb.");
            return;
        }

        if (spawner == null)
        {
            Debug.LogError("MagicClicksTool: Spawner no inicializado.");
            return;
        }

        spawner.Initialize(orbPrefab, targetCanvas, spawnInterval, maxSimultaneousOrbs, this);
        spawner.StartSpawning();
        Debug.Log(
            $"MagicClicksTool: Spawn iniciado - Intervalo: {spawnInterval}s, Máx: {maxSimultaneousOrbs}, Objetivo: {orbsToClick}"
        );
    }

    public void OnOrbClicked()
    {
        clickedOrbs++;
        Debug.Log($"MagicClicksTool: Orbe clickeado! Progreso: {clickedOrbs}/{orbsToClick}");

        if (clickedOrbs >= orbsToClick)
        {
            Debug.Log("MagicClicksTool: ¡Objetivo alcanzado! Llamando a GameManager.Next()");
            
            // Detener el spawner
            if (spawner != null)
            {
                spawner.StopSpawning();
            }

            // Llamar a GameManager
            GameManager.Instance.Next();
        }
    }
}
