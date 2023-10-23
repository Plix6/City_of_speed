using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TextLines", order = 0)]
public class TextLines : ScriptableObject
{
    // Dialogue lines to be displayed
    public List<string> lines = new List<string>();
}
