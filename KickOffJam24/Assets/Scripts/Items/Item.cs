using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static event Action<ItemData> OnItemCreated;
    public static event Action OnItemCompleted;

    [SerializeField] private Transform visualsHolder;

    private GameObject activePuzzle;

    private ItemData itemData;

    private int solvedPuzzleCount;

    public void Init(ItemData itemData)
    {
        this.itemData = itemData;

        // Spawn Visuals
        Instantiate(itemData.visuals, visualsHolder);

        OnItemCreated?.Invoke(itemData);
    }

    public void StartPuzzles()
    {
        solvedPuzzleCount = 0;

        SpawnPuzzle();
    }

    private void SpawnPuzzle()
    {
        GameObject puzzle = Instantiate(itemData.puzzles[solvedPuzzleCount]);
        activePuzzle = puzzle;
    }

    private void Puzzle_OnPuzzleCompleted()
    {
        solvedPuzzleCount++;
        Destroy(activePuzzle);

        if (solvedPuzzleCount < itemData.puzzleCount)
            SpawnPuzzle();
        else
            OnItemCompleted?.Invoke();
    }
}
