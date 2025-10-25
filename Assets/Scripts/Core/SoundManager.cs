using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        // Si ya existe una instancia y no somos nosotros, nos destruimos
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asignamos la instancia y la mantenemos al cambiar de escena
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
