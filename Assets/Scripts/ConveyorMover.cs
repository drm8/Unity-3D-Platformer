using JetBrains.Annotations;
using StarterAssets;
using System;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    public Conveyor conveyor;

    private static ConveyorMover activeMover = null;

    private bool isMoving = false;
    private Vector3 moveDirection = Vector3.forward;
    private Transform child;

    public float groundFriction = 7;
	public float airFriction = 2;
	private float residualSpeed = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveDirection = conveyor.transform.forward * conveyor.moveSpeed;
	}

    public void StartMoving(Transform newChild)
    {
        if (activeMover != null) activeMover.CancelMoving();
        activeMover = this;

        isMoving = true;
        GetComponent<Transform>().position = Vector3.zero;
		newChild.SetParent(transform);
		child = newChild;
	}

    public void StopMoving()
    {
		isMoving = false;
        residualSpeed = 1.0f;
	}

    public void CancelMoving()
    {
        activeMover = null;
		isMoving = false;
        residualSpeed = 0.0f;
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
        else if (residualSpeed > 0.0f)
        {
			GetComponent<Transform>().position += moveDirection * residualSpeed * Time.deltaTime;

            if (child.GetComponentInChildren<ThirdPersonController>().Grounded)
            {
				residualSpeed /= 1 + groundFriction * Time.deltaTime;
			}
            else
            {
				residualSpeed /= 1 + airFriction * Time.deltaTime;
			}

            if (residualSpeed <= 0.05f)
            {
				CancelMoving();
			}
		}
    }
}
