using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GlobalManager : MonoBehaviour
    {
        public static GlobalManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        public void Start()
        {
            LoadGameScene();
        }
        private void LoadGameScene()
        {
            SceneManager.LoadScene("Game");
        }
    }
}