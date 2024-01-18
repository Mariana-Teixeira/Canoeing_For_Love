using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}