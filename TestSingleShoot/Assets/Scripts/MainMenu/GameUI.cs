using UnityEngine.SceneManagement;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
