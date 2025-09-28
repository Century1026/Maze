using UnityEngine;

/// <summary>
/// PageManager - Manages UI page navigation and display.
/// Handles showing/hiding different UI pages, navigation between pages,
/// and maintains the current page state. Supports both forward and backward navigation.
/// </summary>
public class PageManager : MonoBehaviour
{
    [Header("Page Management")]
    [Tooltip("Array of GameObjects representing different UI pages")]
    public GameObject[] pages;
    
    [Header("Page State")]
    private int currentPage = 0;           // Index of the currently active page
    public static int pageToLoad = 0;      // Static variable to specify which page to load on start

    /// <summary>
    /// Initialize the page manager by showing the specified starting page
    /// </summary>
    void Start()
    {
        ShowPage(pageToLoad); // Show the page specified by pageToLoad
    }

    /// <summary>
    /// Show a specific page by index and hide all other pages
    /// </summary>
    /// <param name="index">The index of the page to show</param>
    public void ShowPage(int index)
    {
        // Loop through all pages and activate only the one at the specified index
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
        
        // Update the current page tracking
        currentPage = index;
    }

    /// <summary>
    /// Navigate to the next page in sequence
    /// Does nothing if already on the last page
    /// </summary>
    public void NextPage()
    {
        // Check if there are more pages to navigate to
        if (currentPage < pages.Length - 1)
        {
            ShowPage(currentPage + 1); 
        }
        // Note: If already on the last page, no action is taken
        // This could be extended to loop back to the first page or trigger other logic
    }

    /// <summary>
    /// Navigate to the previous page in sequence
    /// Wraps around to the last page if currently on the first page
    /// </summary>
    public void PreviousPage()
    {
        // Use modulo arithmetic to wrap around to the last page when going back from the first page
        ShowPage((currentPage - 1 + pages.Length) % pages.Length);
    }
}
