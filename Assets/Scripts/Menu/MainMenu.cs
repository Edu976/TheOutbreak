using UnityEngine;
using UnityEngine.SceneManagement;

// Esta clase controla el menú principal a la hora de inicar el juego
public class MainMenu : MonoBehaviour
{
        void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Método para inciar la partida
    public void playGame()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Método para cerrar el juego
    public void exitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    // Método para volver al menú desde el juego
    public void returnMenu()
    {
        Debug.Log("Return Menu");
        SceneManager.LoadScene(0);
    }
}
