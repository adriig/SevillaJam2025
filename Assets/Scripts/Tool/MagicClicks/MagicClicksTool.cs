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

        clickedOrbs = 0;

        GameObject gameCanvasObj = GameObject.Find("CanvasGame");
        if (gameCanvasObj != null)
        {
            targetCanvas = gameCanvasObj.GetComponent<Canvas>();
        }

        if (targetCanvas == null)
        {
            Debug.LogWarning("MagicClicksTool: No se encontró Canvas 'Game', intentando buscar por FindObjectsOfType...");
            Canvas[] allCanvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            foreach (Canvas canvas in allCanvases)
            {
                if (canvas.gameObject.name == "Game")
                {
                    targetCanvas = canvas;
                    break;
                }
            }
        }

        if (targetCanvas == null)
        {
            Debug.LogError("MagicClicksTool: No se encontró Canvas 'Game' en la escena.");
            return;
        }

        spawner = targetCanvas.GetComponentInChildren<MagicOrbSpawner>();
        if (spawner == null)
        {
            GameObject spawnerObj = new GameObject("MagicOrbSpawner");
            spawnerObj.transform.SetParent(targetCanvas.transform);
            spawner = spawnerObj.AddComponent<MagicOrbSpawner>();
        }

        Debug.Log($"MagicClicksTool: Inicializado correctamente para {character} en Canvas '{targetCanvas.gameObject.name}' - Objetivo: {orbsToClick} orbes");
    }

    public override void DisableAll()
    {
        base.DisableAll();

        if (spawner != null)
        {
            spawner.ClearAllOrbs();
            Debug.Log("MagicClicksTool: DisableAll - Spawner detenido y orbes limpiados");
        }

        clickedOrbs = 0;
    }

    public override void UseTool()
    {
        base.UseTool();

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
            
            if (spawner != null)
            {
                spawner.StopSpawning();
            }

            GameManager.Instance.Next();
        }
    }
}
