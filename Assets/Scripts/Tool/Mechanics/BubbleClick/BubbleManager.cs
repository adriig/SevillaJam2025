using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour, ToolMechanic
{
    [Header("Bubble Settings")]
    public GameObject bubblePrefab;
    private List<GameObject> activeBubbles = new List<GameObject>();

    public void GenerateBubble(Vector2 position)
    {
        GameObject newBubble = Instantiate(bubblePrefab, position, Quaternion.identity);
        activeBubbles.Add(newBubble);
    }

    public void OnFinish()
    {
        foreach (var bubble in activeBubbles)
        {
            if (bubble != null)
            {
                Destroy(bubble);
            }
        }
        activeBubbles.Clear();
    }

    public void OnSpriteChange() { }

    public void RemoveBubble(GameObject bubble)
    {
        if (activeBubbles.Contains(bubble))
        {
            activeBubbles.Remove(bubble);
            Destroy(bubble);
        }
    }
}
