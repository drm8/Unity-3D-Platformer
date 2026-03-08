using StarterAssets;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform currentCheckpoint;

    public void RespawnPlayer(Transform player)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.position += (currentCheckpoint.position - player.position);
        player.GetComponent<CharacterController>().enabled = true;
        player.rotation = currentCheckpoint.GetComponent<Checkpoint>().getRotation();
    }

    public bool SetCheckpoint(Transform newCheckpoint)
    {
        if (currentCheckpoint == newCheckpoint) return false;

        currentCheckpoint.GetComponent<Checkpoint>().Deactivate();
        currentCheckpoint = newCheckpoint;
        return true;
    }
}