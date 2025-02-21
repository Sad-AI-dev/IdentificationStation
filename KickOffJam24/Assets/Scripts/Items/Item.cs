using DevKit;
using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static event Action<ItemData> OnItemCreated;
    public static event Action OnItemCompleted;

    [SerializeField] private Transform visualsHolder;

    [Header("Timings")]
    [SerializeField] private float betweenPuzzleDelay;

    private GameObject activePuzzle;

    private ItemData itemData;

    private int solvedPuzzleCount;

    private void Start()
    {
        Puzzle.OnPuzzleCompleted += Puzzle_OnPuzzleCompleted;
    }

    private void OnDestroy()
    {
        Puzzle.OnPuzzleCompleted -= Puzzle_OnPuzzleCompleted;
    }

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
        AudioManager.instance.PlayOneShot("Completed");
        StartCoroutine(HandlePuzzleCompletedCo());
    }

    private IEnumerator HandlePuzzleCompletedCo()
    {
        solvedPuzzleCount++;
        Destroy(activePuzzle);

        yield return new WaitForSeconds(betweenPuzzleDelay);

        if (solvedPuzzleCount < itemData.puzzleCount)
            SpawnPuzzle();
        else
            OnItemCompleted?.Invoke();
    }

    public void SkipItem()
    {
        if (activePuzzle == null)
            return;

        Destroy(activePuzzle);
    }
}
