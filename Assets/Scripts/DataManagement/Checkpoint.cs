using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 checkpoint;
    // Start is called before the first frame update
    void Start()
    {
        ResetCheckpoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCheckpoint (Vector3 checkpoint)
    {
        this.checkpoint = checkpoint;
    }

    public Vector3 GetCheckpoint() 
    { 
        return this.checkpoint; 
    }

    public bool IsCheckpoint()
    {
        return this.checkpoint != Vector3.zero;
    }

    public void ResetCheckpoint()
    {
        this.checkpoint = Vector3.zero;
    }
}
