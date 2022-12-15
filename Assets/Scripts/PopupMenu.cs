using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PopupMenu : MonoBehaviour
{
    public static GameObject popupMenu;
    public static GameObject pauseButton;
    public static GameObject closeButton;
    public static GameObject newBestText;
    public static TMP_Text popupMenuText;

    public static bool isPaused = false;
    public static bool didAchieveNewBest = false;

    void Start()
    {
        if (popupMenu != null) { return; }
        isPaused = false;

        popupMenu = GameObject.FindGameObjectWithTag("PopupMenu");
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
        closeButton = GameObject.FindGameObjectWithTag("CloseButton");
        newBestText = GameObject.FindGameObjectWithTag("NewBestText");
        popupMenuText = GameObject.FindGameObjectWithTag("PopupMenuText").GetComponent<TMP_Text>();

        popupMenu.SetActive(false);
        pauseButton.SetActive(true);
        closeButton.SetActive(false);
        newBestText.SetActive(false);
    }

    public static void Display(bool playerDied = false)
    {
        pauseButton.SetActive(false);
        popupMenu.SetActive(true);
        closeButton.SetActive(!playerDied);
        newBestText.SetActive(didAchieveNewBest);

        //TODO: add sfx on if playerDied == true
        popupMenuText.text = playerDied ? "GAME OVER" : "PAUSED";

        // Just in case reset bool to false after display
        didAchieveNewBest = false;
    }

    public static void Replay() 
    {
        isPaused = false;

        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public static void Home()
    {
        isPaused = false;

        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }


    public static void Close() 
    {
        isPaused = false;

        popupMenu.SetActive(false);
        pauseButton.SetActive(true);
        closeButton.SetActive(false);

        Time.timeScale = 1F;
    }

    public static void Pause(bool playerDied = false) 
    {
        isPaused = true;

        Time.timeScale = 0F;

        Display(playerDied);
    }

}
