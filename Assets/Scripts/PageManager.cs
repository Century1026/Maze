using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages;
    private int currentPage = 0;
    public static int pageToLoad = 0;

    void Start()
    {
        ShowPage(pageToLoad); // show first page
    }

    public void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
        currentPage = index;
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            ShowPage(currentPage + 1); 
        }
        // If already on the last page, don't do anything (or you could add logic for what should happen)
    }

    public void PreviousPage()
    {
        ShowPage((currentPage - 1 + pages.Length) % pages.Length);
    }
}
