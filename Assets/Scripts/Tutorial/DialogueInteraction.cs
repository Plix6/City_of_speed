using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInteraction : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private TextLines textLines;
    private int curCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        NextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("Submit"))
        {
            NextDialogue();
        }
    }

    private void NextDialogue()
    {
        if (textLines.lines.Count > curCount)
        {
            text.text = textLines.lines[curCount];
            curCount++;
        } 
        else
        {
            text.text = string.Empty;
        }
    }
}
