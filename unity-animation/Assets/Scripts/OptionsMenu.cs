using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Toggle invertYToggle;

    // Start is called before the first frame update
    void Start()
    {
        // Charger la valeur de l'inversion depuis PlayerPrefs, si elle existe
        if (PlayerPrefs.HasKey("isInverted"))
        {
            invertYToggle.isOn = PlayerPrefs.GetInt("isInverted") == 1;
        }
        else
        {
            // Par défaut, on peut définir à false
            invertYToggle.isOn = false;
        }
    }

    public void Back()
    {
        // Charger la scène précédente
        SceneManager.LoadScene("MainMenu");
    }

    public void Apply()
    {
        // Sauvegarder la préférence d'inversion dans PlayerPrefs
        PlayerPrefs.SetInt("isInverted", invertYToggle.isOn ? 1 : 0);

        // Appliquer la scène précédente
        Back();
    }
}
