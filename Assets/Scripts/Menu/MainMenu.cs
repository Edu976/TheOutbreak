using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
        void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void playGame()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void exitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void returnMenu()
    {
        Debug.Log("Return Menu");
        SceneManager.LoadScene(0);
    }
}
