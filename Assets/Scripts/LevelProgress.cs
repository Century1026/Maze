/// <summary>
/// LevelProgress - A static utility class that manages level unlock progression.
/// Tracks the highest unlocked level and provides methods to unlock new levels,
/// check unlock status, and reset progress. This data persists during the game session.
/// </summary>
public static class LevelProgress
{
    private static int unlockedLevel = 1; // The highest level that has been unlocked (starts at 1)

    /// <summary>
    /// Unlock a new level if it's higher than the current highest unlocked level
    /// </summary>
    /// <param name="levelIndex">The level index to unlock</param>
    public static void UnlockLevel(int levelIndex)
    {
        // Only unlock if the new level is higher than the current highest
        if (levelIndex > unlockedLevel)
            unlockedLevel = levelIndex;
    }

    /// <summary>
    /// Get the highest level that has been unlocked
    /// </summary>
    /// <returns>The highest unlocked level index</returns>
    public static int GetUnlockedLevel()
    {
        return unlockedLevel;
    }

    /// <summary>
    /// Reset all progress back to level 1
    /// Useful for testing or implementing a "reset progress" feature
    /// </summary>
    public static void ResetProgress()
    {
        unlockedLevel = 1;
    }
}
