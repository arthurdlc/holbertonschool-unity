using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WinMenu winMenu = other.GetComponent<WinMenu>();
            if (winMenu != null)
            {
                winMenu.ShowWinMenu(); // Active le menu de victoire
            }
        }
    }
}
