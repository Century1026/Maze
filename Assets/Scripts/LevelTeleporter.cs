using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// LevelTeleporter - A utility component that provides scene transition functionality.
/// Contains methods for loading the next scene in sequence or loading a specific scene by index.
/// Also includes commented-out code for end zone trigger functionality.
/// </summary>
public class LevelTeleporter : MonoBehaviour
{
    /// <summary>
    /// Load the next scene in the build order
    /// Calculates the next scene index based on the current scene
    /// </summary>
    public void nextScene()
    {
        // Get the current scene's build index
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        
        // Load the next scene in sequence
        SceneManager.LoadScene(currentScene + 1);
    }

    /// <summary>
    /// Load a specific scene by its build index
    /// Includes validation to ensure the scene index is valid
    /// </summary>
    /// <param name="sceneIndex">The build index of the scene to load</param>
    public void ShowScene(int sceneIndex)
    {
        // Validate that the scene index is within the valid range
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            // Log a warning if an invalid scene index is provided
            Debug.LogWarning("Invalid scene index: " + sceneIndex);
        }
    }
    
    // NOTE: The following code is commented out but shows an alternative implementation
    // for handling end zone triggers with audio feedback and page management
    
    // public AudioSource endZoneSound;
    // public PageManager pageManager;

    // /// <summary>
    // /// Handle player entering the end zone trigger
    // /// Plays sound and transitions to next page
    // /// </summary>
    // /// <param name="other">The collider that entered the trigger</param>
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("EndZone"))
    //     {
    //         // Alternative: Use coroutine for delayed loading
    //         // StartCoroutine(PlayAndLoad());
    //         AudioSource.PlayClipAtPoint(endZoneSound.clip, transform.position);
    //         pageManager.NextPage();
    //     }
    // }

    // /// <summary>
    // /// Coroutine to play sound and then load next scene after audio finishes
    // /// </summary>
    // /// <returns>IEnumerator for coroutine execution</returns>
    // private IEnumerator PlayAndLoad()
    // {
    //     AudioSource.PlayClipAtPoint(endZoneSound.clip, transform.position);
    //     yield return new WaitForSeconds(endZoneSound.clip.length);
    // }
}