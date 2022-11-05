using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckpointManager : StaticInstance<CheckpointManager>
{
    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    public Checkpoint currentCheckpoint;
    public void AddCheckpoints()
    {
        foreach (Transform child in transform)
        {
            Checkpoint newCheckpoint = child.GetComponent<Checkpoint>();
            checkpoints.Add(newCheckpoint);
        }
        currentCheckpoint = getNth(0);
    }
    public Checkpoint getNth(int n)
    {
        return checkpoints.ElementAt(n);
    }
    public void changeCurrent(Checkpoint c)
    {
        if (!c.isChecked) return;
        currentCheckpoint = c;
    }
}
