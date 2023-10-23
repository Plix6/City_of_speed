using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFinishedText : MonoBehaviour
{
    [SerializeField] private GameObject dataManagementObject;
    private DataManagement dataManagement;
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        this.text = this.GetComponent<TMP_Text>();
        this.dataManagement = dataManagementObject.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.dataManagement.IsTimerActive())
        {
            text.text = string.Format("Game finished with a time of {0} !\nEnter a 3-letter username to save your score !", dataManagement.GetTimer());
        }
    }
}
