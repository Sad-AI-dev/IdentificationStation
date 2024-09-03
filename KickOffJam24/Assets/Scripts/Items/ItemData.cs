using UnityEngine;

public struct ItemData
{
    public GameObject[] puzzles;

    public GameObject visuals;

    public string name;
    public string[] adjactives;
    public string noun;

    public int puzzleCount;

    public ItemData(
        GameObject visuals,
        int puzzleCount,
        GameObject[] puzzles,
        string name,
        string[] adjactives,
        string noun)
    {
        this.visuals = visuals;
        this.puzzleCount = puzzleCount;
        this.puzzles = puzzles;

        this.name = name;
        this.adjactives = adjactives;
        this.noun = noun;
    }
}
