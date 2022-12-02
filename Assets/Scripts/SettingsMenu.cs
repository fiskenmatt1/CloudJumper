using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static GameObject settingsMenu;
    public static GameObject audioToggle;

    private static float audioVolume;

    void Start()
    {
        if (settingsMenu != null) { return; }

        settingsMenu = GameObject.FindGameObjectWithTag("SettingsMenu");
        audioToggle = GameObject.FindGameObjectWithTag("AudioToggle");

        settingsMenu.SetActive(false);
    }

    public static void Display()
    {
        settingsMenu.SetActive(true);

        SetToggleDisplay();
    }

    public static void Close()
    {
        settingsMenu.SetActive(false);
    }

    public static void ToggleAudio()
    {
        float audioVolume = PlayerPrefs.GetFloat("AudioVolume", 1);
        float newVolume = (audioVolume == 1) ? 0 : 1;
        AudioListener.volume = newVolume;
        PlayerPrefs.SetFloat("AudioVolume", newVolume);
    }

    public static void SetToggleDisplay()
    {
        audioVolume = PlayerPrefs.GetFloat("AudioVolume", 1);
        bool toggleIsOn = (audioVolume == 1) ? true : false;
        audioToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(toggleIsOn);
    }


}
