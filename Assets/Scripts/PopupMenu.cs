using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenu : MonoBehaviour
{
    public static GameObject popupMenu;
    public static GameObject pauseButton;
    public static GameObject closeButton;
    public static bool isPaused = false;

    void Start()
    {
        if (popupMenu != null) { return; }
        isPaused = false;

        popupMenu = GameObject.FindGameObjectWithTag("PopupMenu");
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
        closeButton = GameObject.FindGameObjectWithTag("CloseButton");

        popupMenu.SetActive(false);
        pauseButton.SetActive(true);
        closeButton.SetActive(false);
    }

    public static void display(bool displayCloseButton = false) 
    {
        pauseButton.SetActive(false);
        popupMenu.SetActive(true);
        closeButton.SetActive(displayCloseButton);
    }

    public static void replay() 
    {
        isPaused = false;
        Application.LoadLevel(Application.loadedLevel); //TODO: obselete
    }

    public static void home()
    {
        //TODO
    }


    public static void close() 
    {
        isPaused = false;
        popupMenu.SetActive(false);
        pauseButton.SetActive(true);
        closeButton.SetActive(false);
        Time.timeScale = 1F;
    }

    public static void pause(bool playerDied = false) 
    {
        isPaused = true;
        Time.timeScale = 0F;
        display(!playerDied);
    }

}
