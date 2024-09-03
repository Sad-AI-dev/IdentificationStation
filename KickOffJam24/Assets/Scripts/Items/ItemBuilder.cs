using System.Collections.Generic;
using UnityEngine;

public class ItemBuilder : MonoBehaviour
{
    [Header("Settings")]
    public GameObject[] itemTemplates;

    [Space]
    public int minPuzzles;
    public int maxPuzzles;
    public GameObject[] puzzles;

    public ItemData BuildItemData()
    {
        int randPuzzleCount = GetRandomPuzzleCount();

        return new(
            GetRandomVisuals(),
            randPuzzleCount,
            GetRandomPuzzles(randPuzzleCount));
    }

    private GameObject GetRandomVisuals()
    {
        int randIndex = Random.Range(0, itemTemplates.Length);
        return itemTemplates[randIndex];
    }

    private int GetRandomPuzzleCount()
    {
        return Random.Range(
            minPuzzles,
            maxPuzzles + 1);
    }

    private GameObject[] GetRandomPuzzles(int puzzleCount)
    {
        List<GameObject> validPuzzles = new(puzzles);
        GameObject[] randPuzzles = new GameObject[puzzleCount];

        for (int i = 0; i < puzzleCount; i++)
        {
            int randIndex = Random.Range(0, validPuzzles.Count);

            randPuzzles[i] = validPuzzles[randIndex];
            validPuzzles.RemoveAt(randIndex);
        }

        return randPuzzles;
    }
}
