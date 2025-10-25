using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance;

    [Header("UI")]
    public GameObject optionsMenu;

    private bool isOpen = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (optionsMenu != null)
            optionsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptions();
        }
    }

    public void ToggleOptions()
    {
        if (isOpen)
            CloseOptions();
        else
            OpenOptions();
    }

    public void OpenOptions()
    {
        isOpen = true;
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(true);
            // Opcional: pausar el juego si no estás en el menú principal
            // Time.timeScale = 0f;
        }
    }

    public void CloseOptions()
    {
        isOpen = false;
        if (optionsMenu != null)
        {
            optionsMenu.SetActive(false);
            // Opcional: reanudar el juego
            // Time.timeScale = 1f;
        }
    }
}