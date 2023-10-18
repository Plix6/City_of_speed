using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint
{
    private Vector3 checkpoint = Vector3.zero;
    

    public void SetCheckpoint (Vector3 checkpoint)
    {
        this.checkpoint = checkpoint;
    }

    public Vector3 GetCheckpoint() 
    { 
        return this.checkpoint; 
    }

    public bool IsCheckpointSet()
    {
        return this.checkpoint != Vector3.zero;
    }

    public void ResetCheckpoint()
    {
        this.checkpoint = Vector3.zero;
    }
}
