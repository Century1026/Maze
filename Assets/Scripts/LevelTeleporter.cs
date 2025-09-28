using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelTeleporter : MonoBehaviour
{
    public void nextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void ShowScene(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogWarning("Invalid scene index: " + sceneIndex);
        }
    }
    // public AudioSource endZoneSound;
    // public PageManager pageManager;

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("EndZone"))
    //     {
    //         // StartCoroutine(PlayAndLoad());
    //         AudioSource.PlayClipAtPoint(endZoneSound.clip, transform.position);
    //         pageManager.NextPage();
    //     }
    // }

    // private IEnumerator PlayAndLoad()
    // {
    //     AudioSource.PlayClipAtPoint(endZoneSound.clip, transform.position);
    //     yield return new WaitForSeconds(endZoneSound.clip.length);
    // }
}