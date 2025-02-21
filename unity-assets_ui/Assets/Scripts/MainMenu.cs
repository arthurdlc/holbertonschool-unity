using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LevelSelect(int level)
    {
        string sceneName = "Level0" + level; // Assumes scenes are named "Level01", "Level02", etc.
        SceneManager.LoadScene(sceneName);
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitButton()
    {
        Debug.Log("Exited");
        Application.Quit(); // Closes the game (Only works in a built version)
    }
}
