using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelIndex; // assign in Inspector
    public Button button;

    void Start()
    {
        int unlocked = LevelProgress.GetUnlockedLevel();
        if (levelIndex <= unlocked)
        {
            button.interactable = true;
            button.onClick.AddListener(() => SceneManager.LoadScene(levelIndex));
        }
        else
        {
            button.interactable = false;
        }
    }
}
