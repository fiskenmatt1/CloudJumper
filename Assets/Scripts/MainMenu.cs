using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1F;
    }

    public static void Play()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public static void Settings()
    {
        //TODO
    }
}
