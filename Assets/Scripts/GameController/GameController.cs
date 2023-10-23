using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject dataManagementObject;
    private DataManagement dataManagement;
    private bool gameIsActive = false;
    private bool gameAsStarted = false;

    public TMPro.TextMeshProUGUI timerText;

    [Header("Checkpoint")]
    public GameObject player;
    public float checkPointCooldown;
    private float checkPointCooldownTimer;
    public float checkPointRadius;
    public KeyCode backToCheckpoint = KeyCode.R;

    public GameObject targetsCheckpoint;
    private List<GameObject> targets = new List<GameObject>();

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

    private void Update()
    {
        InputManager();
        if (gameAsStarted && gameIsActive)
        {
            timerText.text = dataManagement.GetTimer();

            // Check if the player is in the checkpoint radius
            Vector3 checkpointPos = checkpointTouch();
            if (checkpointPos != Vector3.zero)
            {
                Debug.Log("Checkpoint touched");
                // Set the checkpoint to the player position
                dataManagement.SetCheckpoint(checkpointPos);
            }
        }
    }

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

        

        updateCheckpointCooldown();
    }

    public bool canBackToCheckpoint()
    {
        return checkPointCooldownTimer == 0;
    }

    public Vector3 GetCheckpoint()
    {
        return dataManagement.GetCheckpoint();
    }
    public void BackToCheckpoint()
    {
        if (gameAsStarted && gameIsActive && checkPointCooldownTimer == 0)
        {
            // Set the player position to the checkpoint
            Vector3 playerNextPos = dataManagement.GetCheckpoint();
            Debug.Log(playerNextPos);
            // Add a y offset to the player position
            playerNextPos.y += 1;
            player.transform.position = playerNextPos;
            checkPointCooldownTimer = checkPointCooldown;
        }
    }

    public void SetGameIsActive(bool boolean)
    {
        gameIsActive = true;

        // Un-pausing timer
        Time.timeScale = 1;
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

    // Check if the player is in the checkpoint radius
    private Vector3 checkpointTouch()
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

    private void updateCheckpointCooldown()
    {
        checkPointCooldownTimer -= Time.deltaTime;
        if (checkPointCooldownTimer < 0)
        {
            checkPointCooldownTimer = 0;
        }
    }
}
