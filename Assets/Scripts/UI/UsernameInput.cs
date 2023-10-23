using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsernameInput : MonoBehaviour
{
    private TMP_InputField inputField;
    private string username;

    // Start is called before the first frame update
    void Start()
    {
        this.inputField = GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        username = inputField.text;
        if (username.Length > 3)
        {
            inputField.text = username.Substring(0, 3);
        }
    }
}
