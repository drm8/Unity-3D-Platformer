using JetBrains.Annotations;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    public Conveyor conveyor;

    private bool isMoving = false;
    private Vector3 moveDirection = Vector3.forward;
    private Transform child;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveDirection = conveyor.transform.forward * conveyor.moveSpeed;
	}

    public void StartMoving(Transform newChild)
    {
        isMoving = true;
        GetComponent<Transform>().position = Vector3.zero;
		newChild.SetParent(transform);
		child = newChild;
	}

    public void StopMoving()
    {
		isMoving = false;
		child.SetParent(null);
		child = null;
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if (isMoving)
        {
			GetComponent<Transform>().position += moveDirection * Time.deltaTime;
		}
    }
}
