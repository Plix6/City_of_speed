using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //---------------------
    //      VARIABLES
    //---------------------

    [SerializeField] private GameObject dataManagementObject;
    private DataManagement dataManagement;
    private bool gameIsActive = false;
    private bool gameAsStarted = false;

    public TMPro.TextMeshProUGUI timerText;

    [Header("Checkpoint")]
    public GameObject player;
    public float checkPointRadius;
    public KeyCode backToCheckpoint = KeyCode.R;

    public GameObject targetsCheckpoint;
    private List<GameObject> targets = new List<GameObject>();

    //--------------------------
    //      START FUNCTION
    //--------------------------
    private void Start()
    {
        dataManagement = dataManagementObject.GetComponent<DataManagement>();
        // When game is finished, call dataManagement.StopTimer(); this will end timer + save into leaderboard

        // For each target in the scene, add it to the list of targets
        foreach (Transform child in targetsCheckpoint.transform)
        {
            targets.Add(child.gameObject);
        }

        // Set the player position to the first checkpoint
        Vector3 playerPos = player.transform.position;
        dataManagement.SetCheckpoint(playerPos);
    }

    //--------------------------
    //      UPDATE FUNCTION
    //--------------------------
    private void Update()
    {
        InputManager();
        if (gameAsStarted && gameIsActive)
        {
            timerText.text = dataManagement.GetTimer();

            // Check if the player is in the checkpoint radius
            Vector3 checkpointPos = CheckpointTouch();
            if (checkpointPos != Vector3.zero)
            {
                // Set the checkpoint to the player position
                dataManagement.SetCheckpoint(checkpointPos);
            }
        }
    }

    //--------------------
    //      FUNCTIONS
    //--------------------
    private void InputManager()
    {
        // Start game
        if (Input.GetButtonDown("Submit"))
        {
            gameIsActive = true;
            gameAsStarted = true;
            if (!dataManagement.IsTimerActive()) 
            {
                dataManagement.ToggleTimer();
            }
        }

        // Pause game
        if (gameAsStarted && gameIsActive && Input.GetButtonDown("Cancel"))
        {
            // Pausing timer
            Time.timeScale = 0;

            gameIsActive = false;
            // Unlock and show cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Load main menu but don't destroy this game object
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
        }
    }

    // Get the checkpoint position
    public Vector3 GetCheckpoint()
    {
        return dataManagement.GetCheckpoint();
    }

    // Respawn the player to the checkpoint
    public void BackToCheckpoint()
    {
        if (gameAsStarted && gameIsActive)
        {
            // Set the player position to the checkpoint
            Vector3 playerNextPos = dataManagement.GetCheckpoint();
            // Add a y offset to the player position
            playerNextPos.y += 1;
            player.transform.position = playerNextPos;
        }
    }

    // Set the game as active
    public void SetGameIsActive(bool boolean)
    {
        gameIsActive = true;

        // Un-pausing timer
        Time.timeScale = 1;
    }

    // Set the game as started
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

    // Check if game is active
    public bool GetGameIsActive()
    {
        return gameIsActive;
    }

    // Check if game is started
    public bool GetGameAsStarted()
    {
        return gameAsStarted;
    }

    // Check if the player is in the checkpoint radius
    private Vector3 CheckpointTouch()
    {
        foreach (GameObject target in targets)
        {
            if (Vector3.Distance(player.transform.position, target.transform.position) < checkPointRadius)
            {
                Vector3 pos = target.transform.position;
                pos.y += 3;
                return pos;
            }
        }
        return Vector3.zero;
    }
}
