using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Collectable : MonoBehaviour
{
	public ScoreUI score;

	public float floatAmplitude;
	public float wavelength;

	public float rotXMult;
	public float rotYMult;
	public float rotZMult;

	private float defaultY;
	private float floatDelta = 0;

	private void Start()
	{
		score = FindObjectsByType<ScoreUI>(FindObjectsSortMode.InstanceID)[0];
		defaultY = gameObject.transform.position.y;
		wavelength = wavelength * 2 * Mathf.PI;
	}

	// Update is called once per frame
	void Update()
    {
		// Rotate
        gameObject.transform.Rotate(new Vector3(rotXMult * Time.deltaTime, rotYMult * Time.deltaTime, rotZMult * Time.deltaTime));

		// Float
        floatDelta += Time.deltaTime;
		Vector3 pos = gameObject.transform.position;
		gameObject.transform.position = new Vector3(pos.x, defaultY+Mathf.Sin(floatDelta * wavelength) * floatAmplitude, pos.z);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			score.IncreaseScore();
			Object.Destroy(gameObject);
		}
	}
}
