using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsernameButton : MonoBehaviour
{

    [SerializeField] private GameObject dataManagementObject;
    [SerializeField] private TMP_InputField input;
    private DataManagement dataManagement;

    // Start is called before the first frame update
    void Start()
    {
        this.dataManagement = dataManagementObject.GetComponent<DataManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {
        if (dataManagement.SetUsername(input.text))
        {
            StartCoroutine(SaveAndQuit());
        }
    }

    private IEnumerator SaveAndQuit()
    {
        yield return new WaitForSeconds(0.3f); 
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(0);
    }
}
