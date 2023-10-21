using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private bool gameIsActive = false;
    private bool gameAsStarted = false;

    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        // Start game
        if (Input.GetButtonDown("Submit"))
        {
            gameIsActive = true;
            gameAsStarted = true;
        }

        // Pause game
        if (gameAsStarted && Input.GetButtonDown("Cancel"))
        {
            gameIsActive = false;
            // Unlock and show cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Load main menu but don't destroy this game object
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
        }
    }

    public void SetGameIsActive()
    {
        gameIsActive = true;
    }

    private void OnGUI()
    {
        if (!gameIsActive && !gameAsStarted)
        {
            // Display a smooth red clignotant bold large text to say to press enter to start
            GUI.color = new Color(1, 0, 0, Mathf.PingPong(Time.time, 1));  
            GUI.skin.label.fontSize = 50;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(Screen.width / 2 - 238, Screen.height / 2 - 50, 475, 100), "Press Enter to Start");
        }
    }

    public bool GetGameIsActive()
    {
        return gameIsActive;
    }

    public bool GetGameAsStarted()
    {
        return gameAsStarted;
    }
}
