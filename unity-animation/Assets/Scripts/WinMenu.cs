using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject winCanvas;
    public Button nextButton;
    public Button menuButton;
    public TextMeshProUGUI finalTimeText;
    private Timer timerScript;

    void Start()
    {
        timerScript = FindObjectOfType<Timer>();
        winCanvas.SetActive(false);

        nextButton.onClick.AddListener(NextLevel);
        menuButton.onClick.AddListener(MainMenu);
    }
    public void ShowWinMenu()
    {
        Time.timeScale = 0;
        winCanvas.SetActive(true);
        timerScript.ArreterTimer();

        // Met à jour l'affichage du temps final
        if (finalTimeText != null)
        {
            finalTimeText.text = timerScript.timerText.text;
        }
    }
    public void NextLevel()
    {
        Time.timeScale = 1;
        string thisSceneIndex = SceneManager.GetActiveScene().name;

        if (thisSceneIndex.StartsWith("Level"))
        {
            int levelNumber = int.Parse(thisSceneIndex.Substring(5)); // Extrait "01", "02", etc.
            string nextLevel = "Level" + (levelNumber + 1).ToString("00");

            if (Application.CanStreamedLevelBeLoaded(nextLevel))
            {
                SceneManager.LoadScene(nextLevel);
            }
            else
            {
                SceneManager.LoadScene("MainMenu"); // Si pas de niveau suivant, retourne au menu
            }
        }
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
