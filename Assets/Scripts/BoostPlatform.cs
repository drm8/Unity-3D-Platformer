using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BoostPlatform : MonoBehaviour
{
	public float cooldownLength;
    private float cooldown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		cooldown = 0f;
	}

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0f)
		{
			cooldown -= Time.deltaTime;
		}
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			cooldown = cooldownLength;

		}
	}
}
