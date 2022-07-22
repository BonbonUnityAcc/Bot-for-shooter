using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Setting()
    {
        Debug.Log("Push setting");
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
