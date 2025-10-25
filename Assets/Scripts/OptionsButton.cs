using UnityEngine;
using UnityEngine.UI;

public class OptionsButton : MonoBehaviour
{
    void Start()
    {
        Button closeButton = GetComponent<Button>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseOptions);
        }
    }

    void CloseOptions()
    {
        if (OptionsManager.Instance != null)
            OptionsManager.Instance.CloseOptions();
    }
}