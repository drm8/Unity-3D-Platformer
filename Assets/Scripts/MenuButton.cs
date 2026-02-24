using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	public string levelToLoad;

	public void PlayGame()
    {
		SceneManager.LoadScene(levelToLoad);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
