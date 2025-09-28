public static class LevelProgress
{
    private static int unlockedLevel = 1;

    public static void UnlockLevel(int levelIndex)
    {
        if (levelIndex > unlockedLevel)
            unlockedLevel = levelIndex;
    }

    public static int GetUnlockedLevel()
    {
        return unlockedLevel;
    }

    public static void ResetProgress()
    {
        unlockedLevel = 1;
    }
}
