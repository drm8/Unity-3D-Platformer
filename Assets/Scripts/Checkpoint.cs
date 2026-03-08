using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform playerTransform;
    private CheckpointManager checkpointManager;
    private bool active = false;
    private Quaternion initialRotation;

    public ParticleSystem particleSystem;
	public AudioClip activateSound;
	public float activateVolume = 1.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialRotation = transform.rotation;
        playerTransform = FindObjectsByType<CharacterController>(FindObjectsSortMode.InstanceID)[0].transform;
        checkpointManager = GetComponentInParent<CheckpointManager>();
        if (checkpointManager.currentCheckpoint == transform) active = true;
    }

    public Quaternion getRotation()
    {
        return initialRotation;
    }

    public void Deactivate()
    {
        active = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
			if (checkpointManager.SetCheckpoint(transform))
            {
                AudioSource.PlayClipAtPoint(activateSound, transform.position, activateVolume);
		        particleSystem.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate towards player
		transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z));
    }
}
