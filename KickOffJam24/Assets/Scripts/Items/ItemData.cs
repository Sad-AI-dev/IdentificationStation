using UnityEngine;

public struct ItemData
{
    public GameObject[] puzzles;

    public GameObject visuals;

    public int puzzleCount;

    public ItemData(
        GameObject visuals,
        int puzzleCount,
        GameObject[] puzzles)
    {
        this.visuals = visuals;
        this.puzzleCount = puzzleCount;
        this.puzzles = puzzles;
    }
}
