using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        // Lock and hide cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Get the game controller
        if(GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if(gameController != null)
        {
            // Change play button text tmp
            GameObject.Find("TextPlayBtn").GetComponent<TMPro.TextMeshProUGUI>().text = "Resume";
        }
    }

    public void PlayGame()
    {
        if(gameController != null && gameController.GetGameAsStarted())
        {
            // Unload the main menu
            SceneManager.UnloadSceneAsync(0);

            // Lock and hide cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Activate game
            gameController.SetGameIsActive(true);
        }
        else
        {
            // Loads the next scene in the build index
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }

    public void QuitGame()
    {
        // Quits the game
        Application.Quit();
    }
}
