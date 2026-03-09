using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
//using static UnityEditor.PlayerSettings;

public class Collectable : MonoBehaviour
{
	public ScoreUI score;

	public float floatAmplitude;
	public float wavelength;

	public float rotXMult;
	public float rotYMult;
	public float rotZMult;

	private float defaultY;
	private float floatDelta;

	private static int currentSound = 0;
	public float soundCooldown = 2;
	private static float soundTimer = 0;
	private static bool cooledDown = false;
	public AudioClip[] collectSounds;
	public float collectVolume = 1.5f;

	private void Start()
	{
		score = FindObjectsByType<ScoreUI>(FindObjectsSortMode.InstanceID)[0];
		defaultY = gameObject.transform.position.y;
		wavelength = wavelength * 2 * Mathf.PI;
		floatDelta = Random.value * wavelength;
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

		if (!cooledDown && soundTimer > 0)
		{
			cooledDown = true;
			soundTimer -= Time.deltaTime;
			if (soundTimer <= 0) currentSound = 0;
		}
	}

	void LateUpdate()
	{
		cooledDown = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			soundTimer = soundCooldown;
			AudioSource.PlayClipAtPoint(collectSounds[currentSound], transform.position, collectVolume);
			if (currentSound < 4) currentSound++;

			score.IncreaseScore();
			Object.Destroy(gameObject);
		}
	}
}
