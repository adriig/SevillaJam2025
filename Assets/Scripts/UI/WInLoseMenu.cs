using UnityEngine;

public class WinLoseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject winMenu;
    [SerializeField]
    private GameObject loseMenu;
    public static WinLoseMenu Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowWinMenu()
    {
        winMenu.SetActive(true);
    }

    public void ShowLoseMenu()
    {
        loseMenu.SetActive(true);
    }
}