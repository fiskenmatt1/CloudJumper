using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    private void Awake()
    {
        CleanupOldAudioSources(2); //TODO: get number dynamically?

        // prevent audio cutting out upon scene change
        DontDestroyOnLoad(this.gameObject);
    }

    //TODO: this method is probably not necessary - look into sharing single ClickAudioSource object across all scenes
    /// <summary>
    /// Remove unnecessary ClickAudioSource objects
    /// </summary>
    /// <param name="numberOfAudioSources">Number of ClickAudioSource objects allowed to be active at once.</param>
    private void CleanupOldAudioSources(int numberOfAudioSources)
    {
        GameObject[] audioSourceObjects = GameObject.FindGameObjectsWithTag("ClickAudioSource");

        if (audioSourceObjects.Length > numberOfAudioSources)
        {
            Destroy(audioSourceObjects[0]);
        }
    }

}