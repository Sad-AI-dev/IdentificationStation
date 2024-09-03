using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> OnScoreChanged;
    public event Action<float> OnMultiplierChanged;

    public static ScoreManager instance;

    [Header("Score Values")]
    [SerializeField] private int puzzleReward;
    [SerializeField] private int itemReward;

    [Header("Multiplier Values")]
    [SerializeField] private float puzzleMultReward;
    [SerializeField] private float itemMultReward;

    public int score { get; private set; }

    private float multiplier = 1f;

    private void Awake()
    {
        Item.OnItemCompleted += Item_OnItemCompleted;
        Puzzle.OnPuzzleCompleted += Puzzle_OnPuzzleCompleted;
        Puzzle.OnMistakeMade += Puzzle_OnMistakeMade;

        if (instance != null && instance != this)
            Destroy(gameObject);
        else
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        Item.OnItemCompleted -= Item_OnItemCompleted;
        Puzzle.OnPuzzleCompleted -= Puzzle_OnPuzzleCompleted;
        Puzzle.OnMistakeMade -= Puzzle_OnMistakeMade;
    }

    private void Item_OnItemCompleted()
    {
        score += Mathf.FloorToInt(itemReward * multiplier);
        OnScoreChanged?.Invoke(score);

        multiplier += itemMultReward;
        OnMultiplierChanged?.Invoke(multiplier);
    }

    private void Puzzle_OnPuzzleCompleted()
    {
        score += Mathf.FloorToInt(puzzleReward * multiplier);
        OnScoreChanged?.Invoke(score);

        multiplier += puzzleMultReward;
        OnMultiplierChanged?.Invoke(multiplier);
    }

    private void Puzzle_OnMistakeMade()
    {
        multiplier = 0;
        OnMultiplierChanged?.Invoke(multiplier);
    }

    public void ResetScore()
    {
        score = 0;
        multiplier = 1f;
    }
}
