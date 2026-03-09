using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	public string levelToLoad;

	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void PlayGame()
    {
		SceneManager.LoadScene(levelToLoad);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
