using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class LittleGuyText : MonoBehaviour
{
	public string nextScene = "Level2";
	public float nextSceneDelay = 3;
	private float nextSceneTimer;

	private float initialScale;
	private float burstDelta = 1;
	public TextMeshProUGUI dialog;
	public LittleGuyButtonPrompt buttonPrompt;
	private ThirdPersonController playerScript;
	private Transform playerTransform;
	private Transform parentTransform;
	public LittleGuy littleGuy;

	private StarterAssetsInputs input;

	private ScoreUI scoreUI;
	public int scoreRequired = 5;
	private float talkDelta;
	public float talkRate;
	public string[] greetings = { "hi!", "hi!", "hey!", "hello!" };
	private bool hasBeenTalkedTo = false;
	private bool hasBeenTalkedToRecently = false;

	public ParticleSystem particleSystem;
	public AudioClip crunchSound;
	public float crunchVolume = 1.0f;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		initialScale = transform.localScale.x;
		playerTransform = FindObjectsByType<CharacterController>(FindObjectsSortMode.InstanceID)[0].transform;
		playerScript = FindObjectsByType<ThirdPersonController>(FindObjectsSortMode.InstanceID)[0];
		parentTransform = GetComponentInParent<Transform>();
		input = GetComponent<StarterAssetsInputs>();
		scoreUI = FindObjectsByType<ScoreUI>(FindObjectsSortMode.InstanceID)[0];
	}

	private void OnInteract()
	{
		if (hasBeenTalkedTo)
		{
			if (scoreUI.GetScore() >= scoreRequired)
			{
				changeText("thank you so much! i was really hungry!");
				triggerLevelEnd();
			}
			else
			{
				changeText("it's you again! it looks like you've found " + scoreUI.GetScore() + " out of " + scoreRequired + " gold cubes!");
			}
		}
		else
		{
			if (scoreUI.GetScore() >= scoreRequired)
			{
				changeText("woah, " + scoreUI.GetScore() + " gold cubes! thank you so much!");
				triggerLevelEnd();
			}
			else
			{
				changeText("hello there! can you help me? i could really use " + scoreRequired + " gold cubes!");
			}
		}

		hasBeenTalkedTo = true;
		hasBeenTalkedToRecently = true;
	}

	private void triggerLevelEnd()
	{
		AudioSource.PlayClipAtPoint(crunchSound, parentTransform.position, crunchVolume);
		particleSystem.Play();
		nextSceneTimer = nextSceneDelay;
	}

	private float burstScale(float delta)
	{
		if (delta < 0 || delta > 1) return 1;

		// I yoinked this equation from a previous project, and I did not write down how it works.
		float strength = 0.5f;
		return 1 + Mathf.Abs(-Mathf.Sin(4 * Mathf.PI * Mathf.Sqrt(delta)) + 1) / 2 * (1 - delta) * strength;
	}

	public void changeText(string text)
	{
		dialog.text = text;
		burstDelta = 0;
	}

	// Update is called once per frame
	void Update()
	{
		dialogLogic();

		// Burst effect
		burstDelta += Time.deltaTime*2;
		transform.localScale = Vector3.one * (initialScale * burstScale(burstDelta));
	}

	private void dialogLogic()
	{
		float distanceToPlayer = (playerTransform.position - (transform.position - parentTransform.localPosition)).magnitude;

		if (distanceToPlayer < (littleGuy.activeRange[0] + littleGuy.activeRange[1]) /2)
		{
			if (hasBeenTalkedToRecently || nextSceneTimer > 0)
			{
				buttonPrompt.hide();
			}
			else
			{
				talkDelta += Time.deltaTime;
				if (talkDelta >= talkRate)
				{
					talkDelta %= talkRate;
					changeText(greetings[Random.Range(0, greetings.Length)]);
				}

				buttonPrompt.show();
				if (playerScript.getInteractState()) OnInteract();
			}
		}
		else if (!dialog.text.Equals(""))
		{
			talkDelta = 0;
			changeText("");

			buttonPrompt.hide();
			hasBeenTalkedToRecently = false;
		}
		else
		{
			buttonPrompt.hide();
		}

		if (nextSceneTimer > 0)
		{
			nextSceneTimer -= Time.deltaTime;
			if (nextSceneTimer <= 0) SceneManager.LoadScene(nextScene);
		}
	}
}
