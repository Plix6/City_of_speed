using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInteraction : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private TextLines textLines;
    private int curCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        // Load first dialogue line
        NextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // Switches to next dialogue on key up after press
        if (Input.GetButtonUp("Jump"))
        {
            NextDialogue();
        }
        if (Input.GetButtonUp("Submit"))
        {
            curCount = textLines.lines.Count + 1;
            NextDialogue();
        }
    }

    private void NextDialogue()
    {
        // Next dialogue if it exists
        if (textLines.lines.Count > curCount)
        {
            text.text = textLines.lines[curCount];
            curCount++;
        } 
        // Otherwise return empty string
        else
        {
            text.text = string.Empty;
        }
    }
}
