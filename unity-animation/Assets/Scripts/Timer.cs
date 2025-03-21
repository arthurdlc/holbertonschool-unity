using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float tempsEcoule = 0f;
    private bool estEnCours = true;

    void Update()
    {
        if (estEnCours)
        {
            tempsEcoule += Time.unscaledDeltaTime;
            MettreAJourAffichageTimer();
        }
    }

    void MettreAJourAffichageTimer()
    {
        int minutes = Mathf.FloorToInt(tempsEcoule / 60);
        int secondes = Mathf.FloorToInt(tempsEcoule % 60);
        int millisecondes = Mathf.FloorToInt((tempsEcoule * 100) % 100);
        timerText.text = string.Format("{0}:{1:00}.{2:00}", minutes, secondes, millisecondes);
    }

    public void ArreterTimer()
    {
        estEnCours = false;
    }

    public void DemarrerTimer()
    {
        estEnCours = true;
    }
}