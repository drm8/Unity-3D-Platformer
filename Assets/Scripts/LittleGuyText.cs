using UnityEngine;

public class LittleGuyText : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		// Rotate towards player
		Vector3 origPos = transform.position;
		Vector3 playerPosition = FindObjectsByType<CharacterController>(FindObjectsSortMode.InstanceID)[0].transform.position;
		transform.LookAt(new Vector3(playerPosition.x, origPos.y, playerPosition.z));
	}
}
