using UnityEngine;

public class KillOnTouch : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			FindObjectsByType<CheckpointManager>(FindObjectsSortMode.InstanceID)[0].RespawnPlayer(other.transform);
        }
    }
}
