using StarterAssets;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Conveyor : MonoBehaviour
{
	public ConveyorMover mover;

	public float moveSpeed = 2;
	
	public float extraSize = 0.45f;

	public Material material;
	

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Vector3 scale = transform.localScale;

		Vector3 newSize = new Vector3((scale.x + extraSize) / scale.x, 5, (scale.z + extraSize) / scale.z);
		GetComponent<BoxCollider>().size = newSize;

		material.mainTextureScale = new Vector2(scale.x, scale.z);
	}

    // Update is called once per frame
    void Update()
    {
		Vector2 newOffset = new Vector2(0, (material.mainTextureOffset.y + Time.deltaTime * moveSpeed) % 1);
		material.mainTextureOffset = newOffset;

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			mover.StartMoving(other.transform.parent.transform);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			mover.StopMoving();
		}
	}
}
