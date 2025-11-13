using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public void MoveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
