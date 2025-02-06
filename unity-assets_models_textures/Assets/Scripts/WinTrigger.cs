using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si le joueur touche l'objet
        {
            Timer playerTimer = other.GetComponent<Timer>(); // Récupère le Timer du Player
            if (playerTimer != null)
            {
                playerTimer.StopTimer(); // Arrête le timer et change l'affichage
            }
        }
    }
}