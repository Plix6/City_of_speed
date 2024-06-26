using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    //---------------------
    //      VARIABLES
    //---------------------

    [SerializeField] private GameObject dataManagementObject;
    [SerializeField] private GameObject usernameInputCanvas;
    [SerializeField] private GameObject playerUICanvas;
    private DataManagement dataManagement;
    private bool gameIsActive = false;
    private bool gameAsStarted = false;
    private int checkpointCount;

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
        usernameInputCanvas.SetActive(false);
        dataManagement = dataManagementObject.GetComponent<DataManagement>();

        // For each target in the scene, add it to the list of targets
        foreach (Transform child in targetsCheckpoint.transform)
        {
            targets.Add(child.gameObject);
        }
        checkpointCount = targets.Count;

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

        if (dataManagement.GetPreviousCheckpointsNumber() == checkpointCount + 1)
        {
            gameIsActive = false;
            dataManagement.StopTimer();
            usernameInputCanvas.SetActive(true);
            playerUICanvas.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
            dataManagement.ToggleTimer();

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
        dataManagement.ToggleTimer();
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
            GUI.Label(new Rect(Screen.width / 2 - 238, Screen.height / 2 + 50, 475, 100), "Press Enter to Start");
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
