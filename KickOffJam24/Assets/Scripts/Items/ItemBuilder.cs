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

    [Space]
    public string[] itemNames;
    public StringCollectionSO itemAdjactives;
    public StringCollectionSO itemNouns;

    private int lastChosenTemplate;

    public ItemData BuildItemData()
    {
        int randVisualsIndex = GetRandomVisualsIndex();
        int randPuzzleCount = GetRandomPuzzleCount();

        return new(
            GetVisuals(randVisualsIndex),
            randPuzzleCount,
            GetRandomPuzzles(randPuzzleCount),
            GetItemName(randVisualsIndex),
            GetRandomItemAdjactives(randPuzzleCount),
            GetRandomItemNoun());
    }

    private int GetRandomVisualsIndex()
    {
        int randVisualsIndex = Random.Range(0, itemTemplates.Length);

        // prevent picking the same item 2 times in a row
        if (randVisualsIndex == lastChosenTemplate)
        {
            randVisualsIndex++;

            if (randVisualsIndex >= itemTemplates.Length)
                randVisualsIndex = 0;
        }

        lastChosenTemplate = randVisualsIndex;
        return randVisualsIndex;
    }

    private GameObject GetVisuals(int index)
    {
        return itemTemplates[index];
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

            if (validPuzzles.Count == 0)
                validPuzzles = new(puzzles);
        }

        return randPuzzles;
    }

    private string GetItemName(int visualsIndex)
    {
        return itemNames[visualsIndex];
    }

    private string[] GetRandomItemAdjactives(int puzzleCount)
    {
        int adjactiveCount = puzzleCount - 1;
        string[] adjactives = new string[adjactiveCount];

        List<string> validAdjactives = new(itemAdjactives.collection);

        for (int i = 0; i < adjactiveCount; i++)
        {
            int randIndex = Random.Range(0, validAdjactives.Count);
            adjactives[i] = validAdjactives[randIndex];
            validAdjactives.RemoveAt(randIndex);
        }

        return adjactives;
    }

    private string GetRandomItemNoun()
    {
        int randIndex = Random.Range(0, itemNouns.collection.Length);
        return itemNouns.collection[randIndex];
    }
}
