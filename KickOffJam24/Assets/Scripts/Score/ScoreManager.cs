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

    private float multiplier;

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

            // Reset Score
            score = 0;
            multiplier = 1f;
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
        int scoreToAdd = Mathf.FloorToInt(itemReward * multiplier);
        SetScore(score + scoreToAdd);

        SetMultiplier(multiplier + itemMultReward);
    }

    private void Puzzle_OnPuzzleCompleted()
    {
        int scoreToAdd = Mathf.FloorToInt(puzzleReward * multiplier);
        SetScore(score + scoreToAdd);

        SetMultiplier(multiplier + puzzleMultReward);
    }

    private void Puzzle_OnMistakeMade()
    {
        SetMultiplier(1f);
    }

    public void ResetScore()
    {
        score = 0;
        multiplier = 1f;
    }

    private void SetScore(int newScore)
    {
        score = newScore;
        OnScoreChanged?.Invoke(score);
    }

    private void SetMultiplier(float newMultiplier)
    {
        multiplier = newMultiplier;
        OnMultiplierChanged?.Invoke(multiplier);
    }
}
