using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrbSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject orbPrefab;

    [SerializeField]
    private float spawnInterval = 2f;

    [SerializeField]
    private int maxSimultaneousOrbs = 5;

    private Canvas targetCanvas;
    private List<GameObject> spawnedOrbs = new List<GameObject>();
    private Coroutine spawnCoroutine;
    private bool isSpawning = false;
    private MagicClicksTool parentTool;

    public void Initialize(
        GameObject prefab,
        Canvas canvas,
        float interval = 2f,
        int maxOrbs = 5,
        MagicClicksTool tool = null
    )
    {
        orbPrefab = prefab;
        targetCanvas = canvas;
        spawnInterval = interval;
        maxSimultaneousOrbs = maxOrbs;
        parentTool = tool;
        Debug.Log($"MagicOrbSpawner: Inicializado - Intervalo: {interval}s, Máx: {maxOrbs}");
    }

    public void StartSpawning()
    {
        Debug.Log($"MagicOrbSpawner: StartSpawning llamado - isSpawning: {isSpawning}");

        if (isSpawning)
        {
            Debug.LogWarning("MagicOrbSpawner: Ya está spawneando");
            return;
        }

        if (orbPrefab == null || targetCanvas == null)
        {
            Debug.LogError(
                $"MagicOrbSpawner: Prefab o Canvas no configurado - Prefab: {(orbPrefab != null ? "OK" : "NULL")}, Canvas: {(targetCanvas != null ? "OK" : "NULL")}"
            );
            return;
        }

        isSpawning = true;
        spawnCoroutine = StartCoroutine(SpawnOrbsRoutine());
        Debug.Log($"MagicOrbSpawner: Coroutine iniciada - Intervalo: {spawnInterval}s");
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        isSpawning = false;
        Debug.Log("MagicOrbSpawner: Detenido");
    }

    public void ClearAllOrbs()
    {
        StopSpawning();

        // Limpiar referencias null primero
        int nullCount = spawnedOrbs.RemoveAll(orb => orb == null);
        if (nullCount > 0)
        {
            Debug.Log($"MagicOrbSpawner: Limpiadas {nullCount} referencias null");
        }

        foreach (GameObject orb in spawnedOrbs)
        {
            if (orb != null)
            {
                Destroy(orb);
            }
        }

        Debug.Log($"MagicOrbSpawner: {spawnedOrbs.Count} orbes destruidos");
        spawnedOrbs.Clear();
    }

    private IEnumerator SpawnOrbsRoutine()
    {
        Debug.Log("MagicOrbSpawner: Coroutine SpawnOrbsRoutine iniciada");
        int iteracion = 0;

        while (isSpawning)
        {
            iteracion++;
            Debug.Log(
                $"MagicOrbSpawner: Iteración {iteracion} - Orbes actuales: {spawnedOrbs.Count}/{maxSimultaneousOrbs}"
            );

            // Limpiar referencias null de orbes autodestruidos
            spawnedOrbs.RemoveAll(orb => orb == null);

            // Solo spawnear si no hemos alcanzado el máximo
            if (spawnedOrbs.Count < maxSimultaneousOrbs)
            {
                Debug.Log("MagicOrbSpawner: Llamando a SpawnOrb...");
                SpawnOrb();
            }
            else
            {
                Debug.Log($"MagicOrbSpawner: Máximo de orbes alcanzado ({maxSimultaneousOrbs})");
            }

            Debug.Log($"MagicOrbSpawner: Esperando {spawnInterval} segundos...");
            yield return new WaitForSeconds(spawnInterval);
        }

        Debug.Log("MagicOrbSpawner: Coroutine terminada");
    }

    private void SpawnOrb()
    {
        Debug.Log(
            $"MagicOrbSpawner: SpawnOrb llamado - Prefab: {(orbPrefab != null ? "OK" : "NULL")}, Canvas: {(targetCanvas != null ? "OK" : "NULL")}"
        );

        if (orbPrefab == null || targetCanvas == null)
        {
            Debug.LogError("MagicOrbSpawner: No se puede spawnear - Prefab o Canvas es null");
            return;
        }

        Debug.Log("MagicOrbSpawner: Instanciando prefab...");
        GameObject orbInstance = Instantiate(orbPrefab, targetCanvas.transform, false);
        Debug.Log($"MagicOrbSpawner: Orbe instanciado: {orbInstance.name}");

        RectTransform canvasRect = targetCanvas.GetComponent<RectTransform>();
        RectTransform orbRect = orbInstance.GetComponent<RectTransform>();

        if (orbRect == null)
        {
            Debug.LogError("MagicOrbSpawner: El prefab debe tener RectTransform");
            Destroy(orbInstance);
            return;
        }

        // Asegurar que el orbe tiene una escala válida ANTES de que Start() se ejecute
        if (orbRect.localScale == Vector3.zero || orbRect.localScale.x < 0.01f)
        {
            orbRect.localScale = Vector3.one;
            Debug.Log("MagicOrbSpawner: Escala del orbe corregida a (1,1,1)");
        }

        // Posición aleatoria en el canvas
        float halfWidth = canvasRect.rect.width / 2f;
        float halfHeight = canvasRect.rect.height / 2f;
        float x = Random.Range(-halfWidth, halfWidth);
        float y = Random.Range(-halfHeight, halfHeight);

        orbRect.anchoredPosition = new Vector2(x, y);
        Debug.Log(
            $"MagicOrbSpawner: Posición configurada en ({x:F1}, {y:F1}) - Canvas: {canvasRect.rect.width}x{canvasRect.rect.height}"
        );

        // Asegurar que tiene el controller
        MagicOrbController controller = orbInstance.GetComponent<MagicOrbController>();
        if (controller == null)
        {
            controller = orbInstance.AddComponent<MagicOrbController>();
            Debug.Log("MagicOrbSpawner: MagicOrbController añadido");
        }
        else
        {
            controller.Reset();
            Debug.Log("MagicOrbSpawner: MagicOrbController reseteado");
        }

        // Configurar la referencia al tool para el callback
        controller.SetParentTool(parentTool);

        spawnedOrbs.Add(orbInstance);
        Debug.Log(
            $"MagicOrbSpawner: Orbe spawneado exitosamente. Total activos: {spawnedOrbs.Count}"
        );
    }

    private void OnDestroy()
    {
        ClearAllOrbs();
    }
}
