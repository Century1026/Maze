using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// LevelButton - A UI button component that handles level selection functionality.
/// Manages button interactivity based on level unlock status and loads the
/// appropriate scene when clicked.
/// </summary>
public class LevelButton : MonoBehaviour
{
    [Header("Level Settings")]
    [Tooltip("The build index of the scene this button should load")]
    public int levelIndex; 
    
    [Header("UI References")]
    [Tooltip("The Button component that handles the click interaction")]
    public Button button;

    /// <summary>
    /// Initialize the level button by checking unlock status and setting up click handler
    /// </summary>
    void Start()
    {
        // Get the highest unlocked level from the progress system
        int unlocked = LevelProgress.GetUnlockedLevel();
        
        // Check if this level is unlocked
        if (levelIndex <= unlocked)
        {
            // Enable the button and add click listener to load the scene
            button.interactable = true;
            button.onClick.AddListener(() => SceneManager.LoadScene(levelIndex));
        }
        else
        {
            // Disable the button if the level is not yet unlocked
            button.interactable = false;
        }
    }
}
