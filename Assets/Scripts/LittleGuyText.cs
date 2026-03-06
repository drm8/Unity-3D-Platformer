using TMPro;
using UnityEngine;

public class LittleGuyText : MonoBehaviour
{
	private float initialScale;
	private float burstDelta = 1;
	public TextMeshProUGUI dialog;
	private Transform playerTransform;
	private Transform parentTransform;
	public LittleGuy littleGuy;
	private float talkDelta;
	public float talkRate;
	public string[] greetings = { "hi!", "hi!", "hey!", "hello!" };

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		initialScale = transform.localScale.x;
		playerTransform = FindObjectsByType<CharacterController>(FindObjectsSortMode.InstanceID)[0].transform;
		parentTransform = GetComponentInParent<Transform>();
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

		// Rotate towards player
		Vector3 origPos = transform.position;
		Vector3 playerPosition = FindObjectsByType<CharacterController>(FindObjectsSortMode.InstanceID)[0].transform.position;
		transform.LookAt(new Vector3(playerPosition.x, origPos.y, playerPosition.z));
	}

	private void dialogLogic()
	{
		float distanceToPlayer = (playerTransform.position - (transform.position - parentTransform.localPosition)).magnitude;

		if (distanceToPlayer < (littleGuy.activeRange[0] + littleGuy.activeRange[1]) /2)
		{
			talkDelta += Time.deltaTime;
			if (talkDelta >= talkRate)
			{
				talkDelta %= talkRate;
				changeText(greetings[Random.Range(0, greetings.Length)]);
			}
		}
		else if (!dialog.text.Equals(""))
		{
			
			talkDelta = 0;
			changeText("");
		}
	}
}
