using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelTrigger : MonoBehaviour
{
    public string scene;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			SceneManager.LoadScene(scene);
        }
    }
}
