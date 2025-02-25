using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject canvasVictoire; // Ajoute une référence au canvas de victoire

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timer playerTimer = other.GetComponent<Timer>();
            if (playerTimer != null)
            {
                playerTimer.ArreterTimer(); // Arrête le timer
                canvasVictoire.SetActive(true); // Affiche le canvas de victoire
                Time.timeScale = 0; // Met le jeu en pause (optionnel)
            }
        }
    }
}