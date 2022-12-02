using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1F;
    }

    public static void play()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public static void settings()
    {
        //TODO
    }
}
