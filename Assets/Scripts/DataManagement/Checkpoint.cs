using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint
{
    private Vector3 checkpoint = Vector3.zero;
    
    // Sets a checkpoint at given position
    public void SetCheckpoint (Vector3 checkpoint)
    {
        this.checkpoint = checkpoint;
    }

    // Gets the current checkpoint
    public Vector3 GetCheckpoint() 
    { 
        return this.checkpoint; 
    }

    // Checks if a checkpoint is currently set
    public bool IsCheckpointSet()
    {
        return this.checkpoint != Vector3.zero;
    }

    // Resets the current checkpoint
    public void ResetCheckpoint()
    {
        this.checkpoint = Vector3.zero;
    }
}
