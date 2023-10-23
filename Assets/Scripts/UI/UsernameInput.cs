using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsernameInput : MonoBehaviour
{
    [SerializeField] GameObject dataManagementObject;
    [SerializeField] TMP_InputField input;
    [SerializeField] Button button;

    private string username;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        username = input.text;
        if (username.Length > 10)
        {
            input.text = username.Substring(0, 10);
        }
    }
}
