using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public Button restartButton;
    public Button resumeButton;
    public Button menuButton;
    private Timer timerScript;

    void Start()
    {
        timerScript = FindObjectOfType<Timer>();
        pauseCanvas.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        resumeButton.onClick.AddListener(ResumeGame);
        menuButton.onClick.AddListener(MainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        timerScript.ArreterTimer();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        timerScript.DemarrerTimer();
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }
}