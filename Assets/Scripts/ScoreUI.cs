using NUnit.Framework.Internal;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        score = 0;
        UpdateScore();
	}

    public void IncreaseScore()
    {
        score++;
        UpdateScore();

	}

    public int GetScore()
    {
        return score;
    }

    private void UpdateScore()
    {
		scoreText.text = "Cubes: " + score;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
